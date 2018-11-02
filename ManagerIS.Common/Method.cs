using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ManagerIS.Helper;

namespace ManagerIS.Common
{
    public class Method    {
        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="strNumber">数字</param>
        /// <returns></returns>
        public static bool IsNumber(string strNumber) {
            //看要用哪種規則判斷，自行修改strValue即可

            //strValue = @"^\d+[.]?\d*$";//非負數字
            string strValue = @"^\d+(\.)?\d*$";//數字
            //strValue = @"^\d+$";//非負整數
            //strValue = @"^-?\d+$";//整數
            //strValue = @"^-[0-9]*[1-9][0-9]*$";//負整數
            //strValue = @"^[0-9]*[1-9][0-9]*$";//正整數
            //strValue = @"^((-\d+)|(0+))$";//非正整數
            Regex r = new Regex(strValue);
            return r.IsMatch(strNumber);
        }

        #region mysql数据库链接
        //看连接的哪个数据库
        //private const string SERVER = "192.168.1.105";
        private const string SERVER = "10.37.129.2";
        //private const string SERVER = "192.168.3.41";
        //private const string SERVER = "20.13.6.209";
        private const uint PORT = 3306;

        private const string DATABASE = "info";

        private const string UID = "admin";

        private const string PWD = "admin";

        private const string CHARSET = "'utf8'";

        public static string table = "";
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string Conntection() {
            StringBuilder conn = new StringBuilder();
            conn.Append("server=");
            conn.Append(SERVER);
            conn.Append(";port=");
            conn.Append(PORT);
            conn.Append(";database=");
            conn.Append(DATABASE);
            conn.Append(";uid=");
            conn.Append(UID);
            conn.Append(";pwd=");
            conn.Append(PWD);
            conn.Append(";charset=");
            conn.Append(CHARSET);
            conn.Append(";Max Pool Size=30000");
            return conn.ToString();

        }
        #endregion
    }


}
