using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijskstra
{
    class DijskstraMethods
    {/// <summary>
     /// This method will find the list of temperorary nodes that can be reached from the current permanent node.
     /// </summary>
     /// <param name="currentPnode">
     /// Permanent node as at current iteration.
     /// </param>
     /// <param name="connMatrix">
     /// Adjacency matrix (1 for connected, 0 for otherwise).
     /// </param>
     /// <param name="status">
     /// Status of node (1 for permanent , 0 for temporary) .
     /// </param>
     /// <returns>
     /// List of temperorary nodes.
     /// </returns>
        public List<int> NextNodes(int currentPnode, int[,] connMatrix, int[] status)
        {
            int length = connMatrix.GetLength(0);

            List<int> thenodes = new List<int>();

            for (int destination = 0; destination < length; destination++)
            {

                //check that destination is not already permanent
                if ((connMatrix[currentPnode, destination] == 1) && (status[destination] != 1))
                {
                    thenodes.Add(destination);
                }
            }

            return thenodes;
        }

        /// <summary>
        /// This method will find the next permanent node to be used in the next iteration.
        /// </summary>
        /// <param name="currentPnode">
        /// Permanent node as at current iteration.
        /// </param>
        /// <param name="tempnodes">
        /// List of all temporary nodes as at current iteration.
        /// </param>
        /// <param name="connM">
        ///  Adjacency matrix (1 for connected, 0 for otherwise).
        /// </param>
        /// <param name="costM">
        /// Matrix containing costs between all the nodes (-- ∞ for infinite cost).
        /// </param>
        /// <param name="CO">
        /// Will store the cummulative costs for each node and the origin nodes
        /// </param>
        /// <param name="status">
        /// Status of node (1 for permanent , 0 for temporary) .
        /// </param>
        /// <returns>
        /// Permanent node to be used in the next iteration.
        /// </returns>
        public int FindNextPnode(int currentPnode, List<int> tempnodes, int[,] connM, double[,] costM, double[,] CO, int[] status)
        {
            int nextPnode = 0;
            List<double> costlist = new List<double>();
            double cost_up_to_cpn = CO[currentPnode, 0];
            double sum = 0;
            int x = tempnodes.Count;
            for (int node_index = 0; node_index < x; node_index++)
            {
                //cummulative cost
                sum = costM[currentPnode, tempnodes[node_index]] + cost_up_to_cpn;
                costlist.Add(sum); 

                if ((sum <= CO[tempnodes[node_index], 0]) || ((sum > CO[tempnodes[node_index], 0]) && (CO[tempnodes[node_index], 0] == 0)))
                {
                    costlist[node_index] = sum;
                    CO[tempnodes[node_index], 1] = currentPnode;
                    CO[tempnodes[node_index], 0] = sum;
                }

                else
                {
                    costlist[node_index] = CO[tempnodes[node_index], 0];
                }
            }

            double minCost = costlist.Min();
            for (int node_index = 0; node_index < tempnodes.Count; node_index++)
            {
                if (costlist[node_index] == minCost)
                {
                    nextPnode = tempnodes[node_index];
                    status[nextPnode] = 1;
                }
            }
            return nextPnode;
        }

        /// <summary>
        /// This method will trace the path from node 1 to destination node
        /// </summary>
        /// <param name="source">
        /// Origin node.
        /// </param>
        /// <param name="destination">
        /// Destination node.
        /// </param>
        /// <param name="CS">
        ///  Will store the cummulative costs for each node and the source nodes.
        ///  </param>
        /// <returns>
        /// The path cosisting of nodes from source to destiation.
        /// </returns>
        public List<int> TracePath(int source, int destination, double[,] CS)
        {
            List<int> path = new List<int>();
            path.Add(destination);
            while (destination > 0)
            {
                path.Add((int)CS[destination, 1]);
                destination = (int)CS[destination, 1];
            }
            return path;
        }
    }
}
