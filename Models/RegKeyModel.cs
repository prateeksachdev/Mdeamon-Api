﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
    public class RegKeyModel
    {
        #region Class Property Declarations

        public int ProductSize { get; set; }

        public string UserID { get; set; }

        public string Username { get; set; }

        public string SKU { get; set; }
        #endregion
    }
}