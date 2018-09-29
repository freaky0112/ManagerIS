using ManagerIS.Common;
using ManagerIS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
                    NzydkToMySQL(data.Guid,nzydk);
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
            sql.Append("DKMJ");
            sql.Append(") values (");
            sql.Append("@GUID,");
            sql.Append("@NZY,");
            sql.Append("@DKMC,");
            sql.Append("@DKMJ)");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("GUID",nzydk.Guid),
                new MySqlParameter("NZY",guid),
                new MySqlParameter("DKMC",nzydk.Dkmc),
                new MySqlParameter("DKMJ",nzydk.Dkmj),
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
        private static void GddkToMySQL(Guid guid, GDDK gddk) {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into gdqk (");
            sql.Append("DKGUID,");
            sql.Append("DZJGH,");
            sql.Append("YDDW,");
            sql.Append("GDMJ,");
            sql.Append("DG");
            sql.Append(") values (");
            sql.Append("@DKGUID,");
            sql.Append("@DZJGH,");
            sql.Append("@YDDW,");
            sql.Append("@GDMJ,");
            sql.Append("@DG)");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@DKGUID",guid),
                new MySqlParameter("@DZJGH",gddk.Dzjgh),
                new MySqlParameter("@YDDW",gddk.Xmmc),
                new MySqlParameter("@GDMJ",gddk.Gdmj),
                new MySqlParameter("@DG",gddk.Dgmj)
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
            } catch (Exception) {
                throw;
            }
            data.Dk = new List<NZYDK>();
            while (reader.Read()) {
                
                NZYDK nzydk = new NZYDK();
                nzydk.Dkmc = reader.GetString("DKMC");
                nzydk.Guid = reader.GetGuid("GUID");
                nzydk.Dkmj = reader.GetDecimal("DKMJ");
                
                data.Dk.Add(nzydk);
                
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
                nzydk.Gddk.Add(gddk);
            }

        }
    }



}
