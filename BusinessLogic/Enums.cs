using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AltnCrossAPI.BusinessLogic
{
    public class Enums
    {
        public enum OrderHttpCustomResponse
        {
            [Description("Request from shopify could not verified as per the request headers received.")]
            AuthenticationError = 1301,
            [Description("Payment not received yet.")]
            POWireTransferPaymentsError,
            [Description("Order cannot be processed until paid")]
            PaymentError,
            [Description("Error occured while getting keys.")]
            KeyRetrievalError
        }
        public enum CustomerHttpCustomResponse
        {
            [Description("Customer Email already in use.")]
            EmailAlreadyExist = 1401,
            [Description("UserId and UserEmail are required.")]
            IdEmailRequired
        }
        public enum ProductsHttpCustomResponse
        {
            [Description("Either productCode or skuString is required.")]
            UnitPriceCodeOrSkuError = 1501,
            [Description("UserId and UserEmail are required.")]
            IdEmailRequired,
            [Description("Order cannot be processed until paid")]
            PaymentError,
            [Description("Error occured while getting keys.")]
            KeyRetrievalError
        }

        public static string ToStringEnums(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        public static string ToStringHttpCustomResponse(OrderHttpCustomResponse en)
        {
            return ToStringEnums(en) + "(Error code - " + (int)en + ")";
        }
        public static string ToStringHttpCustomResponse(CustomerHttpCustomResponse en)
        {
            return ToStringEnums(en) + "(Error code - " + (int)en + ")";
        }
        public static string ToStringHttpCustomResponse(ProductsHttpCustomResponse en)
        {
            return ToStringEnums(en) + "(Error code - " + (int)en + ")";
        }
    }
}