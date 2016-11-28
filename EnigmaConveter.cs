using System;
using System.Collections;
using System.Collections.Generic;

namespace TestEnigma
{
    internal class EnigmaConveter
    {
        #region 恩格玛密码加密

        public static string ENCRYPT(string context, string rotor1, string rotor2, string rotor3, Dictionary<char, char> patchBoards)
        {
            context = context.ToLower();
            context = context.Replace(" ", "");
            context = patchBoardsReplace(context, patchBoards);
            rotor1 = rotor1.ToLower();
            rotor1 = rotor1.Replace(" ", "");
            rotor2 = rotor2.ToLower();
            rotor2 = rotor2.Replace(" ", "");
            rotor3 = rotor3.ToLower();
            rotor3 = rotor3.Replace(" ", "");
            int curCount = 0;
            string result = string.Empty;
            string referenceTable = "abcdefghijklmnopqrstuvwxyz0123456789";
            foreach (char info in context)
            {
                Dictionary<char, char> mapTab1 = makeTrans(referenceTable, rotor1);
                Dictionary<char, char> mapTab2 = makeTrans(referenceTable, rotor2);
                Dictionary<char, char> mapTab3 = makeTrans(referenceTable, rotor3);
                char cipherChar = translate(info, mapTab1);
                cipherChar = translate(cipherChar, mapTab2);
                cipherChar = translate(cipherChar, mapTab3);
                cipherChar = reverse_word(cipherChar);
                Dictionary<char, char> revMapTab1 = makeTrans(rotor1, referenceTable);
                Dictionary<char, char> revMapTab2 = makeTrans(rotor2, referenceTable);
                Dictionary<char, char> revMapTab3 = makeTrans(rotor3, referenceTable);
                cipherChar = translate(info, revMapTab3);
                cipherChar = translate(info, revMapTab2);
                cipherChar = translate(info, revMapTab1);
                result += cipherChar;
                rotor1 = rotorsRotation(rotor1);
                curCount++;
                if (curCount % 36 == 0)
                {
                    rotor2 = rotorsRotation(rotor2);
                }
                else if (curCount % 1296 == 0)
                {
                    rotor3 = rotorsRotation(rotor3);
                }
            }
            return result;
        }

        #endregion 恩格玛密码加密

        #region 恩格玛密码解密

        public static string DECRYPT(string ciphertext, string rotor1, string rotor2, string rotor3, Dictionary<char, char> patchBoards)
        {
            ciphertext = ciphertext.ToLower();
            ciphertext = ciphertext.Replace(" ", "");
            rotor1 = rotor1.ToLower();
            rotor1 = rotor1.Replace(" ", "");
            rotor2 = rotor2.ToLower();
            rotor2 = rotor2.Replace(" ", "");
            rotor3 = rotor3.ToLower();
            rotor3 = rotor3.Replace(" ", "");
            int curCount = 0;
            string result = string.Empty;
            string referenceTable = "abcdefghijklmnopqrstuvwxyz0123456789";
            foreach (char info in ciphertext)
            {
                Dictionary<char, char> mapTab1 = makeTrans(rotor1, referenceTable);
                Dictionary<char, char> mapTab2 = makeTrans(rotor2, referenceTable);
                Dictionary<char, char> mapTab3 = makeTrans(rotor3, referenceTable);
                char cipherChar = translate(info, mapTab1);
                cipherChar = translate(cipherChar, mapTab2);
                cipherChar = translate(cipherChar, mapTab3);
                cipherChar = reverse_word(cipherChar);
                Dictionary<char, char> revMapTab1 = makeTrans(referenceTable, rotor1);
                Dictionary<char, char> revMapTab2 = makeTrans(referenceTable, rotor2);
                Dictionary<char, char> revMapTab3 = makeTrans(referenceTable, rotor3);
                cipherChar = translate(info, revMapTab3);
                cipherChar = translate(info, revMapTab2);
                cipherChar = translate(info, revMapTab1);
                result += cipherChar;
                rotor1 = rotorsRotation(rotor1);
                curCount++;
                if (curCount % 36 == 0)
                {
                    rotor2 = rotorsRotation(rotor2);
                }
                else if (curCount % 1296 == 0)
                {
                    rotor3 = rotorsRotation(rotor3);
                }
                result = patchBoardsReplace(result, patchBoards);
            }
            return result;
        }

        #endregion 恩格玛密码解密

        #region 接线盘替换加密
        public static string patchBoardsReplace(string info, Dictionary<char, char> patchBoards)
        {
            if (patchBoards.Count > 0)
            {
                Dictionary<char, char> patchBoardsDec = new Dictionary<char, char>();
                foreach (var item in patchBoards)
                {
                    patchBoardsDec.Add(item.Value, item.Key);
                }
                foreach (var item in patchBoards)
                {
                    if (info.IndexOf(item.Key)!=-1 && info.IndexOf(item.Value)!=-1)
                    {
                        ArrayList keyArr = new ArrayList();
                        ArrayList valueArr = new ArrayList();
                        for (int i = 0; i < info.Length; i++)
                        {
                            if (info[i].Equals(item.Key))
                            {
                                keyArr.Add(i);
                            }
                            if (info[i].Equals(item.Value))
                            {
                                valueArr.Add(i);
                            }
                        }
                        foreach (int index in keyArr)
                        {
                            char[] temp=info.ToCharArray();
                            temp[index] = item.Value;
                            info = temp.ToString();
                        }
                        foreach (int index in valueArr)
                        {
                            char[] temp = info.ToCharArray();
                            temp[index] = item.Key;
                            info = temp.ToString();
                        }
                    }
                    else
                    {
                        info = info.Replace(item.Key, item.Value);
                    }
                }
            }
            return info;
        }
        #endregion

        #region 创建转子映射表

        public static Dictionary<char, char> makeTrans(string inTab, string outTab)
        {
            Dictionary<char, char> dic = new Dictionary<char, char>();
            for (int i = 0; i < inTab.Length; i++)
            {
                dic.Add(inTab[i], outTab[i]);
            }
            return dic;
        }

        #endregion 创建转子映射表

        #region 替换转换

        public static char translate(char info, Dictionary<char, char> reference)
        {
            return reference[info];
        }

        #endregion 替换转换

        #region 自反转换

        public static char reverse_word(char letter)
        {
            string keyArr = Utils.IniReadValue("ENIGMA", "REVERSEKEY");
            string valueArr = Utils.IniReadValue("ENIGMA", "REVERSEVALUE");
            Dictionary<char, char> dic = new Dictionary<char, char>();
            for (int i = 0; i < keyArr.Length; i++)
            {
                dic.Add(keyArr[i], valueArr[i]);
            }
            return dic[letter];
        }

        #endregion 自反转换

        #region 转子转动

        public static string rotorsRotation(string rotors)
        {
            rotors += rotors[0];
            rotors = rotors.Substring(1);
            return rotors;
        }

        #endregion 转子转动
    }
}