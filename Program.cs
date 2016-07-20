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

        static void Main(string[] args)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string header = string.Format("{0} by {1} (v.{2})", versionInfo.ProductName, versionInfo.CompanyName, versionInfo.ProductVersion);
            Console.WriteLine(header);
            Console.WriteLine();

            var signature = Get45andLaterVersion();
            if (signature == null)
            {
                Console.WriteLine("Detected CLR is: " + Environment.Version);
            }
            else
            {
                Console.WriteLine(signature);
            }
            Console.WriteLine();
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
                    string result = string.Empty;

                    if (releaseKey >= 394748)
                    {
                        result = ".NET Framework 4.6.2 Preview installed on all other Windows OS versions";
                    }
                    else if (releaseKey >= 394747)
                    {
                        result = ".NET Framework 4.6.2 Preview installed on Windows 10 RS1 Preview";
                    }
                    else if (releaseKey >= 394271)
                    {
                        result = ".NET Framework 4.6.1 installed on all other Windows OS versions";
                    }
                    else if (releaseKey >= 394254)
                    {
                        result = ".NET Framework 4.6.1 installed on Windows 10";
                    }
                    else if (releaseKey >= 393297)
                    {
                        result = ".NET Framework 4.6 installed on all other Windows OS versions";
                    }
                    else if (releaseKey >= 393295)
                    {
                        result = ".NET Framework 4.6 installed with Windows 10";
                    }
                    else if (releaseKey >= 379893)
                    {
                        result = ".NET Framework 4.5.2";
                    }
                    else if (releaseKey >= 378758)
                    {
                        result = ".NET Framework 4.5.1 installed on Windows 8, Windows 7 SP1, or Windows Vista SP2";
                    }
                    else if (releaseKey >= 378675)
                    {
                        result = ".NET Framework 4.5.1 installed with Windows 8.1";
                    }
                    else if (releaseKey >= 378389)
                    {
                        result = ".NET Framework 4.5";
                    }
                    else
                    {
                        return null;
                    }

                    return "Detected CLR is: " + result + " (or later)";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
