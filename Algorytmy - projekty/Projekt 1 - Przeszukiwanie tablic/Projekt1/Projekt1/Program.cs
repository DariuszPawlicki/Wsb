using System;
using System.Collections;
using System.Diagnostics;

namespace Projekt1
{
    class Program
    {
        static ulong OpComparisonEQ = 0;
        const int NIter = 10;

        static ArrayList data = new ArrayList();

        static bool IsPresent_LinearTim(int[] Vector, int Number)
        {
            for (int i = 0; i < Vector.Length; i++)
                if (Vector[i] == Number)
                    return true;
            return false;
        }

        static bool IsPresent_LinearInstr(int[] Vector, int Number)
        {

            for (int i = 0; i < Vector.Length; i++)
            {
                OpComparisonEQ++;
                if (Vector[i] == Number) return true;
            }
            return false;
        }

        static bool IsPresent_BinaryTim(int[] Vector, int Number)
        {
            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                Middle = (Left + Right) / 2;
                if (Vector[Middle] == Number) return true;
                else if (Vector[Middle] > Number) Right = Middle - 1;
                else Left = Middle + 1;
            }
            return false;
        }

        static bool IsPresent_BinaryInstr(int[] Vector, int Number)
        {

            int Left = 0, Right = Vector.Length - 1, Middle;
            while (Left <= Right)
            {
                Middle = (Left + Right) / 2;
                if (Vector[Middle] == Number)
                {
                    OpComparisonEQ++;
                    return true;
                }
                else
                {
                    if (Vector[Middle] > Number) Right = Middle - 1;
                    else Left = Middle + 1;

                    OpComparisonEQ++;
                }
            }
            return false;
        }

        static void LinearMaxInstr(int[] TestVector)
        {
            OpComparisonEQ = 0;

            bool IsPresent = IsPresent_LinearInstr(TestVector, TestVector.Length - 1);
            Console.WriteLine("Linear Max Instr " + OpComparisonEQ);
            data.Add(OpComparisonEQ);

        }

        static void LinearAvgInstr(int[] TestVector)
        {
            OpComparisonEQ = 0;
            bool Present;
            for (int i = 0; i < TestVector.Length; ++i)
                Present = IsPresent_LinearInstr(TestVector, i);

            double average = (double)OpComparisonEQ / (double)TestVector.Length;
            data.Add(average);

            Console.WriteLine("Linear Avg Instr " + average.ToString("F1"));
        }

        static void LinearAvgTim(int[] TestVector)
        {          
                double IterationAvgTime = 0; // Czas średni jednego przejścia pętli NIter

                for (int n = 0; n < (NIter + 1 + 1); ++n)
                {

                long TotalTime = 0; 

                for (int i = 0; i < TestVector.Length; i++)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present = IsPresent_LinearTim(TestVector, i);
                    long EndingTime = Stopwatch.GetTimestamp();

                    TotalTime += EndingTime - StartingTime;                    
                }

                IterationAvgTime += ((double)TotalTime / TestVector.Length);
                }

                double AvgTime = (IterationAvgTime / NIter) * (1.0 / Stopwatch.Frequency) ;

                data.Add(AvgTime.ToString("F8"));

                Console.WriteLine("Linear Avg Tim " + AvgTime.ToString("F8"));            
        }

        static void LinearMaxTim(int[] TestVector)
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_LinearTim(TestVector, TestVector[TestVector.Length - 1]);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));

            data.Add(ElapsedSeconds.ToString("F8"));

            Console.WriteLine("Linear Max Tim " + ElapsedSeconds.ToString("F8"));
        }

        static void BinaryMaxInstr(int[] TestVector)
        {
            OpComparisonEQ = 0;
            bool Present = IsPresent_BinaryInstr(TestVector, TestVector[TestVector.Length - 1]);
            Console.WriteLine("Binary Max Instr " + OpComparisonEQ);
            data.Add(OpComparisonEQ);
        }

        static void BinaryMaxTim(int[] TestVector)
        {
            double ElapsedSeconds;
            long ElapsedTime = 0, MinTime = long.MaxValue, MaxTime = long.MinValue, IterationElapsedTime;
            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long StartingTime = Stopwatch.GetTimestamp();
                bool Present = IsPresent_BinaryTim(TestVector, TestVector[TestVector.Length - 1]);
                long EndingTime = Stopwatch.GetTimestamp();
                IterationElapsedTime = EndingTime - StartingTime;
                ElapsedTime += IterationElapsedTime;
                if (IterationElapsedTime < MinTime) MinTime = IterationElapsedTime;
                if (IterationElapsedTime > MaxTime) MaxTime = IterationElapsedTime;
            }
            ElapsedTime -= (MinTime + MaxTime);
            ElapsedSeconds = ElapsedTime * (1.0 / (NIter * Stopwatch.Frequency));

            data.Add(ElapsedSeconds.ToString("F10"));

            Console.WriteLine("Binary Max Tim " + ElapsedSeconds.ToString("F10"));
        }

        static void BinaryAvgInstr(int[] TestVector)
        {
            OpComparisonEQ = 0;
            bool Present;
            for (int i = 0; i < TestVector.Length; ++i)
                Present = IsPresent_BinaryInstr(TestVector, i);
            
            double average = (double)OpComparisonEQ / (double)TestVector.Length;
            data.Add(average);

            Console.WriteLine("Binary Avg Instr " + average.ToString("F1"));

        }

        static void BinaryAvgTim(int[] TestVector)
        {
            double IterationAvgTime = 0; // Czas średni jednego przejścia pętli NIter

            for (int n = 0; n < (NIter + 1 + 1); ++n)
            {
                long TotalTime = 0;

                for (int i = 0; i < TestVector.Length; i++)
                {
                    long StartingTime = Stopwatch.GetTimestamp();
                    bool Present = IsPresent_BinaryTim(TestVector, i);
                    long EndingTime = Stopwatch.GetTimestamp();

                    TotalTime += EndingTime - StartingTime;
                }

                IterationAvgTime += ((double)TotalTime / TestVector.Length);
            }

            double AvgTime = (IterationAvgTime / NIter) * (1.0 / Stopwatch.Frequency);
            data.Add(AvgTime.ToString("F10"));

            Console.WriteLine("Binary Avg Tim " + AvgTime.ToString("F10"));
        }

        static void WriteDataToFile()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Darek\Desktop\Algorytmy\dane.txt"))
            {
                for(int i = 0; i < data.Count; i++) { 
                    if (i % 9 == 0 && i != 0)
                        file.WriteLine();

                    file.Write(data[i]);

                    if (i < data.Count - 1)
                        file.Write(" ");
                }
            }
        }

        static void Main(string[] args)
        {
            for (int ArraySize = 10000; ArraySize <= 1000000; ArraySize += 10000)
            {
                Console.WriteLine("Size " + ArraySize);

                data.Add(ArraySize);

                int[] TestVector = new int[ArraySize];

                for (int i = 0; i < TestVector.Length; ++i)
                    TestVector[i] = i;

                
                LinearMaxInstr(TestVector);                
                LinearMaxTim(TestVector); 
                BinaryMaxInstr(TestVector); 
                BinaryMaxTim(TestVector); 
                LinearAvgInstr(TestVector); 
                LinearAvgTim(TestVector); 
                BinaryAvgInstr(TestVector); 
                BinaryAvgTim(TestVector); 

                Console.Write("\n");              
            }
            WriteDataToFile();
        }
    }
}