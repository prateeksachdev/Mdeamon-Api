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
    public class CustomersBL: ICustomersBL
    {
        private IShopifyData _shopifyData;
        private ILogger _logger;
        public CustomersBL(
            IShopifyData shopifyData,
            ILogger logger)
        {
            _shopifyData = shopifyData;
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
                UserProfile existingUser_UserID = new UserProfile(customer.Id.ToString());

                //New user. add it to db
                if (!existingUser_Email.IsPopulated && !existingUser_UserID.IsPopulated)
                {
                    UserProfile newUser = new UserProfile(customer.Id.ToString(), customer.Email, UserType.EndUser);
                    newUser.Save();
                }
                //User Email changed but already in use in db.
                else if ((!existingUser_UserID.IsPopulated && existingUser_Email.IsPopulated) || (existingUser_UserID.IsPopulated && existingUser_Email.IsPopulated && existingUser_UserID.UserID != existingUser_Email.UserID))//it means userId does not exists or user email changed and user with new email already exists
                {
                    result.Message = ToStringHttpCustomResponse(CustomerHttpCustomResponse.EmailAlreadyExist);
                    return result;
                }
                //update user.
                else
                {
                    existingUser_UserID.UserEmail = customer.Email;
                    existingUser_UserID.UserType = UserType.EndUser;
                    existingUser_UserID.Save();
                    _shopifyData.ShopifyDataUpdate(sData.Id);
                }

                result.Message = HttpStatusCode.OK.ToString();
            }
            catch (Exception exp)
            {
                _logger.Error("CustomerSync", exp);
                result.Message = exp.Message;
            }
            return result;
        }
    }
}