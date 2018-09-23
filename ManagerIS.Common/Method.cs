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
        /// <summary>
        /// 数据库是否存在，如果不存在新建数据库
        /// </summary>
        public static void Initialize() {
            if (File.Exists(Description.DATABASE)) {
                SQLiteHelper.CreateDB(Description.DATABASE);
            }
        }
    }


}
