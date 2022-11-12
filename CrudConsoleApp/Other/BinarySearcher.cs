using CrudConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudConsoleApp.Other
{
    class BinarySearcher
    {
        public void Run()
        {
            Console.Clear();
            List<int> currentList = RandomList(10);
            DisplayList(currentList);
            currentList.Sort();
            DisplayList(currentList);
            
            while (true)
            {
                string input = Console.ReadLine();

                if(int.TryParse(input, out int parsedInput)){

                    int result = FindIndex(parsedInput, currentList);
                    if(result < 0)
                    {
                        Console.WriteLine("Number is not in the list...");
                    }
                    else
                    {
                        Console.WriteLine($"Number is on index {result}");
                    }
                        
                    break;
                }
            }
            

            Console.ReadKey();
        }
        


        void DisplayList(List<int> list)
        {
            foreach (int nr in list)
            {
                Console.Write($"{nr} ");

            }
            Console.WriteLine();
            ConsoleHelper.DrawLine();
        }

        int FindIndex(int nr, List<int> nrs)
        {
          
            
            int rightBorder = nrs.Count -1;
            int leftBorder = 0;
            if (nrs.Count < 1)
            {
                return -1;
            }

            while (true)
            {

                int currIndex = (int)Math.Round((double)(leftBorder + rightBorder) / 2);

                
                if(nr < nrs[currIndex])
                {

                    rightBorder = currIndex -1;
                }
                else if(nr > nrs[currIndex])
                {

                    leftBorder = currIndex +1;
                }
                else
                {
                    return currIndex;
                }
            }

        }


        List<int> RandomList(int length)
        {
            Random random = new Random();
            List<int> result = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int randomNr;

                while (true)
                {
                   randomNr = random.Next(0, 100);
                    if (!result.Contains(randomNr)) break;
                }
                
                result.Add(randomNr);
            }

            return result;
        }
    }

}
