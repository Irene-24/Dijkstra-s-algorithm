using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijskstra
{
    class Program
    {/// <summary>
    /// This method fills in the values for my matrices 
    /// </summary>
    /// <param name="costM">
    /// Cost matrix
    /// </param>
    /// <param name="conn">
    /// Ajacency matrix
    /// </param>
        public static void Fill_matrices(double[,] costM, int[,] conn)
        {
            int len = costM.GetLength(0); //get number of rows
            bool[,] isset = new bool[len, len]; //store set status of nodes
            int row = 0;
            double cost = 0;
            while (row < len)
            {
                for (int col = 0; col < len; col++)
                {
                    Console.Clear();
                    if (isset[row, col]) //have i set cell before?
                    {
                        continue;
                    }
                    else
                    {
                        isset[row, col] = (bool)true;
                        isset[col, row] = (bool)true;
                        if (row == col)
                        {
                            conn[row, col] = 0; //no loops allowed
                            costM[row, col] = 0;
                            continue;
                        }

                        Console.WriteLine("Is node {0} connected to node {1}?\nPress \n->Y for Yes \n-> N for No", row + 1, col + 1);
                        string s = Console.ReadLine().ToUpper();
                        if (s == "Y")
                        {
                            conn[row, col] = 1;
                            costentry:
                            Console.Write("Node {0} and {1} are connected.Enter cost:", row + 1, col + 1);
                            if (Double.TryParse(Console.ReadLine(), out cost))
                            {
                                costM[row, col] = cost;
                                Console.WriteLine("Is node {0} connected to node {1}?\nPress \n->Y for Yes \n-> N for No", col + 1, row + 1);
                                string s2 = Console.ReadLine().ToUpper();
                                if (s2 == "Y")
                                {
                                    conn[col, row] = 1;
                                    costM[col, row] = cost;
                                }
                                else
                                {
                                    conn[col, row] = 0;
                                    costM[col, row] = double.PositiveInfinity;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Invalid entry:");
                                Console.ForegroundColor = ConsoleColor.White;
                                goto costentry;
                            }
                        }

                        else
                        {
                            conn[row, col] = 0;
                            costM[row, col] = double.PositiveInfinity;
                            Console.WriteLine("Is node {0} connected to node {1}?\nPress \n->Y for Yes \n-> N for No", col + 1, row + 1);
                            string s2 = Console.ReadLine().ToUpper();
                            if (s2 == "Y")
                            {
                                costentry2:
                                Console.Write("Node {0} and {1} are connected.Enter cost:", col + 1, row + 1); ;
                                conn[col, row] = 1;
                                if (Double.TryParse(Console.ReadLine(), out cost))
                                {
                                    costM[col, row] = cost;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Invalid entry:");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto costentry2;
                                }
                            }
                            else
                            {
                                conn[col, row] = 0;
                                costM[col, row] = double.PositiveInfinity;
                            }
                        }
                    }
                }
                row++;
            }
        }

       
        public static void Correct_matrices(double[,] costM, int[,] conn)
        {
            Console.Write("Enter node to node connection/cost (separated by ,) you wish to edit:");
            string[] nodes = Console.ReadLine().Split(',');
            int r = Int32.Parse(nodes[0]) - 1;
            int c = Int32.Parse(nodes[1]) - 1;
            double cost = 0;

            //change connection and/or cost
            if (conn[r, c] == 1)
            {
                conn[r, c] = 0;
                costM[r, c] = double.PositiveInfinity;
                Console.WriteLine("Node {0} to node {1} is now unconnected.", r + 1, c + 1);

            }
            else
            {

                conn[r, c] = 1;
                Console.WriteLine("Node {0} to node {1} is now connected.Input cost: ", r + 1, c + 1);
                if (Double.TryParse(Console.ReadLine(), out cost))
                {
                    costM[r, c] = cost;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid entry:");
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }

        }

        /// <summary>
        /// Print the matrices
        /// </summary>
        /// <param name="costM">
        /// Cost matrix
        /// </param>
        /// <param name="conn">
        /// Ajacency matrix
        /// </param>
        public static void Print_matrix(double[,] costM, int[,] conn)
        {

            int len = conn.GetLength(0);
            string inf = "\u221E";
            Console.WriteLine("COST MATRIX");
            for (int i = 0; i < len; i++)
            {
                Console.Write("{0,5}", i + 1);
            }
            Console.WriteLine();
            for (int row = 0; row < len; row++)
            {
                Console.Write("{0}  ", row + 1);
                for (int col = 0; col < len; col++)
                {
                    if (costM[row, col] == Double.PositiveInfinity)
                    {
                        Console.Write("{0,-4} ", inf);
                    }
                    else { Console.Write("{0,4:f2} ", costM[row, col]); }

                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            //Collect number of nodes
            int Nodes = 10;
            int[,] Connection_matrix;
            double[,] Cost_matrix;
            double[,] Cost_Source = new double[Nodes, 2];

            int[] status = new int[Nodes];
            status[0] = 1;

          

            int[,] The10nodesConn =  
             {
                 { 0,1,1,1,0,0,0,0,0,0},
                 { 0,0,0,1,1,0,0,0,0,0},
                 { 0,0,0,0,1,0,1,0,0,0},
                 { 0,0,0,0,1,1,1,0,0,0},
                 { 0,0,0,0,0,0,1,1,0,0},
                 { 0,0,0,0,0,0,1,0,1,0},
                 { 0,0,0,0,0,0,0,1,1,1},
                 { 0,0,0,0,0,0,0,0,0,1},
                 { 0,0,0,0,0,0,0,0,0,1},
                 { 0,0,0,0,0,0,0,0,0,0},

            };

            double[,] The10nodesCost =  
            {
                 { 0,4,5,8,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity},
                 { double.PositiveInfinity,0,double.PositiveInfinity,3,12,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity},
                 { double.PositiveInfinity,double.PositiveInfinity,0,1,double.PositiveInfinity,11,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,9,4,10,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,double.PositiveInfinity,6,10,double.PositiveInfinity,double.PositiveInfinity},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,5,double.PositiveInfinity,11,double.PositiveInfinity},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,3,5,15},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,double.PositiveInfinity,14},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0,8},
                 { double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,double.PositiveInfinity,0},

            };



           
            Connection_matrix = The10nodesConn;
            Cost_matrix = The10nodesCost;

            Print_matrix(Cost_matrix, Connection_matrix);
            bool startDij = false;

            while (!startDij)
            {
                act:
                Console.Write("\nPress: \n1 to correct matrices \n2 to start calculation \n>> ");
                string input = Console.ReadLine();
                int action = 1;
                if (Int32.TryParse(input, out action))
                {
                    switch (action)
                    {
                        case 1:
                            Correct_matrices(Cost_matrix, Connection_matrix);
                            goto act;
                        case 2:
                            Print_matrix(Cost_matrix, Connection_matrix);
                            startDij = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Invalid entry.");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto act;

                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid entry.");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto act;

                }
            }

            DijskstraMethods dij = new DijskstraMethods();

            int alreadyP = 1;
            int nextP = 0;
            int currentP = 0;
            int possibleP = 0;

            ///This list will store all currently temporarary nodes
            List<int> Tnodes = new List<int>();

            while (alreadyP < Nodes)
            {
                Tnodes.Remove(currentP);
                List<int> neb = dij.NextNodes(currentP, Connection_matrix, status);
                if (neb.Count!=0)
                {
                    for (int i = 0; i < neb.Count; i++)
                    {
                        if (!(Tnodes.Contains(neb[i])))
                        {
                            Tnodes.Add(neb[i]);
                        }
                    }

                    nextP = dij.FindNextPnode(currentP,Tnodes,Connection_matrix,Cost_matrix,Cost_Source,status);
                 
                    currentP = nextP;
                    alreadyP++;

                }
                else
                {
                 
                    double minCost = Cost_Source[Tnodes[0], 0];
                    possibleP = Tnodes[0];

                    for (int node_index = 0; node_index < Tnodes.Count; node_index++)
                    {
                        if (Cost_Source[Tnodes[node_index], 0] < minCost)
                        {
                            minCost = Cost_Source[Tnodes[node_index], 0];
                            possibleP = Tnodes[node_index];
                        }
                    }

                    Tnodes.Remove(possibleP);
                    nextP = possibleP;
                    status[nextP] = 1;
                    currentP = nextP;
                    alreadyP++;                
                    
                }
             
            }

            Console.WriteLine("\nNODE COST SOURCE STATUS");
            Console.WriteLine("1    0     0     P");
            for (int row = 1; row < Cost_Source.GetLength(0); row++)
            {
                Console.Write("{0} ", row + 1);
                for (int col = 0; col < Cost_Source.GetLength(1); col++)
                {
                    int source = (int)Cost_Source[row, col];
                    if (col == 0)
                    {
                        Console.Write("{0,5} ", Cost_Source[row, col]); //get cost

                    }
                    else
                    {
                        Console.Write("{0,4}", source + 1); //get source
                        Console.Write("     P");

                    }

                }
                Console.WriteLine();
            }

            while (true)
            {

                Console.WriteLine("\n>> Path format: (node) <---- [cummulative cost,source node]  << \n");
                dest:
                Console.Write("Enter destination: ");
                if (int.TryParse(Console.ReadLine(), out int destination))
                {
                    if (destination - 1 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You are still at the starting node.Enter another destination.");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto dest;
                    }
                    else if (destination - 1 < Nodes && destination - 1 >= 0)
                    {
                        List<int> path = dij.TracePath(0, destination - 1, Cost_Source);
                        int nexti = 0;
                        for (int i = 0; i < path.Count; i++)
                        {
                            if (i != 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("  <----  ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            if (i == path.Count - 1)
                            {
                                nexti = -1;
                            }
                            else { nexti = path[i + 1]; }
                            Console.Write("( {0} )", path[i] + 1);
                         
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("  <----  ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("[ {0},{1} ]", Cost_Source[path[i], 0], nexti + 1);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Node does not exist.");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto dest;
                    }

                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid entry.");
                    Console.ForegroundColor = ConsoleColor.White;
                    goto dest;
                }

                Console.WriteLine("\nTotal cost is {0} units", Cost_Source[destination - 1, 0]);

            }

        }
    }
}
