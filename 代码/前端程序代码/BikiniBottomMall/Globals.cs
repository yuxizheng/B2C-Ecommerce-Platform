using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikiniBottomMall
{
    public static class Globals
    {
        public static String UserID = "blank"; // Modifiable
        public static String UserPwd = "blank";
        public static String UserName = "blank";
        public static String BusinessID = "blank";
        public static String BusinessPwd = "blank";
        public static String saID = "sa";
        public static String saPwd = "00000000";
        public static String ProuductID = "blank";
        public static string CommentID;
        public static string OrderID;
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    
}
