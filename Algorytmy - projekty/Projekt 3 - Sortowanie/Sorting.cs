using System;
using System.Text;

namespace SortingMethods
{
    public static class Sorting
    {
        public static bool IsGreaterThan<T>(T x, T y) where T: IComparable
        {
            return x.CompareTo(y) > 0;
        }

        public static bool IsLowerThan<T>(T x, T y) where T: IComparable
        {
            return x.CompareTo(y) < 0;
        }

        public static bool AreEqual<T>(T x, T y) where T: IComparable
        {
            return x.CompareTo(y) == 0;
        }

        public static bool IsGreaterEqual<T>(T x, T y) where T: IComparable
        {
            return x.CompareTo(y) >= 0;
        }

        public static int InsertionSort<T>(T[] t) where T : IComparable
        {           
            for (uint i = 1; i < t.Length; i++)
            {
                uint j = i;
                T Buf = t[j];

                while ((j > 0) && (IsGreaterThan(t[j - 1], Buf)))
                {
                    t[j] = t[j - 1];
                    j--;
                }

                t[j] = Buf;
            }

            return 1;
        }

        public static int SelectionSort<T>(T[] t) where T : IComparable
        {
            uint k;

            for (uint i = 0; i < (t.Length - 1); i++)
            {
                var Buf = t[i];
                k = i;
                for (uint j = i + 1; j < t.Length; j++)
                    if (IsLowerThan(t[j], Buf))
                    {
                        k = j;
                        Buf = t[j];
                    }

                t[k] = t[i];
                t[i] = Buf;
            }
            
            return 1;
        }

        public static int CocktailSort<T>(T[] t) where T : IComparable
        {
            int Left = 1, Right = t.Length - 1, k = t.Length - 1;
            do
            {
                for (int j = Right; j >= Left; j--)
                    if (IsGreaterThan(t[j - 1], t[j]))
                    {
                        var Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Left = k + 1;
                for (int j = Left; j <= Right; j++)
                    if (IsGreaterThan(t[j - 1], t[j]))
                    {
                        var Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Right = k - 1;
            }
            while (Left <= Right);

            return 1;
        }

        public static void Heapify<T>(T[] t, uint left, uint right) where T : IComparable
        {
            uint i = left,

            j = 2 * i + 1;

            var buf = t[i];

            while (j <= right)
            {
                if (j < right)
                    if (IsLowerThan(t[j], t[j + 1])) j++;
                if (IsGreaterEqual(buf, t[j])) break;
                t[i] = t[j];
                i = j;
                j = 2 * i + 1;
            }

            t[i] = buf;
        }

        public static int HeapSort<T>(T[] t) where T : IComparable
        {
            uint left = ((uint)t.Length / 2),
                 right = (uint)t.Length - 1;

            while (left > 0)
            {
                left--;
                Heapify(t, left, right);
            }
            while (right > 0)
            {
                var buf = t[left];
                t[left] = t[right];
                t[right] = buf;
                right--;
                Heapify(t, left, right);
            }
            return 1;
        }
    }
}