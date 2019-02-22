using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo
{
    static class Extension
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static string ExtendPath(this string arg, string arg2)
        {
            string path = arg.Substring(0, arg.IndexOf("."));
            string extension = arg.Substring(arg.IndexOf("."));

            return path + arg2 + extension;
        }
    }
}
