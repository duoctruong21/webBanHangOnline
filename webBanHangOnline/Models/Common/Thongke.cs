using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webBangHangOnline.Models.Common
{
    public class Thongke
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static StatisticalViewModel Statistical() {
            using(var connect = new SqlConnection(strConnect)){
                var item = connect.QueryFirstOrDefault<StatisticalViewModel>("sp_thongke", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}