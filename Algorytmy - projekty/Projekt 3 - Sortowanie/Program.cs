using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SortingMethods;
using Utilities;

namespace Projekt_3___Sortowanie
{
    class Program
    {

        static Func<T[], int>[] ReturnFuncOfType<T>() where T : IComparable // Zwraca obiekt zawierający tablicę odwołań do wszystkich metod sortujących,
                                                                           //  typ generyczny określa typ tablicy na jakiej operować będą metody sortujące
        {
            Func<T[], int>[] sorting_methods = {Sorting.InsertionSort, Sorting.SelectionSort,
                                                Sorting.HeapSort, Sorting.CocktailSort};

            return sorting_methods;
        }

        static void SortingMethodsTest(int measurement_points, bool save_results = false)
        {
            Func<double[], int>[] sorting_methods = ReturnFuncOfType<double>();

            IEnumerable<int> sizes_range = Enumerable.Range(1, measurement_points).Select(x => 2500 * x);

            String[] init_types = { "random", "ascending", "descending", "constant", "v-shape" };

            Dictionary<String, Object> results_by_method = new Dictionary<String, Object>();

            Dictionary<int, Object> results_by_size;
            Dictionary<String, long> results_by_init;

            foreach (Func<double[], int> method in sorting_methods)
            {
                results_by_size = new Dictionary<int, Object>();

                foreach (int size in sizes_range)
                {
                    results_by_init = new Dictionary<String, long>();

                    foreach (String init_type in init_types)
                    {
                        double[] tmp_arr = Utils.InitArray<double>(size, init_type);
                        long time = Utils.TimeMeasurement<double>(method, tmp_arr);

                        results_by_init[init_type] = time;
                    }

                    results_by_size[size] = results_by_init;
                }

                results_by_method[method.Method.Name] = results_by_size;
            }

            Utils.IterateResultsDictionary(results_by_method, save_results, results_to_csv: true);
        } // Pomiary dla wszystkich algorytmów oprócz QuickSorta

        static void QuickSortTest(int measurement_points, bool save_results = false)
        {
            ArrayList results = new ArrayList();

            String[] pivot_positions = { "random", "left", "right", "middle" };

            IEnumerable<int> sizes_range = Enumerable.Range(1, measurement_points).Select(x => 2500 * x);

            results.Add("Pivot," + "Typ," + "Rozmiar," + "Czas" + Environment.NewLine);

            int[] arr;
            long time;

            foreach(String pivot_position in pivot_positions)
            {
                foreach (String type in new []{ "recurrent", "iterative" } ){

                    foreach (int size in sizes_range)
                    {
                        arr = Utils.InitArray<int>(size, "random");
                        time = Utils.TimeMeasurement(arr, type, pivot_position);

                        results.Add(pivot_position + "," + type + "," + size + "," + time + Environment.NewLine);
                    }
                }
            }

            foreach(String line in results)
            {
                Console.Write(line);
            }

            if (save_results == true)
                Utils.WriteDataToFile(results, Utils.PATH, "pomiary_quicksort", save_results);
        }

        static void Main(string[] args)
        {

            SortingMethodsTest(20, false);
            QuickSortTest(20, false);

            Console.ReadKey();
        }
    }
}