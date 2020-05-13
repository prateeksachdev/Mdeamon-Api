using altn_common.Profiles;
using AltnCrossAPI.BusinessLogic.Interfaces;
using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Database.Models;
using AltnCrossAPI.Shared;
using Newtonsoft.Json;
using AltnCrossAPI.Shared.Logging;
using ShopifySharp;
using System;
using System.Net;
using static AltnCrossAPI.Shared.Enums;

namespace AltnCrossAPI.BusinessLogic
{
    public class CustomersBL : ICustomersBL
    {
        private IShopifyData _shopifyData;
        private IUsers _user;
        private ILogger _logger;
        public CustomersBL(
            IShopifyData shopifyData,
            IUsers user,
            ILogger logger)
        {
            _shopifyData = shopifyData;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// Inserts a new shopify customer or Update existing shopify customer in db
        /// </summary>
        /// <param name="customer">ShopifySharp Customer posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        public Result CustomerSync(Customer customer)
        {
            Result result = new Result();
            try
            {
                if (customer.Id == null || string.IsNullOrEmpty(customer.Email))
                {
                    result.Message = ToStringHttpCustomResponse(CustomerHttpCustomResponse.IdEmailRequired);
                    return result;
                }
                //Adding logs of the json posted from shopify to the database
                ShopifyDataModel sData = new ShopifyDataModel();
                sData.JSON = JsonConvert.SerializeObject(customer);
                sData.EventType = "Insert";
                sData.Entity = "Customer";
                sData.DateAdded = DateTime.Now;
                sData.Id = _shopifyData.ShopifyDataInsert(sData);

                UserProfile existingUser_Email = new UserProfile(customer.Email);
                UsersModel existingUser_ShopifyID = _user.UserGetByShopifyCustomerID(customer.Id);

                //New user. add it to db
                if (!existingUser_Email.IsPopulated && !existingUser_ShopifyID.IsPopulated)
                {
                    UserProfile newUser = new UserProfile(Guid.NewGuid().ToString(), customer.Email, UserType.EndUser);
                    newUser.Save();
                    _user.UserShopifyCustomerIDUpdate(new UsersModel { UserID = newUser.UserID, ShopifyCustomerID = customer.Id ?? 0 });

                }
                //User exists but does not have ShopifyCustomerID. So Add it.
                else if (!existingUser_ShopifyID.IsPopulated && existingUser_Email.IsPopulated)
                {
                    _user.UserShopifyCustomerIDUpdate(new UsersModel { UserID = existingUser_Email.UserID, ShopifyCustomerID = customer.Id ?? 0 });
                }
                //User Email changed but already exists with other user in db.
                else if (existingUser_ShopifyID.IsPopulated && existingUser_Email.IsPopulated && existingUser_ShopifyID.UserID != existingUser_Email.UserID)//it means userId does not exists or user email changed and user with new email already exists
                {
                    result.Message = ToStringHttpCustomResponse(CustomerHttpCustomResponse.EmailAlreadyExist);
                    return result;
                }
                //update user.
                else
                {
                    if (existingUser_Email.UserID == null)//email changed
                    {
                        existingUser_Email = new UserProfile(existingUser_ShopifyID.UserID);
                    }
                    existingUser_Email.UserEmail = customer.Email;
                    //existingUser_Email.UserType = UserType.EndUser;
                    existingUser_Email.Save();
                    _shopifyData.ShopifyDataUpdate(sData.Id);
                }

                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("CustomerSync", exp);
                result.Message = HttpStatusCode.InternalServerError.ToString();
            }
            return result;
        }

        /// <summary>
        /// Deletes shopify customer in db
        /// </summary>
        /// <param name="customer">ShopifySharp Customer posted by shopify</param>
        /// <returns>Returns status of the request after processing</returns>
        public Result CustomerDelete(Customer customer)
        {
            Result result = new Result();
            try
            {
                if (customer.Id == null)
                {
                    result.Message = ToStringHttpCustomResponse(CustomerHttpCustomResponse.IdEmailRequired);
                    return result;
                }
                //Adding logs of the json posted from shopify to the database
                ShopifyDataModel sData = new ShopifyDataModel();
                sData.JSON = JsonConvert.SerializeObject(customer);
                sData.EventType = "Delete";
                sData.Entity = "Customer";
                sData.DateAdded = DateTime.Now;
                sData.Id = _shopifyData.ShopifyDataInsert(sData);

                _user.UserDeleteByShopifyCustomerID(customer.Id);

                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("CustomerDelete", exp);
                result.Message = HttpStatusCode.InternalServerError.ToString();
            }
            return result;
        }
    }
}