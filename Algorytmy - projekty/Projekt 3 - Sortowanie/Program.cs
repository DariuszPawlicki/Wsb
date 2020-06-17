using System;
using System.Reflection;

namespace Projekt_3___Sortowanie
{
    class Program
    {
        
        static int InsertionSort(int[] t)
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


        static int SelectionSort(int[] t)
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

        static int CocktailSort(int[] t)
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

        static void Heapify(int[] t, uint left, uint right)
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

        static int HeapSort(int[] t)
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

        static int[] InitArray(int size, String init_type) 
        {
            int[] arr = new int[size];

            if (init_type == "random")
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());

                for (int i = 0; i < size; i++)
                {
                    arr[i] = rnd.Next(size);
                }
            }
            else if(init_type == "ascending")
            {
                for(int i = 0; i < size; i++)
                {
                    arr[i] = i;
                }
            }
            else if(init_type == "descending")
            {
                int j = 0;

                for(int i = size - 1; i >= 0; i--)
                {
                    arr[i] = j;
                    j++;
                }
            }
            else if(init_type == "constant")
            {
                for(int i = 0; i < size; i++)
                {
                    arr[i] = 1;
                }
            }
            else if(init_type == "v-shape")
            {
                int mid_index;

                if (size % 2 == 0)
                {
                    mid_index = (size / 2) - 1;
                    arr[arr.Length - 1] = arr.Length;
                }
                else
                    mid_index = size / 2;

                arr[mid_index] = 1;

                int i = mid_index - 1;
                int j = mid_index + 1;

                int x = 1;

                while(i >= 0)
                {
                    arr[j] = ++x;
                    arr[i] = ++x;
                   
                    --i;
                    ++j;
                }
            }
            else
                Console.WriteLine("Wrong init_type.\n");

            return arr;
        }

        static void PrintArray(int[] arr)
        {
            foreach(var item in arr)
            {
                Console.Write(item);
                Console.Write(" ");
            }

            Console.WriteLine("\n");
        }

        static void TimeMeasurement(Func<int[], int> sorting_method, int[] arr)
        {                       
            var watch = System.Diagnostics.Stopwatch.StartNew();
            sorting_method(arr);

            watch.Stop();

            Console.WriteLine("Sorting time: " + watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("Array size: " + arr.Length + "\n");
        }

        static void Main(string[] args)
        {

            String init_type = "v-shape";

            int size = 10;
            int[] random_array = InitArray(size, init_type);

            
            PrintArray(random_array);

            TimeMeasurement(SelectionSort, random_array);

            PrintArray(random_array);

            Console.ReadKey();
        }
    }
}