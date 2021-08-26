using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.DTO
{
    class InternetConnection
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        private static InternetConnection _Instance;
        public static InternetConnection Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InternetConnection();
                }
                return _Instance;
            }
            private set { }
        }
        public bool isConnected()
        {
            int o;
            return InternetGetConnectedState(out o, 0);
        }
        private InternetConnection()
        {

        }
    }
}
