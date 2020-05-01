using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public interface IRegKeys
    {
        string RegKeyStringGet(RegKeyModel model);
    }
}