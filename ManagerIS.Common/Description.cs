using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIS.Common {
    public class Description {
        public static readonly string DATABASE = @"data.db";

        public static string CREATE() {
            StringBuilder sqlString = new StringBuilder();

            //创建农转用表
            sqlString.Append("CREATE TABLE NZY(");
            sqlString.Append("GUID VARCHAR(36) PRIMARY KEY");
            sqlString.Append("UNIQUE,");
            sqlString.Append("PCMC TEXT,");
            sqlString.Append("PZWH TEXT,");
            sqlString.Append("PZRQ DATE,");
            sqlString.Append("PZMJ DECIMAL");
            sqlString.Append("  );");
            sqlString.AppendLine();


            //创建地块表
            sqlString.Append("CREATE TABLE DKQK(");
            sqlString.Append("GUID VARCHAR(36) PRIMARY KEY,");
            sqlString.Append("NZY  VARCHAR(36) REFERENCES NZY(GUID) ON UPDATE NO ACTION,");
            sqlString.Append("DKMC TEXT,");
            sqlString.Append("DKMJ DECIMAL");
            sqlString.Append("DGMJ DECIMAL");
            sqlString.Append("BZ TEXT,");
            sqlString.Append(");");
            sqlString.AppendLine();
            //创建供地情况表
            sqlString.Append("CREATE TABLE GDQK(");
            sqlString.Append("DZJGH  TEXT         PRIMARY KEY,");
            sqlString.Append("YDDW   TEXT,");
            sqlString.Append("GDMJ   DECIMAL,");
            sqlString.Append("TDYT   TEXT,");
            sqlString.Append("DKGUID VARCHAR(36)");
            sqlString.Append(");");
            sqlString.AppendLine();
            return sqlString.ToString();
        }
    }
}
