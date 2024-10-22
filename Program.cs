using System;

namespace LottoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int forsok = 0; // Teller for antall forsøk
            int[] lottoLapp = new int[7]; // Lottolapp som skal fylles med tall

            // Meny for å velge mellom å få tilfeldige tall eller velge selv
            Console.WriteLine("Velkommen til denne lotto appen!");
            Console.WriteLine("Velg et alternativ:");
            Console.WriteLine("1: Få tilfeldige lottotall");
            Console.WriteLine("2: Velg dine egne lottotall");

            string valg = Console.ReadLine();

            if (valg == "1")
            {
                // 1 - Trekke 7 tilfeldige unike tall for brukeren
                for (int i = 0; i < lottoLapp.Length; i++)
                {
                    int nyttTall = random.Next(1, 35);

                    // Sjekker om tallet allerede finnes i arrayen
                    if (FindNumber(nyttTall, lottoLapp))
                    {
                        i--; // Hvis tallet allerede finnes, trekk nytt tall
                    }
                    else
                    {
                        lottoLapp[i] = nyttTall; // Legg til det nye tallet hvis det ikke finnes fra før
                    }
                }
                Console.WriteLine("Dine tilfeldige lottotall er: " + string.Join(", ", lottoLapp));
            }
            else if (valg == "2")
            {
                // 2 - La brukeren velge sine egne 7 tall
                Console.WriteLine("Velg 7 unike tall mellom 1 og 34:");
                for (int i = 0; i < lottoLapp.Length; i++)
                {
                    int brukerTall;
                    bool gyldigTall = false;

                    while (!gyldigTall)
                    {
                        Console.Write($"Velg tall {i + 1}: ");
                        brukerTall = int.Parse(Console.ReadLine());

                        if (brukerTall < 1 || brukerTall > 34)
                        {
                            Console.WriteLine("Tallet må være mellom 1 og 34. Prøv igjen.");
                        }
                        else if (FindNumber(brukerTall, lottoLapp))
                        {
                            Console.WriteLine("Dette tallet er allerede valgt. Velg et annet.");
                        }
                        else
                        {
                            lottoLapp[i] = brukerTall;
                            gyldigTall = true;
                        }
                    }
                }
                Console.WriteLine("Dine valgte lottotall er: " + string.Join(", ", lottoLapp));
            }
            else
            {
                Console.WriteLine("Ugyldig valg");
                return;
            }

            // 3 - Fortsett å trekke nye lottotall helt til de matcher brukerens lottolapp
            for (; ; )
            {
                forsok++; // Øk antall forsøk

                int[] lottoTall = new int[7];
                for (int i = 0; i < lottoTall.Length; i++)
                {
                    int nyttTall = random.Next(1, 35);

                    // Sjekker om tallet allerede finnes i arrayen
                    if (FindNumber(nyttTall, lottoTall))
                    {
                        i--; // Hvis tallet allerede finnes, trekk nytt tall
                    }
                    else
                    {
                        lottoTall[i] = nyttTall; // Legg til det nye tallet hvis det ikke finnes fra før
                    }
                }

                // 4 - Sjekke om systemets lottotall matcher brukerens lottolapp
                if (ArraysEqual(lottoLapp, lottoTall))
                {
                    // Hvis tallene matcher, skriv dem ut og avslutt
                    Console.WriteLine("Lottotallene matcher etter " + forsok + " forsøk!");
                    Console.WriteLine("Systemets lottotall: " + string.Join(", ", lottoTall));
                    Console.WriteLine("Din lottolapp var: " + string.Join(", ", lottoLapp));
                    break; // Avslutt løkken
                }
            }
        }

        /// <summary>
        /// Leter etter et spesifikt tall i en array. Returnerer true om funnet.
        /// </summary>
        /// <param name="userNum"></param>
        /// <param name="lottoNum"></param>
        /// <returns> bool </returns>
        public static bool FindNumber(int userNum, int[] lottoNum)
        {
            foreach (int num in lottoNum)
            {
                if (num == userNum)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sjekker om to arrays er like
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns> bool </returns>
        public static bool ArraysEqual(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length) return false;

            Array.Sort(arr1); // Sorterer begge arrays for å sjekke likhet samme hvilke av rekkefølge
            Array.Sort(arr2);

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }
    }
}
