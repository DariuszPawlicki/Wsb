using System;
using System.Collections.Generic;
using System.IO;
using SortingMethods;
using Utilities;

namespace Projekt_3___Sortowanie
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int[], int>[] sorting_methods = {Sorting.InsertionSort, Sorting.SelectionSort,
                                                  Sorting.HeapSort, Sorting.CocktailSort};

            int[] arr_size = { 50000,  100000,  150000,  200000,  250000,  300000,  350000,  400000,
                               450000,  500000,  550000,  600000,  650000,  700000,  750000 };

            int[] test_arr = { 5000 };

            String[] init_types = { "random", "ascending", "descending", "constant", "v-shape" };

            Dictionary<String, Object> results_by_method = new Dictionary<String, Object>();
            Dictionary<int, Object> results_by_size = new Dictionary<int, Object>();
            Dictionary<String, long> results_by_init = new Dictionary<String, long>();

            foreach (Func<int[], int> method in sorting_methods)
            {
                foreach (int size in test_arr)
                {
                    foreach (String init_type in init_types)
                    {
                        int[] tmp_arr = Utils.InitArray(size, init_type);
                        var time = Utils.TimeMeasurement(method, tmp_arr);

                        results_by_init[init_type] = time;
                    }
                    results_by_size[size] = results_by_init;
                }
                results_by_method[method.Method.Name] = results_by_size;
            }

            Utils.IterateResultsDictionary(results_by_method);
            
            Console.ReadKey();
        }
    }
}