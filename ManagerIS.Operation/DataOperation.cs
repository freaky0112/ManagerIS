using ManagerIS.Common;
using ManagerIS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIS.Operation {
    public abstract class DataOperation {
        public static bool ExcelToMysql(string file,int year) {
            List<Data> datas = DatatableRead(ExcelToData(file, year));
            
            return true;
        }

        private static DataTable  ExcelToData(string file,int year) {
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
                if (!IsExist(result,pzwh) && !String.IsNullOrWhiteSpace(pzwh)) {
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
                    gddk.Dzjgh= dr["供地文号"].ToString();
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
                if (data.Pzwh==pzwh) {
                    return true;
                }
            }
            return false;
        }

        private void DatatToMySQL(List<Data> datas) {

        }
    }
}
