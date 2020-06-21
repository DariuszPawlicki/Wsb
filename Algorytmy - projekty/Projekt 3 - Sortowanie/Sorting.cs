using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortingMethods
{
    public class Stack
    {
        private ArrayList data = new ArrayList();

        public void Push(int item)
        {
            data.Insert(0, item);
        }

        public int Pop()
        {
            int item = (int)data[0];

            data.RemoveAt(0);

            return item;
        }

        public void Peek()
        {
            if (data.Count > 0)
                Console.WriteLine("Wierzchołek stosu: " + data[0]);
            else
                Console.WriteLine("Stos jest pusty.");
        }

        public int Size()
        {
            return data.Count;
        }
    }

    public static class Sorting
    {

        public static bool IsGreaterThan<T>(T x, T y) where T : IComparable
        {
            return x.CompareTo(y) > 0;
        }

        public static bool IsLowerThan<T>(T x, T y) where T : IComparable
        {
            return x.CompareTo(y) < 0;
        }

        public static bool AreEqual<T>(T x, T y) where T : IComparable
        {
            return x.CompareTo(y) == 0;
        }

        public static bool IsGreaterEqual<T>(T x, T y) where T : IComparable
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

        public static int ChoosePivotPosition(int left, int right, String pivot_position)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());

            if (pivot_position == "middle")
                return (left + right) / 2;
            else if (pivot_position == "random")
                return rnd.Next(right);
            else if (pivot_position == "first")
                return left;
            else if (pivot_position == "last")
                return right;
            else
                return left;
        }

        public static void Swap(int[] arr, int x1_id, int x2_id)
        {
            int tmp = arr[x1_id];

            arr[x1_id] = arr[x2_id];
            arr[x2_id] = tmp;
        }

        private static int Partition(int[] t, int position, int start, int end)
        {
            int l = start;
            int h = end - 2;
            int piv = t[position];
            Swap(t, position, end - 1);

            while (l < h)
            {
                if (t[l] < piv)
                {
                    l++;
                }
                else if (t[h] >= piv)
                {
                    h--;
                }
                else
                {
                    Swap(t, l, h);
                }
            }
            int idx = h;
            if (t[h] < piv)
            {
                idx++;
            }
            Swap(t, end - 1, idx);
            return idx;
        }

        public static int QuickSortRecurrent(int[] t, int left, int right, String pivot_position = "middle")
        {
            int i, j, x;

            i = left;
            j = right;
            x = t[ChoosePivotPosition(left, right, pivot_position)];

            while (i <= j)
            {
                while (IsLowerThan(t[i], x)) i++;
                while (IsLowerThan(x, t[j])) j--;

                if (i <= j)
                {
                    Swap(t, i, j);
                    i++; j--;
                }
            }

            if (left < j) QuickSortRecurrent(t, left, j);
            if (i < right) QuickSortRecurrent(t, i, right);

            return 1;
        }

        public static void QuickSortIterative(int[] t, String pivot_position = "middle")
        {
            Stack stack = new Stack();

            stack.Push(0);
            stack.Push(t.Length);

            while (stack.Size() > 0)
            {
                int end = stack.Pop();
                int start = stack.Pop();
                if (end - start < 2)
                {
                    continue;
                }
                int p = start + ((end - start) / 2);
                p = Partition(t, p, start, end);

                stack.Push(p + 1);
                stack.Push(end);

                stack.Push(start);
                stack.Push(p);

            }
        }

        

    }   
}