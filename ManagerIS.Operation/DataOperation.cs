using ManagerIS.Common;
using ManagerIS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading;

namespace ManagerIS.Operation {
    public abstract class DataOperation {
        public static bool ExcelToMysql(string file, int year) {
            List<Data> datas = DatatableRead(ExcelToData(file, year));
            DatatableToMySQL(datas);
            return true;
        }

        private static DataTable ExcelToData(string file, int year) {
            ExcelHelper excelHelper = new ExcelHelper(file);
            DataTable dt = excelHelper.ExcelToDataTable(year.ToString(), true);
            return dt;
        }

        private static List<Data> DatatableRead(DataTable dt) {
            List<Data> result = new List<Data>();
            Data data = new Data();
            for (int i = 0; i < dt.Rows.Count; i++) {
                DataRow dr = dt.Rows[i];
                string pzwh = dr["批准文号"].ToString();
                #region 如果不存在该批准文号并且该栏不为空
                if (!IsExist(result, pzwh) && !String.IsNullOrWhiteSpace(pzwh)) {
                    data = new Data(pzwh);
                    //data.Pzwh = pzwh;
                    data.Nzy = dr["批次名称"].ToString();
                    try {
                        DateTime pzrq = new DateTime();
                        if (DateTime.TryParse(dr["批准时间"].ToString(), out pzrq)) {
                            data.Pzrq = pzrq;
                        }
                    } catch (Exception) {
                        throw new Exception("wrong datetime");
                    }
                    result.Add(data);
                }
                #endregion


                NZYDK nzydk = new NZYDK();
                string dkmc = dr["地块名称"].ToString();
                #region 如果该栏不为空
                if (!String.IsNullOrWhiteSpace(dkmc)) {

                    nzydk = data.IsExist(dkmc);
                    //nzydk.Dkmc = dkmc;
                    Decimal dkmj = new decimal();
                    try {
                        if (Decimal.TryParse(dr["地块面积"].ToString(), out dkmj)) {
                            nzydk.Dkmj = dkmj;
                        }
                    } catch (Exception) {
                        throw new Exception(dkmc);
                    }
                    //data.Dk.Add(nzydk);
                }
                #endregion

                if (!string.IsNullOrEmpty(dr["出让面积"].ToString())) {
                    GDDK gddk = new GDDK();
                    gddk.Dzjgh = dr["供地文号"].ToString();
                    gddk.Xmmc = dr["用地单位"].ToString();

                    gddk.Gdmj = Decimal.Parse(dr["出让面积"].ToString());
                    if (string.IsNullOrWhiteSpace(dr["带供"].ToString())) {
                        gddk.Dgmj = 0;
                    } else {
                        gddk.Dgmj = Decimal.Parse(dr["带供"].ToString());
                    }
                    gddk.Bz = dr["备注"].ToString();
                    nzydk.Gddk.Add(gddk);
                }



            }


            return result;
        }


        private static bool IsExist(List<Data> datas, string pzwh) {
            foreach (Data data in datas) {
                if (data.Pzwh == pzwh) {
                    return true;
                }
            }
            return false;
        }

        private static void DatatableToMySQL(List<Data> datas) {
            foreach (Data data in datas) {
                DataToMySQL(data);
                foreach (NZYDK nzydk in data.Dk) {
                    NzydkToMySQL(data.Guid, nzydk);
                    foreach (GDDK gddk in nzydk.Gddk) {
                        GddkToMySQL(nzydk.Guid, gddk);
                    }
                }
            }
        }
        /// <summary>
        /// 批次写入数据库
        /// </summary>
        /// <param name="data"></param>
        private static void DataToMySQL(Data data) {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into nzy (");
            sql.Append("GUID,");
            sql.Append("PCMC,");
            sql.Append("PZWH,");
            sql.Append("PZRQ,");
            sql.Append("PZMJ");
            sql.Append(") values (");
            sql.Append("@GUID,");
            sql.Append("@PCMC,");
            sql.Append("@PZWH,");
            sql.Append("@PZRQ,");
            sql.Append("@PZMJ)");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("GUID",data.Guid),
                new MySqlParameter("PCMC",data.Nzy),
                new MySqlParameter("PZWH",data.Pzwh),
                new MySqlParameter("PZRQ",data.Pzrq),
                new MySqlParameter("PZMJ",data.GetArea())
            };
            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
            } catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 农转用地块写入数据库
        /// </summary>
        /// <param name="nzydk"></param>
        private static void NzydkToMySQL(Guid guid, NZYDK nzydk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into dkqk (");
            sql.Append("GUID,");
            sql.Append("NZY,");
            sql.Append("DKMC,");
            sql.Append("DKMJ,");
            sql.Append("BZ");
            sql.Append(") values (");
            sql.Append("@GUID,");
            sql.Append("@NZY,");
            sql.Append("@DKMC,");
            sql.Append("@DKMJ,");
            sql.Append("@BZ)");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@GUID",nzydk.Guid),
                new MySqlParameter("@NZY",guid),
                new MySqlParameter("@DKMC",nzydk.Dkmc),
                new MySqlParameter("@DKMJ",nzydk.Dkmj),
                new MySqlParameter("@BZ",nzydk.Bz)
            };
            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
            } catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 供地情况写入数据库
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="gddk"></param>
        public static void GddkToMySQL(Guid guid, GDDK gddk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into gdqk (");
            sql.Append("DKGUID,");
            sql.Append("DZJGH,");
            sql.Append("YDDW,");
            sql.Append("GDMJ,");
            sql.Append("DG, ");
            sql.Append("BZ ");
            sql.Append(") values (");
            sql.Append("@DKGUID,");
            sql.Append("@DZJGH,");
            sql.Append("@YDDW,");
            sql.Append("@GDMJ,");
            sql.Append("@DG,");
            sql.Append("@BZ)");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@DKGUID",guid),
                new MySqlParameter("@DZJGH",gddk.Dzjgh),
                new MySqlParameter("@YDDW",gddk.Xmmc),
                new MySqlParameter("@GDMJ",gddk.Gdmj),
                new MySqlParameter("@DG",gddk.Dgmj),
                new MySqlParameter("@BZ",gddk.Bz)
            };
            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
            } catch (Exception) {

                throw;
            }
        }


        /// <summary>
        /// 读取批次信息
        /// </summary>
        /// <returns></returns>
        public static List<Data> MySQLRead() {
            List<Data> datas = new List<Data>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from nzy ORDER BY PZRQ asc");
            MySqlDataReader reader;
            try {
                reader = Helper.MySqlHelper.ExecuteReader(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (Exception) {
                throw;
            }
            while (reader.Read()) {
                Data data = ReadData(reader);
                datas.Add(data);
            }
            return datas;
        }

        private static Data ReadData(MySqlDataReader reader) {
            Data data = new Data();
            try {
                data.Guid = reader.GetGuid("GUID");
                data.Nzy = reader.GetString("PCMC");
                data.Pzwh = reader.GetString("PZWH");
                data.Pzrq = reader.GetDateTime("PZRQ");
            } catch (Exception) {

                throw;
            }
            return data;
        }

        public static void MySQLDKRead(Data data) {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM info.dkqk where NZY='");
            sql.Append(data.Guid);
            sql.Append(@"'");
            //MySqlParameter[] pt = new MySqlParameter[] {
            //    new MySqlParameter("@NZY", data.Guid)
            //};

            MySqlDataReader reader;
            try {
                reader = Helper.MySqlHelper.ExecuteReader(Method.Conntection(), CommandType.Text, sql.ToString(), null);
                data.Dk = new List<NZYDK>();
                while (reader.Read()) {

                    NZYDK nzydk = new NZYDK();
                    nzydk.Dkmc = reader.GetString("DKMC");
                    nzydk.Guid = reader.GetGuid("GUID");
                    nzydk.Dkmj = reader.GetDecimal("DKMJ");
                    nzydk.Bz = (reader.IsDBNull(5)) ? "" : reader.GetString("BZ");

                    data.Dk.Add(nzydk);
                }
            } catch (Exception ex) {
                if (ex.Message.Contains("Timeout")) {
                    MySQLDKRead(data);
                }
            }
            
            



        }

        public static void MySQLGDRead(NZYDK nzydk) {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM info.gdqk where DKGUID='");
            sql.Append(nzydk.Guid);
            sql.Append(@"'");
            //MySqlParameter[] pt = new MySqlParameter[] {
            //    new MySqlParameter("@NZY", data.Guid)
            //};
            MySqlDataReader reader;
            try {
                reader = Helper.MySqlHelper.ExecuteReader(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (Exception) {
                throw;
            }
            nzydk.Gddk = new List<GDDK>();
            while (reader.Read()) {
                GDDK gddk = new GDDK();
                gddk.Guid = nzydk.Guid;
                gddk.Dzjgh = reader.GetString("DZJGH");
                gddk.Gdmj = reader.GetDecimal("GDMJ");
                gddk.Xmmc = reader.GetString("YDDW");
                gddk.Dgmj = reader.GetDecimal("DG");
                gddk.Bz = (reader.IsDBNull(6)) ? "" : reader.GetString("BZ");
                gddk.Id = reader.GetInt32("ID");
                nzydk.Gddk.Add(gddk);
            }


            sql = new StringBuilder();
            sql.Append(@"SELECT * FROM info.czfs where GUID='");
            sql.Append(nzydk.Guid);
            sql.Append(@"'");

            try {
                reader = Helper.MySqlHelper.ExecuteReader(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (Exception) {
                throw;
            }
            while (reader.Read()) {
                for (int i = 0; i < nzydk.Czfs.Length; i++) {
                    nzydk.Czfs[i] = reader.GetDecimal((i + 20).ToString());
                }
                nzydk.Sx = reader.GetInt32(34.ToString());
            }


        }

        public static void DeleteGDDK(GDDK gddk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from gdqk where ID =");
            sql.Append(gddk.Id);
            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (MySqlException ex) {
                throw ex;
            }
        }

        public static void UpdateGDDK(GDDK gddk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("update ");
            sql.Append("gdqk");
            sql.Append(" set ");
            sql.Append("DZJGH=@DZJGH,");
            sql.Append("YDDW=@YDDW,");
            sql.Append("GDMJ=@GDMJ,");
            sql.Append("DG=@DG, ");
            sql.Append("BZ=@BZ ");
            sql.Append("where ");
            sql.Append("ID= @ID ");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@ID",gddk.Id),
                new MySqlParameter("@DZJGH",gddk.Dzjgh),
                new MySqlParameter("@YDDW",gddk.Xmmc),
                new MySqlParameter("@GDMJ",gddk.Gdmj),
                new MySqlParameter("@DG",gddk.Dgmj),
                new MySqlParameter("@BZ",gddk.Bz)
                
            };

            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                throw ex;
            }
        }



        public static void UpdateCZFS(NZYDK nzydk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT count(*) from czfs where GUID=@GUID");
            MySqlParameter[] pt = new MySqlParameter[] { new MySqlParameter("@GUID", nzydk.Guid) };
            int exist = Int32.Parse(Helper.MySqlHelper.ExecuteScalar(Method.Conntection(), CommandType.Text, sql.ToString(), pt).ToString());
            if (exist == 0) {
                sql = new StringBuilder();
                sql.Append(@"INSERT INTO `info`.`czfs` (`");
                for (int i = 0; i < nzydk.Czfs.Length; i++) {
                    sql.Append(i + 20);
                    sql.Append("`, `");
                }
                sql.Append(nzydk.Czfs.Length + 20);
                sql.Append("`, `GUID`");
                sql.Append(") VALUES('");
                for (int i = 0; i < nzydk.Czfs.Length; i++) {
                    sql.Append(nzydk.Czfs[i]);
                    sql.Append("', '");
                }
                sql.Append(nzydk.Sx);
                sql.Append("','");
                sql.Append(nzydk.Guid);
                sql.Append("')");
            } else {
                sql = new StringBuilder();
                sql.Append("UPDATE `info`.`czfs` SET ");
                for (int i = 0; i < nzydk.Czfs.Length; i++) {
                    sql.Append("`");
                    sql.Append(i + 20);
                    sql.Append("`= '");
                    sql.Append(nzydk.Czfs[i]);
                    sql.Append("', ");
                }
                sql.Append("`");
                sql.Append(nzydk.Czfs.Length + 20);
                sql.Append("`= '");
                sql.Append(nzydk.Sx);
                sql.Append("' where( ");
                sql.Append("`GUID`= '");
                sql.Append(nzydk.Guid);
                sql.Append("')");

            }

            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (MySqlException ex) {
                throw ex;
            }
        }

        public static void UpdateNzydk(NZYDK nzydk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("update ");
            sql.Append("dkqk");
            sql.Append(" set ");
            sql.Append("BZ=@bz ");
            sql.Append("where ");
            sql.Append("GUID= @GUID ");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@GUID",nzydk.Guid),
                new MySqlParameter("@BZ",nzydk.Bz)
            };

            try {
                Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
            } catch (MySqlException ex) {
                throw ex;
            }
        }

        public static void DataExport(string file) {
            int num = 0;
            List<Data> datas = MySQLRead();
            foreach(Data data in datas) {
                MySQLDKRead(data);
                
                //Thread.Sleep(200);
                foreach(NZYDK nzydk in data.Dk) {
                    MySQLGDRead(nzydk);
                }
                num += data.GetQuery();
            }
            ExcelExport(datas, file);
        }

        private static void ExcelExport(List<Data> datas, string file) {
            ///初始化9张表格
            DataTable[] result = new DataTable[9];
            int[] index_year = new int[9];
            ///初始化1-39列
            foreach (DataTable dt in result) {
                for (int index = 1; index < 40; index++) {
                    dt.Columns.Add(index.ToString());
                }
            }
            for (int i = 2009; i <= 2017; i++) {               
                ///遍历年份
                foreach(Data data in datas) {
                    if (data.Pzrq.Year > 2008) {
                       DataTable dt = result[data.Pzrq.Year - 2009];///对应各年份至格表
                        index_year[data.Pzrq.Year - 2009]++;
                        foreach (NZYDK nzydk in data.Dk) {
                            if (nzydk.Gddk.Count == 0) {
                                dt.Rows.Add(DataRowInitialize(data, nzydk, null, index_year[data.Pzrq.Year - 2009], dt));
                            }
                            foreach (GDDK gddk in nzydk.Gddk) {

                                dt.Rows.Add(DataRowInitialize(data, nzydk, gddk, index_year[data.Pzrq.Year - 2009], dt));

                            }
                        }
                    }
                }

                ExcelHelper excel = new ExcelHelper(file);
                int count = excel.DataTablesToExcel(result[i - 2009], (i + 2009).ToString(), true);

            }
            
        }
        /// <summary>
        /// 生成行
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="index">序号</param>
        /// <param name="dt">表</param>
        /// <returns></returns>
        private static DataRow DataRowInitialize(Data data,NZYDK nzydk,GDDK gddk,int index,DataTable dt) {
            DataRow row = dt.NewRow();//读至供地数据开始写入行
            row[0] = index;
            row[1] = "海宁市";
            row[2] = data.Nzy;
            row[3] = ChangePCMC(data.Nzy);
            row[4] = data.Pzwh;
            row[5] = data.Pzrq.ToString("yyyy/M/d");
            row[6] = nzydk.Dkmc;
            row[7] = nzydk.Dkmj;
            row[8] = "是";
            if (gddk !=null) {
                row[9] = gddk.Dzjgh;
                row[10] = gddk.Xmmc;
                row[11] = gddk.Gdmj;
                row[12] = gddk.Tdyt;
                row[13] = nzydk.SYMJ();
                row[38] = nzydk.Bz + @"|" + gddk.Bz;
            } else {
                row[38] = nzydk.Bz;
            }
            
            for (int i = 0; i < 14; i++) {
                row[i + 19] = nzydk.Czfs[i];
            }
            if (nzydk.Sx!=0) {
                row[32 + nzydk.Sx] = "√";
            }
            
            return row;
               
        }
        /// <summary>
        /// 根据批次名称选择指标类型
        /// </summary>
        /// <param name="pcmc">批次名称</param>
        /// <returns></returns>
        private static string ChangePCMC(string pcmc) {
            string zblx="";//批标类型
            if (pcmc.Contains("批次") ){
                zblx = "计划指标";
            } else if (pcmc.Contains("盘活")) {
                zblx = "盘活指标";
            } else if (pcmc.Contains("增减")) {
                zblx = "增减挂钩指标";
            } else if (pcmc.Contains("整理")) {
                zblx = "折抵指标";
            } else if (pcmc.Contains("复耕")) {
                zblx = "建设用地复垦指标";
            }
            return zblx;
        }
    }
}




