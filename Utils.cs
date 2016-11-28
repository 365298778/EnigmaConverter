using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestEnigma
{
    class Utils
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
                      StringBuilder retVal, int size, string filePath);

        //读取固定配置文件
        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, System.Environment.CurrentDirectory + @"\setting.ini");
            return String.Format(temp.ToString());
        }
    }
}
