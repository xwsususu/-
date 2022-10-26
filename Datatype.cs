using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrecisionControlApp
{
    public class Lockpoint
    {
        private long oid = -1;
        private string pname;
        private int pointrank = -1;
        private int pscope = -1;
        private int worknum = -1;
        private string workstate;
        private int personnum = -1;
        private double x = -1;
        private double y = -1;

        public Lockpoint() { }

        public long OID { get => oid; set => oid = value; }
        public string PNAME { get => pname; set => pname = value; }
        public int POINTRANK { get => pointrank; set => pointrank = value; }
        public int PSCOPE { get => pscope; set => pscope = value; }
        public int WORKNUM { get => worknum; set => worknum = value; }
        public string WORKSTATE { get => workstate; set => workstate = value; }
        public int PERSONNUM { get => personnum; set => personnum = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public object this[int index]
        {
            get
            {
                if (index == 1) return oid;
                else if (index == 2) return pname;
                else if (index == 3) return pointrank;
                else if (index == 4) return pscope;
                else if (index == 5) return worknum;
                else if (index == 6) return workstate;
                else if (index == 7) return personnum;
                else return null;
            }
            set
            {
                if (index == 1) oid = (long)value;
                else if (index == 2) pname = (string)value;
                else if (index == 3) pointrank = (int)value;
                else if (index == 4) pscope = (int)value;
                else if (index == 5) worknum = (int)value;
                else if (index == 6) workstate = (string)value;
                else if (index == 7) personnum = (int)value;
            }
        }
    }

    public class Streetarea
    {
        private long oid;
        private string name;
        private int sum;
        private int polygonrank;
        private DateTime lasttime;
        private DateTime releasetime;
        private DateTime currenttime;

        public Streetarea() { }

        public long OID { get => oid; set => oid = value; }
        public string NAME { get => name; set => name = value; }
        public int SUM { get => sum; set => sum = value; }
        public int POLYGONRANK { get => polygonrank; set => polygonrank = value; }
        public DateTime LASTTIME { get => lasttime; set => lasttime = value; }
        public DateTime RELEASETIME { get => releasetime; set => releasetime = value; }
        public DateTime CURRENTTIME { get => currenttime; set => currenttime = value; }
    }
}
