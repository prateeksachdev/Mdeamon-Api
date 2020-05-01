using altn_common.KeyCodes;
using altn_common.Profiles;
using AltnCrossAPI.Database;
using AltnCrossAPI.Helper;
using Newtonsoft.Json;
using ShopifySharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static AltnCrossAPI.BusinessLogic.Enums;

namespace AltnCrossAPI.BusinessLogic
{
    public interface ICustomersBL
    {
        Result CustomerSync(Customer customer);
    }
}