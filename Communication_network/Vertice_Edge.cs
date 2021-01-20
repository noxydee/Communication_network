using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_network
{
        struct IP_Addreess_3v
        {
            int first_okt;
            int second_okt;
            int third_okt;
            public string whole_addr;
            string whole_addr_with_zero;

            static string[] segments = new string[3];

            public IP_Addreess_3v(string whole_addr)
            {
                this.whole_addr = whole_addr;
                segments = whole_addr.Split(".");
                first_okt = Convert.ToInt32(segments[0]);
                second_okt = Convert.ToInt32(segments[1]);
                third_okt = Convert.ToInt32(segments[2]);
                whole_addr_with_zero = "";
                whole_addr_with_zero = build_ip(whole_addr);
            }
            private string build_ip(string whole_addr)
            {
                    string[] zeros = new string[] { "", "0", "00" };
                    string ip_3 = zeros[count_zeros(first_okt)] + first_okt + "." + zeros[count_zeros(second_okt)] + second_okt + "." + zeros[count_zeros(third_okt)] + third_okt;
                    return ip_3;
            }
            private int count_zeros(int okt)
            {
                if (okt > 99)
                {
                    return 0;
                }
                else if (okt > 9)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }

        class Edge
        {
            public Vertice From;
            public Vertice To;

            public long Speed;

            public Edge(Vertice From, Vertice To,string Speed)
            {
                this.From = From;
                this.To = To;
                this.Speed = 0;
                this.Speed = get_speed(Speed);
            }
        private long get_speed(string Speed)
        {
            try
            {
                char x = Speed[Speed.Length - 1];

                if (x.Equals('M'))
                {
                    return 100000/Convert.ToInt64(Speed.Remove(Speed.Length-1, 1));
                }
                else if (x.Equals('G'))
                {
                    return 100 / Convert.ToInt64(Speed.Remove(Speed.Length-1, 1));
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        }
        
        class Vertice
        {
            public List<Edge> edges;
            public IP_Addreess_3v addr;
            public Vertice(IP_Addreess_3v whole_addr)
            {
                addr = whole_addr;
                edges = new List<Edge>();
            }
        }
}
