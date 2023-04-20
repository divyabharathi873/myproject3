using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Flips("HHTHTT");
            //int[][] arrays = lst.Select(a => a.ToArray()).ToArray();
            //string[][] product = new string[][] { { "10", "d0", "d1" }, { "15", "E", "E" }, { "20", "d1", "E" } };

            //string[][] discount = new string[][] { { "d0", "1", "27" }, { "d1", "2", "5" } };

            List<List<string>> products = new List<List<string>>();
            products.Add(new List<String>() { "10", "d0", "d1" });
            products.Add(new List<String>() { "15", "E", "E" });
            products.Add(new List<String>() { "20", "d1", "E" });

            List<List<string>> discounts = new List<List<string>>();
            discounts.Add(new List<String>() { "d0", "1", "27" });
            discounts.Add(new List<String>() { "d1", "2", "5" });

            /* products = product.Select(x => x.ToList()).ToList();

              products= product.Select(x => x.ToList()).ToList();
             for (int i=0;i< product.Length;i++)
             {
                 products.Add(product[i].List());

             }*/

            Console.WriteLine(Totalprice(products, discounts));
        }

        private static int Totalprice(List<List<string>> product, List<List<string>> discount)
        {
            Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();
            int totalPrice = 0;
            for (int i = 0; i < discount.Count; i++)
            {
                if (discount[i][1] == "1")
                {
                    int value = Convert.ToInt32(discount[i][2]);
                    dic.Add(discount[i][0], new List<int> { 1, value });
                }
                else if (discount[i][1] == "2")
                {
                    int value = Convert.ToInt32(discount[i][2]);
                    dic.Add(discount[i][0], new List<int> { 2, value });
                }
                else if (discount[i][1] == "0")
                {
                    int value = Convert.ToInt32(discount[i][2]);
                    dic.Add(discount[i][0], new List<int> { 0, value });
                }
            }


            for (int i = 0; i < product.Count; i++)
            {
                int Price = Convert.ToInt32(product[i][0]);
                int minPrice = Price;
                for (int j = 1; j < product[i].Count; j++)
                {
                    if (dic.ContainsKey(product[i][j]))
                    {
                        if (dic[product[i][j]][0] == 1)
                        {
                            minPrice = Math.Min(minPrice, Price - Price * (dic[product[i][j]][1]) / 100);
                        }
                        else if (dic[product[i][j]][0] == 2)
                        {
                            minPrice = Math.Min(minPrice, Price - dic[product[i][j]][1]);
                        }
                        else if (dic[product[i][j]][0] == 0)
                        {
                            minPrice = Math.Min(minPrice, dic[product[i][j]][1]);
                        }
                    }
                    else
                    {
                        minPrice = Math.Min(minPrice, Price);
                    }
                }
                totalPrice += minPrice;
            }

            return totalPrice;
        }

        public static int Flips(string s)
        {
            // WRITE YOUR BRILLIANT CODE HERE

            int count_Ts = 0;
            int flips = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'T')
                    count_Ts++;
                else
                    flips++;
                flips = Math.Min(count_Ts, flips);
            }
            return flips;
        }
        public static int Teams(List<int> skills, int size, int diff)
        {

            int i = 0, count = 0;
            int n = skills.Count;
            skills.Sort();
            while (i <= n - size)
            {
                if (skills[i + size - 1] - skills[i] <= diff)
                {
                    count++;
                    i = i + size;
                }
                else
                {
                    i++;
                }
            }
            return count;
        }

        //max avg stock price take sum and keep dividing array


        //truck problem nlogn time complexity
        public int MaximumUnits(int[][] boxTypes, int truckSize)
        {

            int[][] boxtype = boxTypes.OrderByDescending(x => x[1]).ToArray();
            int boxcount = 0;
            int maxUnits = 0;
            for (int i = 0; i < boxtype.Length; i++)
            {
                if (truckSize > 0)
                {
                    boxcount = Math.Min(boxtype[i][0], truckSize);
                    maxUnits += (boxcount * boxtype[i][1]);
                    //Console.WriteLine(boxtype[i][0]);
                    truckSize -= boxcount;
                    boxcount = 0;
                    //Console.WriteLine(boxtype[i][1]);
                }
                else
                {
                    break;
                }
            }
            return maxUnits;
        }

        public int NumPairsDivisibleBy60(int[] time)
        {
            int[] remainder = new int[60];
            int count = 0;
            for (int i = 0; i < time.Length; i++)
            {
                if (time[i] % 60 == 0)
                {
                    count += remainder[0];

                }
                else
                {
                    count += remainder[60 - (time[i] % 60)];
                }
                remainder[time[i] % 60]++;
            }
            return count;
        }



        // Let's use numbers from 0 to 3 to mark the directions: north = 0, east = 1, south = 2, west = 3. In the array directions we could store corresponding coordinates changes, i.e. directions[0] is to go north, directions[1] is to go east, directions[2] is to go south, and directions[3] is to go west.

        // The initial robot position is in the center x = y = 0, facing north idx = 0.

        // Now everything is ready to iterate over the instructions.

        // If the current instruction is R, i.e. to turn on the right, the next direction is idx = (idx + 1) % 4. Modulo here is needed to deal with the situation - facing west, idx = 3, turn to the right to face north, idx = 0.

        // If the current instruction is L, i.e. to turn on the left, the next direction could written in a symmetric way idx = (idx - 1) % 4. That means we have to deal with negative indices. A more simple way is to notice that 1 turn to the left = 3 turns to the right: idx = (idx + 3) % 4.

        // If the current instruction is to move, we simply update the coordinates: x += directions[idx][0], y += directions[idx][1].

        // After one cycle we have everything to decide. It's a limit cycle trajectory if the robot is back to the center: x = y = 0 or if the robot doesn't face north: idx != 0.
        //Time complexity: O(N)\mathcal{O}(N)O(N), where NNN is a number of instructions to parse.

        //Space complexity: O(1)\mathcal{O}(1)O(1) because the array directions contains only 4 elements.

      
            public bool IsRobotBounded(string instructions)
            {
                //north =0, east=1, south=2, west=3
                int[,] directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
                int x = 0;
                int y = 0;
                int index = 0;

                foreach (char c in instructions)
                {
                    if (c == 'L')
                    {
                        index = (index + 3) % 4;
                    }
                    else if (c == 'R')
                    {
                        index = (index + 1) % 4;
                    }
                    else
                    {
                        x += directions[index, 0];
                        y += directions[index, 1];
                    }
                }

                return (x == 0 && y == 0) || (index != 0);

            }


        public char SlowestKey(int[] releaseTimes, string keysPressed)
        {
            int longduration = releaseTimes[0];
            char slowkey = keysPressed[0];

            for (int i = 1; i < keysPressed.Length; i++)
            {
                int curduration = releaseTimes[i] - releaseTimes[i - 1];
                if (curduration > longduration || (curduration == longduration && keysPressed[i] > slowkey))
                {
                    longduration = curduration;
                    slowkey = keysPressed[i];
                }


            }

            return slowkey;
        }

    }
    }
