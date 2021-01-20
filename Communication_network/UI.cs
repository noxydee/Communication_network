using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;


namespace Communication_network
{
    class UI
    {
        AVL_TREE tree = new AVL_TREE();
        Graph graph = new Graph();
        

        public string[] Lines = File.ReadAllLines("projekt3_in7.txt");

        public void run()
        {
            try
            {
                foreach (string line in Lines.Skip(1))
                {
                    string[] word = new string[4];
                    word = line.Split(" ");

                    switch (word[0])
                    {
                        case "DK":
                        {
                            tree.Add_ip_address(word[1]);
                            graph.Add_Vertice_To_Grpahv1(word[1]);
                            break;
                        }
                        case "UK":
                        {
                            tree.Delete(word[1]);
                            break;
                        }
                        case "WK":
                        {
                            tree.Search_for(word[1]);
                            break;
                        }
                        case "LK":
                        {
                            word[1] = word[1] + ".000";
                            tree.Count_ip(word[1]);
                            break;
                        }
                        case "WY":
                        {
                            tree.print2D();
                            break;
                        }
                        case "DP":
                        {
                            graph.Add_Connection_To_Vertice(word[1], word[2], word[3]);
                            break;
                        }
                        case "UP":
                        {
                            graph.Delete_connection_from_graph(word[1], word[2]);
                            break;
                        }
                        case "NP":
                        {
                            graph.Dijkstra_pass(word[1], word[2]);
                            break;
                        }
                        case "NP2":
                        {
                            Console.WriteLine("NP2");
                            break;
                        }
                        default:
                        {
                            throw new Exception($"UI error -> command( {word[0]} )not found");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
