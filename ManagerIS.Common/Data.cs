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

        public Guid Guid { get => guid; }
        public string Nzy { get => nzy; set => nzy = value; }
        public string Pzwh { get => pzwh; set => pzwh = value; }
        public List<NZYDK> Dk { get => dk; set => dk = value; }
        public DateTime Pzrq { get => pzrq; set => pzrq = value; }

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

    }

    public class NZYDK {

        private Guid guid;
        private string dkmc;
        private decimal dkmj;
        private List<GDDK> gddk;
        private string bz;


        
        public NZYDK() {
            this.gddk = new List<GDDK>();
            this.guid = Guid.NewGuid();
        }

        public NZYDK(string dkmc) {
            this.dkmc = dkmc;
            this.gddk = new List<GDDK>();
            this.guid = Guid.NewGuid();
        }

        public bool Equals(NZYDK nzydk) {
            return this.dkmc.Equals(nzydk.dkmc);
        }

        public override int GetHashCode() {
            return this.dkmc.GetHashCode();
        }
public Guid Guid { get => guid; }
        public string Dkmc { get => dkmc; set => dkmc = value; }
        public decimal Dkmj { get => dkmj; set => dkmj = value; }
        public List<GDDK> Gddk { get => gddk; set => gddk = value; }
        public string Bz { get => bz; set => bz = value; }
    }

    public class GDDK {

        private Guid guid;
        private string dzjgh;
        private string xmmc;
        private decimal gdmj;
        private decimal dgmj;
        private string bz;

        public GDDK() {
            this.guid = Guid.NewGuid();
        }

        public Guid Guid { get => guid; }
        public string Dzjgh { get => dzjgh; set => dzjgh = value; }
        public string Xmmc { get => xmmc; set => xmmc = value; }
        public decimal Gdmj { get => gdmj; set => gdmj = value; }
        public decimal Dgmj { get => dgmj; set => dgmj = value; }
        public string Bz { get => bz; set => bz = value; }
    }
}
