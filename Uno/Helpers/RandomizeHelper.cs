using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Uno.Helpers
{
    public static class RandomizeHelper
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> liste)
        {
            var provider = new RNGCryptoServiceProvider();
            var list = liste.ToList();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            
            return list;
        }
    }
}