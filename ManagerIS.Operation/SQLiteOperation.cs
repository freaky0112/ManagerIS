using ManagerIS.Helper;
using ManagerIS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIS.Operation
{
    public class SQLiteOperation
    {
        /// <summary>
        /// 数据库是否存在，如果不存在新建数据库
        /// </summary>
        public static void Initialize() {
            if (!File.Exists(Description.DATABASE)) {
                CreatDatabase();

            }
        }


        private static void CreatDatabase() {
            string sql = Description.CREATE();
            SQLiteHelper.CreateDB(Description.DATABASE);
            SQLiteHelper db = new SQLiteHelper(Description.DATABASE);            
            db.ExecuteNonQuery(sql, null);
        }
    }
}
