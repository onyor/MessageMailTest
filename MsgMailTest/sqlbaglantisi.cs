﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MsgMailTest
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-9BAHUVF;Initial Catalog=DboMesajSistemi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
