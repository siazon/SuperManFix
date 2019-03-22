using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Super.Website.Core
{
    public class GlobalData
    {
        private static GlobalData _ins;
        public static object obj = new object();
        public static GlobalData Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new GlobalData();
                    }
                }

                return _ins;
            }
        }
        public GlobalData()
        {
            systemConfigs = new List<tb_systemConfig>();
        }
        public  List<tb_systemConfig> systemConfigs ;

    }
}
