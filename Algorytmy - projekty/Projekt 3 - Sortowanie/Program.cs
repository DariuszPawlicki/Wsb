using System;
using System.Collections.Generic;
using System.Linq;
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

            IEnumerable<int> sizes_range = Enumerable.Range(1, 20).Select(x => 2500 * x);

            String[] init_types = { "random", "ascending", "descending", "constant", "v-shape" };

            Dictionary<String, Object> results_by_method = new Dictionary<String, Object>();

            Dictionary<int, Object> results_by_size;
            Dictionary<String, long> results_by_init;

            foreach (Func<int[], int> method in sorting_methods)
            {
                results_by_size = new Dictionary<int, Object>();             

                foreach (int size in sizes_range)
                {
                    results_by_init = new Dictionary<String, long>();

                    foreach (String init_type in init_types)
                    {
                        int[] tmp_arr = Utils.InitArray(size, init_type);
                        long time = Utils.TimeMeasurement(method, tmp_arr);

                        results_by_init[init_type] = time;
                    }

                    results_by_size[size] = results_by_init;
                }

                results_by_method[method.Method.Name] = results_by_size;
            }

            Utils.IterateResultsDictionary(results_by_method, results_to_csv: true);
            
            Console.ReadKey();
        }
    }
}