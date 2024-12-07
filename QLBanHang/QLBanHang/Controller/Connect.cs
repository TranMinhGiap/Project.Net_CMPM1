using QLBanHang.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBanHang.Controller
{
    public class Connect
    {
        public SqlConnection connect;
        private string sql = "Data Source=DESKTOP-VDPSTNV;Initial Catalog=QLBanHang2;User ID=sa; Password = 15092004";
        public Connect()
        {
            connect = new SqlConnection(sql);
        }
        public SqlConnection GetConnect()
        {
            return connect;
        }
    }
}
