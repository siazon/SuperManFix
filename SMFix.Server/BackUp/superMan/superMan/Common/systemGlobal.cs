using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superMan
{
   public class systemGlobal
    {
        public systemGlobal()
        {

        }
        private static systemGlobal _ins;
        public static object Lock = new object();
        public static systemGlobal Ins
        {
            get {
                lock (Lock)
                {
                    if (_ins==null)
                    {
                        _ins = new systemGlobal();
                    }
                }
                return _ins; }
        }
        public MainWindow MainWin { get; set; }
    }
}
