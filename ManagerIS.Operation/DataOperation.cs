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
using System.Collections;
using System.IO;

namespace ManagerIS.Operation {
    public abstract partial class DataOperation {
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
            sql.Append("BZ, ");
            sql.Append("TDZL, " );
            sql.Append("TDYT ");
            sql.Append(") values (");
            sql.Append("@DKGUID,");
            sql.Append("@DZJGH,");
            sql.Append("@YDDW,");
            sql.Append("@GDMJ,");
            sql.Append("@DG,");
            sql.Append("@BZ,");
            sql.Append("@TDZL, ");
            sql.Append("@TDYT )");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@DKGUID",guid),
                new MySqlParameter("@DZJGH",gddk.Dzjgh),
                new MySqlParameter("@YDDW",gddk.Xmmc),
                new MySqlParameter("@GDMJ",gddk.Gdmj),
                new MySqlParameter("@DG",gddk.Dgmj),
                new MySqlParameter("@BZ",gddk.Bz),
                new MySqlParameter("@TDZL",gddk.Tdzl),
                new MySqlParameter("@TDYT",gddk.Tdyt)

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
                data.Pzmj = reader.GetDecimal("PZMJ");
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
                gddk.Tdyt = (reader.IsDBNull(4)) ? "":reader.GetString("TDYT");
                gddk.Tdzl = (reader.IsDBNull(8)) ? "":reader.GetString("TDZL");
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
            sql.Append("BZ=@BZ, ");
            sql.Append("TDYT=@TDYT, ");
            sql.Append("TDZL=@TDZL ");
            sql.Append("where ");
            sql.Append("ID= @ID ");
            MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@ID",gddk.Id),
                new MySqlParameter("@DZJGH",gddk.Dzjgh),
                new MySqlParameter("@YDDW",gddk.Xmmc),
                new MySqlParameter("@GDMJ",gddk.Gdmj),
                new MySqlParameter("@DG",gddk.Dgmj),
                new MySqlParameter("@BZ",gddk.Bz),
                new MySqlParameter("@TDZL",gddk.Tdzl),
                new MySqlParameter("@TDYT",gddk.Tdyt)

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
            List<Data> datas = MySQLViewRead();

            ExcelExport(datas, file);
            ExcelPHExport(datas, file);//导出盘活
        }


        private static List<Data> MySQLViewRead() {
            //Hashtable datas = new Hashtable();
            List<Data> datas = new List<Data>();
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM info.nzy_view;");
            MySqlDataReader reader;
            try {
                reader = Helper.MySqlHelper.ExecuteReader(Method.Conntection(), CommandType.Text, sql.ToString(), null);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            while (reader.Read()) {
                ///初始化DATA
                Data data = new Data();
                ///读取批次GUID
                Guid guid = reader.GetGuid("GUID");
                bool unExistData = true;//判断是是否存在该批次

                foreach (Data origin in datas) {
                    if (origin.Guid == guid) {
                        data = origin;
                        unExistData = false;
                        break;
                    }
                }



                if (unExistData) {//如果该批次存在
                    data.Guid = guid;
                    data.Nzy = reader.GetString("PCMC");//批次名称
                    data.Pzwh = reader.GetString("PZWH");//批准文号
                    data.Pzrq = reader.GetDateTime("PZRQ");//批准日期
                    data.Pzmj = reader.GetDecimal("PZMJ");//批准面积
                    datas.Add(data);
                }


                string dkmc = reader.GetString("DKMC");//地块名称
                //NZYDK nzydk = data.IsExist(dkmc);//是否已存在该地块
                NZYDK nzydk = new NZYDK();
                bool unExist = true;//判断是否已存在该 农转用地块
                foreach (NZYDK new_nzydk in data.Dk) {
                    if (new_nzydk.Dkmc == dkmc) {
                        nzydk = new_nzydk;
                        unExist = false;
                    }
                }
                if (unExist) {//如果不存在
                    nzydk.Guid = reader.GetGuid("DKGUID");//农转用地块GUID
                    nzydk.Dkmc = reader.GetString("DKMC");//地块名称
                    nzydk.Dkmj = reader.GetDecimal("DKMJ");//地块面积
                    ///读取处置方式内容20-34列
                    for (int i = 20; i < 34; i++) {
                        nzydk.Czfs[i - 20] = (reader.IsDBNull(i - 4)) ? 0 : reader.GetDecimal(i.ToString());
                    }
                    nzydk.Sx = (reader.IsDBNull(30)) ? 0 : reader.GetInt32("34");
                    data.Dk.Add(nzydk);
                }
                string yddw = (reader.IsDBNull(11)) ? "" : reader.GetString("YDDW");//获取用地单位，如果不存在则为空
                if (!string.IsNullOrEmpty(yddw)) {//始果用地单位存在
                    GDDK gddk = new GDDK();
                    gddk.Xmmc = yddw;
                    gddk.Dzjgh = (reader.IsDBNull(10)) ? "" : reader.GetString("DZJGH");//获取电子监管号，如果不存在则为空
                    gddk.Gdmj = reader.GetDecimal("GDMJ");//获取供地面积
                    gddk.Dgmj = reader.GetDecimal("DG");//获取带供面积
                    gddk.Tdzl = (reader.IsDBNull(31)) ? "" : reader.GetString("TDZL");//土地坐落
                    gddk.Tdyt = (reader.IsDBNull(13)) ? "" : reader.GetString("TDYT");//获取电子监管号，如果不存在则为空
                    gddk.Bz = ((reader.IsDBNull(8)) ? "" : reader.GetString("DKBZ"))
                        + "|" +
                        ((reader.IsDBNull(15)) ? "" : reader.GetString("BZ"));//获取备注，如果不存在则为空
                    nzydk.Gddk.Add(gddk);
                }


            }


            //List<Data> result = new List<Data>();
            //foreach (Data data in datas.Values) {
            //    result.Add(data);
            //}

            return datas;
        }



        /// <summary>
        /// 输出EXCEL
        /// </summary>
        /// <param name="datas">数据</param>
        /// <param name="file">保存文件名</param>
        private static void ExcelExport(List<Data> datas, string file) {
            ///初始化9张表格
            DataTable[] result = new DataTable[10];
            int[] index_year = new int[10];
            ///初始化9张表
            for (int i = 0; i < 10; i++) {
                result[i] = new DataTable();
                ///初始化1-39列
                for (int index = 1; index < 40; index++) {
                    result[i].Columns.Add(index.ToString());
                }
                index_year[i] = 0;
            }
            //foreach (DataTable dt in result) {
            //    dt = new DataTable();

            //}
            DataTable dt = new DataTable();
            DataTable dt_result = result[9];
            string pcmc = "";
            foreach (Data data in datas) {

                //if (data.Nzy.Contains("盘活")) {


                for (int i = 2009; i <= 2017; i++) {
                    ///遍历年份

                    if (data.Pzrq.Year > 2008) {
                        dt = result[data.Pzrq.Year - 2009];///对应各年份至格表
                        if (data.Nzy == pcmc) {
                            index_year[data.Pzrq.Year - 2009]++;

                        } else {
                            pcmc = data.Nzy;
                        }

                        index_year[9]++;
                    }
                }

                foreach (NZYDK nzydk in data.Dk) {
                    if (nzydk.Gddk.Count == 0) {
                        dt.Rows.Add(DataRowInitialize(data, nzydk, null, index_year[data.Pzrq.Year - 2009], dt));
                        dt_result.Rows.Add(DataRowInitialize(data, nzydk, null, index_year[9], dt_result));//导入汇总表
                    }
                    foreach (GDDK gddk in nzydk.Gddk) {

                        dt.Rows.Add(DataRowInitialize(data, nzydk, gddk, index_year[data.Pzrq.Year - 2009], dt));
                        dt_result.Rows.Add(DataRowInitialize(data, nzydk, gddk, index_year[9], dt_result));//导入汇总表

                    }

                }
                //}
            }
            if (File.Exists(file + "导出.xlsx")) {
                File.Delete(file + "导出.xlsx");
            }


            ExcelHelper excel = new ExcelHelper(file + "导出.xlsx");
            int count = excel.DataTableToExcel(dt_result, "汇总", true);
            excel.Dispose();


            for (int i = 0; i < 9; i++) {
                excel = new ExcelHelper(file + (i + 2009) + ".xlsx");
                count = excel.DataTableToExcel(result[i], (i + 2009).ToString(), true);
            }

        }
        /// <summary>
        /// 生成行
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="index">序号</param>
        /// <param name="dt">表</param>
        /// <returns></returns>
        private static DataRow DataRowInitialize(Data data, NZYDK nzydk, GDDK gddk, int index, DataTable dt) {
            DataRow row = dt.NewRow();//读至供地数据开始写入行
            row[0] = index;
            row[1] = "海宁市";
            row[2] = data.Nzy;
            row[3] = ChangePCMC(data.Nzy);
            row[4] = data.Pzwh;
            row[5] = data.Pzrq.ToString("yyyy/M/d");
            row[6] = nzydk.Dkmc;
            row[7] = nzydk.Dkmj * 15;
            row[8] = "是";
            if (gddk != null) {
                row[9] = gddk.Dzjgh;
                row[10] = gddk.Xmmc;
                row[11] = gddk.Gdmj * 15;
                row[12] = gddk.Tdyt;

                row[13] = data.GetSYMJ() * 15;//批次剩余面积
                row[38] = nzydk.Bz + @"|" + gddk.Bz;
            } else {
                row[13] = data.GetSYMJ() * 15;//批次剩余面积
                row[38] = nzydk.Bz;
            }

            for (int i = 0; i < 14; i++) {
                row[i + 19] = nzydk.Czfs[i] * 15;
            }
            if (nzydk.Sx != 0) {
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
            string zblx = "";//批标类型
            if (pcmc.Contains("计划")) {
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



        public static void DataCheck(string file) {
            List<Data> datas = MySQLViewRead();
            DataTable dt = new DataTable();
            DataTable dt_pc = new DataTable();
            dt_pc.Columns.Add("批次名");
            dt_pc.Columns.Add("批准面积");
            dt_pc.Columns.Add("当前面积");
            dt_pc.Columns.Add("批准时间");
            dt.Columns.Add("批次名");
            dt.Columns.Add("批次面积");
            dt.Columns.Add("已供面积");
            dt.Columns.Add("剩余面积");
            foreach (Data data in datas) {
                //if (data.Pzmj != data.GetArea()) {
                DataRow dataRow = dt_pc.NewRow();
                dataRow[0] = data.Nzy;
                dataRow[1] = ChangePCMC(data.Nzy);
                //dataRow[1] = data.Pzmj;
                dataRow[2] = data.GetArea();
                dataRow[3] = data.Pzrq.ToLongDateString();
                dt_pc.Rows.Add(dataRow);

                //}

                Decimal area = 0;
                foreach (NZYDK nzydk in data.Dk) {
                    //if (nzydk.Dkmj - nzydk.GetLeftArea() != nzydk.GetWGYY()) {
                    //    DataRow dr = dt.NewRow();
                    //    dr[0] = data.Nzy;
                    //    dr[1] = nzydk.Dkmc;
                    //    dr[2] = nzydk.Dkmj - nzydk.GetLeftArea();
                    //    dt.Rows.Add(dr);
                    //}
                    foreach (GDDK gddk in nzydk.Gddk) {
                        area += gddk.Gdmj;
                    }
                    //bool check = false;
                    //if (nzydk.GetWGYY() != nzydk.SYMJ() || nzydk.SYMJ() != nzydk.GetCZFS()||nzydk.GetCZFS()!=nzydk.GetWGYY()) {
                    //    DataRow dr = dt.NewRow();
                    //    dr[0] = data.Nzy;
                    //    dr[1] = nzydk.Dkmc;
                    //    dr[2] = nzydk.Dkmj;
                    //    dt.Rows.Add(dr);
                    //}

                }

                //if (data.GetArea() - area != data.GetSYMJ()) {
                DataRow dr = dt.NewRow();
                dr[0] = data.Nzy;
                dr[1] = data.GetArea();
                dr[2] = area * 15;
                dr[3] = data.GetSYMJ() * 15;
                dt.Rows.Add(dr);
                //}


                //if (nzydk.GetLeftArea() == 0) {
                //        DataRow dr = dt.NewRow();
                //        dr[0] = data.Nzy;
                //        dr[1] = nzydk.Dkmc;
                //        dr[2] = nzydk.Dkmj;
                //        dt.Rows.Add(dr);
                //    }


                //}


            }
            ExcelHelper excel = new ExcelHelper(file + "检查.xlsx");
            int count = excel.DataTableToExcel(dt, "检查", true);
            excel = new ExcelHelper(file + "自查.xlsx");
            count = excel.DataTableToExcel(dt_pc, "自查", true);

        }


        public static void ImportDZJGH(string file) {
            ExcelHelper excelHelper = new ExcelHelper(file);
            DataTable dt = excelHelper.ExcelToDataTable("SQL Results", true);
            for (int i = 0; i < dt.Rows.Count; i++) {
                DataRow dr = dt.Rows[i];
                string dzjgh = dr[4].ToString();
                string tdyt = dr[5].ToString();
                string tdzl = dr[6].ToString();
                StringBuilder sql = new StringBuilder();
                sql.Append("update ");
                sql.Append("gdqk");
                sql.Append(" set ");
                sql.Append("TDYT=@TDYT, ");
                sql.Append("TDZL=@TDZL ");
                sql.Append("where ");
                sql.Append("DZJGH= @DZJGH ");
                MySqlParameter[] pt = new MySqlParameter[] {
                new MySqlParameter("@DZJGH",dzjgh),
                new MySqlParameter("@TDYT",tdyt),
                new MySqlParameter("@tdzl",tdzl)

            };

                try {
                    Helper.MySqlHelper.ExecuteNonQuery(Method.Conntection(), CommandType.Text, sql.ToString(), pt);
                } catch (MySqlException ex) {
                    throw ex;
                }

            }
        }
        /// <summary>
        /// 盘活导出
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="file"></param>
        private static void ExcelPHExport(List<Data> datas, string file) {
            

            DataTable dt = new DataTable();
            for (int i = 0; i < 17; i++) {
                dt.Columns.Add(i.ToString());
            }
            int index = 0;
            foreach (Data data in datas) {
                index++;
                if (data.Nzy.Contains("盘活")) {
                    foreach (NZYDK nzydk in data.Dk) {
                        if (nzydk.Gddk.Count == 0) {
                            dt.Rows.Add(DataRowPHInitialize(data, nzydk, null, index, dt));
                        }
                        foreach (GDDK gddk in nzydk.Gddk) {
                            dt.Rows.Add(DataRowPHInitialize(data, nzydk, gddk, index, dt));
                        }

                    }

                }
                
            }
            if (File.Exists(file + "导出盘活.xlsx")) {
                File.Delete(file + "导出盘活.xlsx");
            }


            ExcelHelper excel = new ExcelHelper(file + "导出盘活.xlsx");
            int count = excel.DataTableToExcel(dt, "汇总", true);
            excel.Dispose();
        }

        /// <summary>
        /// 盘活批次导出生成行
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="index">序号</param>
        /// <param name="dt">表</param>
        /// <returns></returns>
        private static DataRow DataRowPHInitialize(Data data, NZYDK nzydk, GDDK gddk, int index, DataTable dt) {
            DataRow row = dt.NewRow();//读至供地数据开始写入行
            row[0] = index;
            row[1] = "海宁市";
            row[3] = data.GetArea()*15;
            row[4] = data.Nzy;
            row[5] = nzydk.Dkmc;
            row[6] = "是";
            row[7] = nzydk.Dkmj*15;
            row[10] = nzydk.SYMJ()*15;//地块剩面积
            if (gddk != null) {
                row[11] = gddk.Dzjgh;
                row[12] = gddk.Xmmc;
                row[13] = gddk.Gdmj * 15;
                row[15] = gddk.Tdyt;
                row[14] = gddk.Tdzl;
                
            } else {
            }
            return row;

        }
    }
}




