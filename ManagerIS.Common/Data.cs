using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIS.Common {
    public class Data {

        public Data() {
            this.dk = new List<NZYDK>();
            this.guid = Guid.NewGuid();
        }

        public Data(string pzwh) {
            this.pzwh = pzwh;
            this.dk = new List<NZYDK>();
            this.guid = Guid.NewGuid();
        }

        private Guid guid;
        private string nzy;
        private string pzwh;
        private DateTime pzrq;
        private List<NZYDK> dk;
        private Decimal pzmj;

        public Guid Guid { get => guid; set => guid = value; }
        public string Nzy { get => nzy; set => nzy = value; }
        public string Pzwh { get => pzwh; set => pzwh = value; }
        public List<NZYDK> Dk { get => dk; set => dk = value; }
        public DateTime Pzrq { get => pzrq; set => pzrq = value; }
        public decimal Pzmj { get => pzmj; set => pzmj = value; }

        public bool Equals(Data data) {
            return this.pzwh.Equals(data.pzwh);
        }

        public override int GetHashCode() {
            return this.pzwh.GetHashCode();
        }
        ///判断是否为已有地块
        public NZYDK IsExist(string dkmc) {
            NZYDK new_nzydk = new NZYDK(dkmc);
            if (dk.Count<=0) {
                this.dk.Add(new_nzydk);
                return new_nzydk;
            }
            
            foreach (NZYDK nzydk in dk) {
                if (nzydk.Dkmc==dkmc) {
                    return nzydk;
                }
            }
            this.dk.Add(new_nzydk);
            return new_nzydk;
        }
        
        /// <summary>
        /// 面积汇总
        /// </summary>
        /// <returns></returns>
        public Decimal GetArea() {
            Decimal area = 0;
            foreach (NZYDK nzydk in dk) {
                area += nzydk.Dkmj;
            }
            return area;
        }
        
        public void DropDK() {
            dk.Clear();
        }

        public int GetQuery() {
            int num = 0;
            num += dk.Count;
            for (int i = 0; i < dk.Count; i++) {
                num += dk[i].Gddk.Count;
            }
            return num;
        }
        /// <summary>
        /// 批次剩余面积
        /// </summary>
        /// <returns></returns>
        public Decimal GetSYMJ() {
            Decimal area = 0;
            foreach (NZYDK nzydk in dk) {
                area += nzydk.SYMJ();
            }
            return area;
        }

    }

    public class NZYDK {

        private Guid guid;
        private string dkmc;
        private decimal dkmj;
        private List<GDDK> gddk;
        private string bz;

        private decimal[] czfs;
        private int sx;
        
        public NZYDK() {
            this.gddk = new List<GDDK>();
            this.guid = Guid.NewGuid();
            czfs = new decimal[14];
        }

        public NZYDK(string dkmc) {
            this.dkmc = dkmc;
            this.gddk = new List<GDDK>();
            this.guid = Guid.NewGuid();
            czfs = new decimal[14];
        }

        public bool Equals(NZYDK nzydk) {
            return this.dkmc.Equals(nzydk.dkmc);
        }

        public override int GetHashCode() {
            return this.dkmc.GetHashCode();
        }
        public Guid Guid { get => guid; set => guid = value; }
        public string Dkmc { get => dkmc; set => dkmc = value; }
        public decimal Dkmj { get => dkmj; set => dkmj = value; }
        public List<GDDK> Gddk { get => gddk; set => gddk = value; }
        public string Bz { get => bz; set => bz = value; }
        /// <summary>
        /// 时限
        /// </summary>
        public int Sx { get => sx; set => sx = value; }
        /// <summary>
        /// 处置方式
        /// </summary>
        public decimal[] Czfs { get => czfs; set => czfs = value; }
        /// <summary>
        /// 地块剩余面积
        /// </summary>
        /// <returns></returns>
        public Decimal SYMJ() {

            return dkmj - GetYGMJ() ;
        }

        public bool Check() {
            try {
                if (dkmj - GetYGMJ() == GetWGYY() && GetWGYY() == GetCZFS()) {

                } else {
                    throw new Exception("未供面积与处理面积不一致");
                }
                if (dkmj - GetYGMJ()>0&&sx==0) {
                    throw new Exception("未填写处理时间");
                } 
                
            } catch (Exception e) {

                throw e;
            }
            return true;
        }
        /// <summary>
        /// 已供面积
        /// </summary>
        /// <returns></returns>
        public Decimal GetYGMJ() {
            Decimal area=0;
            foreach (GDDK gd in gddk) {
                area += gd.Gdmj;
            }
            return area;
        }

        public Decimal GetWGYY() {
            Decimal area = 0;
            for (int i = 0; i < 8; i++) {
                area += czfs[i];
            }
            return area;
        }

        public Decimal GetCZFS() {
            Decimal area = 0;
            for (int i = 8; i < 14; i++) {
                area += czfs[i];
            }
            return area;
        }

    }

    public class GDDK {

        private Guid guid;
        private string dzjgh;
        private string xmmc;
        private decimal gdmj;
        private decimal dgmj;
        private string bz;
        private int id;
        private string tdyt;
        private string tdzl;

        public GDDK() {
            this.guid = Guid.NewGuid();
        }
        /// <summary>
        /// 农转用地块GUID
        /// </summary>
        public Guid Guid { get => guid; set => guid = value; }
        /// <summary>
        /// 电子监管号
        /// </summary>
        public string Dzjgh { get => dzjgh; set => dzjgh = value; }
        /// <summary>
        /// 用地单位 
        /// </summary>
        public string Xmmc { get => xmmc; set => xmmc = value; }
        /// <summary>
        /// 供地面积
        /// </summary>
        public decimal Gdmj { get => gdmj; set => gdmj = value; }
        /// <summary>
        /// 带供面积
        /// </summary>
        public decimal Dgmj { get => dgmj; set => dgmj = value; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Bz { get => bz; set => bz = value; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// 土地用途
        /// </summary>
        public string Tdyt { get => tdyt; set => tdyt = value; }
        public string Tdzl { get => tdzl; set => tdzl = value; }
    }
}
