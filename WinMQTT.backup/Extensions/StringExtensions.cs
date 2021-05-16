using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMQTT.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveMachineName(this string value)
        {
            var split = value.Split('/');
            return split.Count() != 3 ? value : $"{split[0]}/{split[2]}";
        }
    }
}