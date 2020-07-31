using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMQTT
{
    public class OptionsModel
    {
        public string Server { get; set; }
        public int Port { get; set; } = 1883;
        public string Username { get; set; }
        public string Password { get; set; }
    }
}