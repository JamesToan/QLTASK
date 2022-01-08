using System;

using System.Text.RegularExpressions;

namespace coreWeb.Helpers
{
    public class Util
    {
        public static bool IsPhoneNumber(string PhoneNumber)
        {
            return Regex.Match(PhoneNumber, @"^(032|033|034|035|036|037|038|039|056|058|059|070|076|077|078|079|081|082|083|084|085|086|088|089|090|091|092|093|094|096|097|098|099)[0-9]{7}$").Success;
        }
        public static bool IsAccount(string Account)
        {
            return Regex.Match(Account, @"^[a-zA-Z0-9._-]{6,25}$").Success;
        }

        public static string EncodeString(string input)
        {
            CryptLib _crypt = new CryptLib();
            string plainText = input;
            String iv = "Vnpt2LongAn3HsKD";//CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
            string key = CryptLib.getHashSha256("hskd1234la", 32); //32 bytes = 256 bits
            String cypherText = _crypt.encrypt(plainText, key, iv);
            return cypherText;
        }

        public static string DecodeString(string input)
        {
            CryptLib _crypt = new CryptLib();
            String iv = "Vnpt2LongAn3HsKD";//CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
            string key = CryptLib.getHashSha256("hskd1234la", 32); //32 bytes = 256 bits
            String decode = _crypt.decrypt(input, key, iv);
            return decode;
        }
    }
}
