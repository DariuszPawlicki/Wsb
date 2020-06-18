using System;
using System.Text;

namespace SortingMethods
{
    public static class Sorting
    {
        public static int InsertionSort(int[] t)
        {
            for (uint i = 1; i < t.Length; i++)
            {
                uint j = i;
                int Buf = t[j];

                while ((j > 0) && (t[j - 1] > Buf))
                {
                    t[j] = t[j - 1];
                    j--;
                }

                t[j] = Buf;
            }

            return 1;
        }

        public static int SelectionSort(int[] t)
        {
            uint k;

            for (uint i = 0; i < (t.Length - 1); i++)
            {
                int Buf = t[i];
                k = i;
                for (uint j = i + 1; j < t.Length; j++)
                    if (t[j] < Buf)
                    {
                        k = j;
                        Buf = t[j];
                    }

                t[k] = t[i];
                t[i] = Buf;
            }

            return 1;
        }

        public static int CocktailSort(int[] t)
        {
            int Left = 1, Right = t.Length - 1, k = t.Length - 1;
            do
            {
                for (int j = Right; j >= Left; j--)
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Left = k + 1;
                for (int j = Left; j <= Right; j++)
                    if (t[j - 1] > t[j])
                    {
                        int Buf = t[j - 1]; t[j - 1] = t[j]; t[j] = Buf;
                        k = j;
                    }
                Right = k - 1;
            }
            while (Left <= Right);

            return 1;
        }

        public static void Heapify(int[] t, uint left, uint right)
        {
            uint i = left,

            j = 2 * i + 1;

            int buf = t[i];

            while (j <= right)
            {
                if (j < right)
                    if (t[j] < t[j + 1]) j++;
                if (buf >= t[j]) break;
                t[i] = t[j];
                i = j;
                j = 2 * i + 1;
            }

            t[i] = buf;
        }

        public static int HeapSort(int[] t)
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
                int buf = t[left];
                t[left] = t[right];
                t[right] = buf;
                right--;
                Heapify(t, left, right);
            }
            return 1;
        }
    }
}