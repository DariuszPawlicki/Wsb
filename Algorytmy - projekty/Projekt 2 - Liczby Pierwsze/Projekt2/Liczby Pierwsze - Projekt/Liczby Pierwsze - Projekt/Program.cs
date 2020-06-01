using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Liczby_Pierwsze___Projekt
{
    class Program
    {
        static ulong DivsNum = 0;
        static List<String> wyniki = new List<String>();

        static bool AlgorytmPrzykładowy(BigInteger Num)
        {
            if (Num < 2) return false;
            else if (Num < 4) return true;
            else if (Num % 2 == 0) return false;
            else for (BigInteger u = 3; u < Num / 2; u += 2)
                    if (Num % u == 0) return false;
            return true;
        }
        static bool AlgorytmPrzykładowyInstrumentacja(BigInteger Num)
        {
            DivsNum = 0;

            if (Num < 2) return false;
            else if (Num < 4) return true;
            if (Num % 2 == 0)
            {
                DivsNum++;
                return false;
            }
            else
            {
                DivsNum++;
            }

            for (BigInteger u = 3; u < Num / 2; u += 2)
            {
                    DivsNum++;
                    if (Num % u == 0) return false;
            }

            return true;
        }

        static bool AlgorytmLepszy(BigInteger Num)
        {
            if (Num < 2) return false;
            else if (Num < 4) return true;
            else if (Num % 2 == 0) return false;
            else
            {
                BigInteger pierwiastek;

                if (Num > Int64.MaxValue){
                    pierwiastek = PrzyblizeniePierwiastka(Num);
                }
                else
                {
                    pierwiastek = (BigInteger)Math.Sqrt((Int64)Num);
                }


                for (BigInteger u = 3; u <= pierwiastek; u += 2)
                    if (Num % u == 0) return false;
            }
            return true;
        }

        static bool AlgorytmLepszyInstrumentacja(BigInteger Num)
        {
            DivsNum = 0;

            if (Num < 2) return false;
            else if (Num < 4) return true;
            if (Num % 2 == 0)
            {
                DivsNum++;
                return false;
            }
            else
            {
                DivsNum++;
            }

                BigInteger pierwiastek;

                if (Num > Int64.MaxValue)
                {
                    pierwiastek = PrzyblizeniePierwiastka(Num);
                }
                else
                {
                    pierwiastek = (BigInteger)Math.Sqrt((Int64)Num);
                }

                for (BigInteger u = 3; u <= pierwiastek; u += 2)
                {
                    DivsNum++;
                    if (Num % u == 0) return false;
                }

            return true;
        }

        static bool AlgorytmJeszczeLepszy(BigInteger Num)
        {
            if (Num < 2) return false;
            else if (Num < 4) return true;
            else if (Num % 2 == 0 || Num % 3 == 0) return false;
            else
            {
                BigInteger pierwiastek;

                if (Num > Int64.MaxValue)
                {
                    pierwiastek = PrzyblizeniePierwiastka(Num);
                }
                else
                {
                    pierwiastek = (BigInteger)Math.Sqrt((Int64)Num);
                }


                for (BigInteger u = 6; u <= pierwiastek; u += 6)
                {
                    if (Num % (u - 1) == 0) return false;
                    if (Num % (u + 1) == 0) return false;
                }
            }

            return true;
        }

        static bool AlgorytmJeszczeLepszyInstrumentacja(BigInteger Num)
        {
            DivsNum = 0;

            if (Num < 2) return false;
            else if (Num < 4) return true;

            if (Num % 2 == 0 || Num % 3 == 0)
            {
                DivsNum += 2;
                return false;
            }
            else
            {
                DivsNum += 2;
            }

            BigInteger pierwiastek;

            if (Num > Int64.MaxValue)
            {
                pierwiastek = PrzyblizeniePierwiastka(Num);
            }
            else
            {
                pierwiastek = (BigInteger)Math.Sqrt((Int64)Num);
            }


            for (BigInteger u = 6; u <= pierwiastek; u += 6)
            {
                DivsNum++;
                if (Num % (u - 1) == 0) return false;
                DivsNum++;
                if (Num % (u + 1) == 0) return false;
            }
            
            return true;
        }

        static BigInteger PrzyblizeniePierwiastka(BigInteger Num, int ilosc_przyblizen = 50)
        {
            BigInteger x = Num;     // Działa dobrze tylko dla bardzo dużych liczb, ponieważ
                                   //  przy dzieleniu nie uwzględniana jest część dziesiętna, ze względu na
                                  //   typ zmiennych

            for(int i = 0; i < ilosc_przyblizen; i++)
            {
                BigInteger f0 = BigInteger.Multiply(x, x) - Num; // Wartość funkcji - (x^2 - Num)
                BigInteger f1 = BigInteger.Multiply(x, 2);      //  Wartość pierwszej pochodnej funkcji f0 - (2 * x)

                BigInteger div = BigInteger.Divide(f0, f1);

                x -= div;
            }

            return x;
        }

        static void ZapiszWynikiDoPliku()
        {
            Console.WriteLine("Zapisywanie wyników do pliku.");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Darek\Desktop\Studia\Algorytmy\Projekt2\dane.txt"))
            {
                for (int i = 0; i < wyniki.Count; i++)
                {
                    if (i % 4 == 0 && i != 0)
                        file.WriteLine();

                    file.Write(wyniki[i]);

                    if (i < wyniki.Count - 1)
                        file.Write(" ");
                }
            }
        }

        static void Main(string[] args)
        {
            String[] PrimeNums = new String[]{ "101", "1009", "10091", "100913", "1009139",
                                               "1009140611", "10091406133", "100914061337", "1009140613399"};

            Console.WriteLine("Test pierwszości liczb.");

            foreach (String number in PrimeNums)
            {
                BigInteger Num = BigInteger.Parse(number);
                wyniki.Add(number);

                if (Num <= 1009140611) // Zmniejszam zakres dla algorytmu podstawowego, 
                                      //  ze względu na bardzo długi czas jego wykonywania dla większych liczb
                {
                    AlgorytmPrzykładowyInstrumentacja(Num); 
                    wyniki.Add(DivsNum.ToString());
                }
                else
                {
                    wyniki.Add("0"); // Dodaję 0, aby w pliku z wynikiami zachować poprawność wyświetlania
                }

                AlgorytmLepszyInstrumentacja(Num);
                wyniki.Add(DivsNum.ToString());

                AlgorytmJeszczeLepszyInstrumentacja(Num);
                wyniki.Add(DivsNum.ToString());

            }


           // ZapiszWynikiDoPliku();


        }
    }
}