using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
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

        public static void IterateResultsDictionary(Object results, bool results_to_csv = false)
        {
            String separator1 = new String('-', 30);
            String separator2 = new String('*', 30);

            String new_line = Environment.NewLine;

            ArrayList measurements;

            if (results_to_csv == false)
                measurements = new ArrayList();
            else
                measurements = new ArrayList()
                {
                    "Sortowanie,", "RozmiarTablicy,", "MetodaInicjalizacji,", "CzasSortowania", new_line
                };


            foreach (KeyValuePair<String, Object> method_dict in (Dictionary<String, Object>)results)
            {
                if(results_to_csv == false)
                    measurements.Add(separator1 + new_line);

                Console.WriteLine(separator1);

                if (results_to_csv == false)
                    measurements.Add("Sortowanie: " + method_dict.Key + new_line + new_line);
                
                Console.WriteLine("Sortowanie: " + method_dict.Key + "\n");

                foreach (KeyValuePair<int, Object> size_dict in (Dictionary<int, Object>)method_dict.Value)
                {
                    if (results_to_csv == false)
                        measurements.Add("Rozmiar tablicy: " + size_dict.Key + new_line + new_line);

                    Console.WriteLine("Rozmiar tablicy: " + size_dict.Key + "\n");

                    foreach (KeyValuePair<String, long> init_type_dict in (Dictionary<String, long>)size_dict.Value)
                    {
                        if (results_to_csv == false) {
                            measurements.Add("Metoda inicjalizacji tablicy: " + init_type_dict.Key + new_line);
                            measurements.Add("Czas sortowania: " + init_type_dict.Value + "ms" + new_line + new_line);
                        }
                        else {
                            measurements.Add(method_dict.Key + ",");
                            measurements.Add(size_dict.Key + ",");
                            measurements.Add(init_type_dict.Key + ",");
                            measurements.Add(init_type_dict.Value + new_line);
                        }

                        Console.WriteLine("Metoda inicjalizacji tablicy: " + init_type_dict.Key + "\n");
                        Console.WriteLine("Czas sortowania: " + init_type_dict.Value + "ms" + "\n");
                                                   
                    }

                    if (results_to_csv == false)
                        measurements.Add(separator2 + new_line);

                    Console.WriteLine(separator2);
                }
            }

            String path = "C:\\Users\\Darek\\Desktop\\wsb\\Algorytmy - projekty\\Projekt 3 - Sortowanie\\";

            WriteDataToFile(measurements, path, "pomiary", csv: results_to_csv);
        }

        public static void WriteDataToFile(ArrayList data, String path, String filename, bool csv = false)
        {
            String extension;

            if (csv == true)
                extension = ".csv";
            else
                extension = ".txt";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + filename + extension))
            {
                foreach (String line in data)
                {
                    file.Write(line);
                }
            }
        }
    }
}