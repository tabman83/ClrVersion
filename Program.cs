using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClrVersion
{
    class Program
    {

        private static readonly Dictionary<int, string> versions45andLaterMap = new Dictionary<int, string>
        {
            { 378389, ".NET Framework 4.5" },
            { 378675, ".NET Framework 4.5.1 installed with Windows 8.1" },
            { 378758, ".NET Framework 4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2" },
            { 379893, ".NET Framework 4.5.2" },
            { 393295, ".NET Framework 4.6 installed with Windows 10" },
            { 393297, ".NET Framework 4.6 installed on all other Windows OS versions" },
            { 394254, ".NET Framework 4.6.1 installed on Windows 10" },
            { 394271, ".NET Framework 4.6.1 installed on all other Windows OS versions" },
            { 394747, ".NET Framework 4.6.2 Preview installed on Windows 10 RS1 Preview" },
            { 394748, ".NET Framework 4.6.2 Preview installed on all other Windows OS versions" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("CLR Version");
            var signature = Get45andLaterVersion();
            Console.WriteLine(signature);
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadKey();
            }
        }

        private static string Get45andLaterVersion()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    int releaseKey = (int)ndpKey.GetValue("Release");
                    if (versions45andLaterMap.ContainsKey(releaseKey))
                    {
                        return versions45andLaterMap[releaseKey];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
