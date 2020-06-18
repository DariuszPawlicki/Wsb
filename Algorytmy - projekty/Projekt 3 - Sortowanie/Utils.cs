using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utilities
{
    public static class Utils
    {
        public static int[] InitArray(int size, String init_type)
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
            else if (init_type == "ascending")
            {
                for (int i = 0; i < size; i++)
                {
                    arr[i] = i;
                }
            }
            else if (init_type == "descending")
            {
                int j = 0;

                for (int i = size - 1; i >= 0; i--)
                {
                    arr[i] = j;
                    j++;
                }
            }
            else if (init_type == "constant")
            {
                for (int i = 0; i < size; i++)
                {
                    arr[i] = 1;
                }
            }
            else if (init_type == "v-shape")
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

                while (i >= 0)
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

        public static void PrintArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item);
                Console.Write(" ");
            }

            Console.WriteLine("\n");
        }

        public static long TimeMeasurement(Func<int[], int> sorting_method, int[] arr)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            sorting_method(arr);

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        public static void WriteDataToFile(Dictionary<String, int[]> results)
        {

        }

        public static void IterateResultsDictionary(Object results)
        {
            String separator1 = new String('-', 30);
            String separator2 = new String('*', 30);

            ArrayList communicates = new ArrayList();

            foreach (KeyValuePair<String, Object> method_dict in (Dictionary<String, Object>)results)
            {
                communicates.Add(separator1 + Environment.NewLine);
                Console.WriteLine(separator1);

                communicates.Add("Sortowanie: " + method_dict.Key + Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Sortowanie: " + method_dict.Key + "\n");

                foreach (KeyValuePair<int, Object> size_dict in (Dictionary<int, Object>)method_dict.Value)
                {
                    communicates.Add("Rozmiar tablicy: " + size_dict.Key + Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Rozmiar tablicy: " + size_dict.Key + "\n");

                    foreach (KeyValuePair<String, long> init_type_dict in (Dictionary<String, long>)size_dict.Value)
                    {
                        communicates.Add("Metoda inicjalizacji tablicy: " + init_type_dict.Key + Environment.NewLine);
                        Console.WriteLine("Metoda inicjalizacji tablicy: " + init_type_dict.Key + "\n");

                        communicates.Add("Czas sortowania: " + init_type_dict.Value + "ms" + Environment.NewLine + Environment.NewLine);
                        Console.WriteLine("Czas sortowania: " + init_type_dict.Value + "ms" + "\n");
                    }

                    communicates.Add(separator2 + Environment.NewLine);
                    Console.WriteLine(separator2);
                }
            }

            String path = "C:\\Users\\Darek\\Desktop\\wsb\\Algorytmy - projekty\\Projekt 3 - Sortowanie\\";

            WriteDataToFile(communicates, path, "komunikaty");
        }

        public static void WriteDataToFile(ArrayList data, String path, String filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + filename + ".txt"))
            {
                foreach (String line in data)
                {
                    file.Write(line);
                }
            }
        }
    }
}