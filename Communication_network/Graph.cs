using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace Communication_network
{
    class Graph
    {
        Vertice[] vertices = new Vertice[100000];

        bool from_not_found;
        bool to_not_found;

        int added = 0;

        public Graph()
        {

        }
        
        private bool check_if_exist(IP_Addreess_3v ip_addr)
        {
            try
            {
                foreach (Vertice vert in vertices)
                {
                    if (vert.addr.whole_addr == ip_addr.whole_addr)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void Add_Connection_To_Vertice(string fromx,string tox, String Speedx)
        {
            try
            {
                IP_Addreess_3v from = new IP_Addreess_3v(fromx);
                IP_Addreess_3v to = new IP_Addreess_3v(tox);

                IP_Addreess_3v x = new IP_Addreess_3v("0.0.0.0");

                Vertice temp_from = new Vertice(x);
                Vertice temp_to = new Vertice(x);
                
                foreach(Vertice v in vertices)
                {
                    if(v!=null)
                    {
                        if (v.addr.whole_addr == from.whole_addr)
                        {
                            temp_from = v;
                            //v.edges.Add(from_e);
                        }
                        else if (v.addr.whole_addr == to.whole_addr)
                        {
                            temp_to = v;
                            //v.edges.Add(to_e);
                        }
                    }
                    
                }

                Edge from_e = new Edge(temp_from, temp_to, Speedx);
                Edge to_e = new Edge(temp_to, temp_from, Speedx);

                foreach (Vertice v in vertices)
                {
                    if(v!=null)
                    {
                        if (v.addr.whole_addr == from.whole_addr)
                        {
                            v.edges.Add(from_e);
                        }
                        else if (v.addr.whole_addr == to.whole_addr)
                        {
                            v.edges.Add(to_e);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("add_connec");
            }
        }
        public void Add_Vertice_To_Grpahv1(string addr)
        {
            try
            {
                IP_Addreess x = new IP_Addreess(addr);
                string addr_y = x.first_okt+"."+x.second_okt+"."+x.third_okt;
                //Console.WriteLine(addr_y+" <-- ");

                IP_Addreess_3v addr_x = new IP_Addreess_3v(addr_y);
                Add_Vertice_To_Graph(addr_x);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private void Add_Vertice_To_Graph(IP_Addreess_3v addr)
        {
            try
            {
                Vertice x = new Vertice(addr);
                vertices[added] = x;
                added += 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Write_Down_Vertices()
        {
            try
            {
                foreach(Vertice x in vertices)
                {
                    Console.WriteLine(x.addr.whole_addr);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Write_Connections_to(string addr)
        {
            IP_Addreess_3v addr_x = new IP_Addreess_3v(addr);
            foreach(Vertice v in vertices)
            {
                if(v!=null)
                {
                    if (v.addr.whole_addr == addr_x.whole_addr)
                    {
                        foreach (Edge e in v.edges)
                        {
                            Console.WriteLine(e.From.addr.whole_addr);
                            Console.WriteLine(e.To.addr.whole_addr);
                            Console.WriteLine(e.Speed);
                        }
                    }
                }
            }
        }

        public void Delete_connection_from_graph(string addr1,string addr2)
        {
            try
            {
                IP_Addreess_3v addr1_x = new IP_Addreess_3v(addr1);
                IP_Addreess_3v addr2_x = new IP_Addreess_3v(addr2);
                bool From_success = false;
                bool To_success = false;

                foreach(Vertice v in vertices)
                {
                    if(v.addr.whole_addr == addr1_x.whole_addr)
                    {
                        foreach(Edge e in v.edges)
                        {
                            if(e.From.addr.whole_addr==addr1_x.whole_addr&&e.To.addr.whole_addr==addr2_x.whole_addr)
                            {
                                v.edges.Remove(e);
                                From_success = true;
                            }
                        }
                    }
                    else if(v.addr.whole_addr == addr2_x.whole_addr)
                    {
                        foreach (Edge e in v.edges)
                        {
                            if (e.From.addr.whole_addr == addr2_x.whole_addr && e.To.addr.whole_addr == addr1_x.whole_addr)
                            {
                                v.edges.Remove(e);
                                To_success = true;
                            }
                        }
                    }
                }
                if (From_success == false || To_success == false)
                {
                    throw new Exception("Nie usunieto polaczenia poprawnie");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Dijkstra_pass(string ip_addr1,string ip_addr2)
        {
            try
            {
                IP_Addreess addr_v4_from = new IP_Addreess(ip_addr1);
                IP_Addreess addr_v4_to = new IP_Addreess(ip_addr2);

                IP_Addreess_3v from_addr = addr_v4_from.convert_to_ipv3(addr_v4_from);
                IP_Addreess_3v to_addr = addr_v4_to.convert_to_ipv3(addr_v4_to);

                bool from_success = false;
                bool to_success = false;

                Vertice from = new Vertice(from_addr);
                Vertice to = new Vertice(to_addr);

                foreach (Vertice v in vertices)
                {
                    if(v!=null)
                    {
                        if (v.addr.whole_addr == from_addr.whole_addr)
                        {
                            from = v;
                            from_success = true;
                        }
                        else if (v.addr.whole_addr == to_addr.whole_addr)
                        {
                            to = v;
                            to_success = true;
                        }
                    }
                }
                if(from_success==false || to_success == false)
                {
                    throw new Exception("NIE");
                }
                //Console.WriteLine($"wykonanie dijkstry z {from.addr.whole_addr} do {to.addr.whole_addr}");
                //Write_Connections_to(from.addr.whole_addr);
                //Console.WriteLine(" ");
                Console.WriteLine(Dijkstra(from,to));
                //Console.WriteLine(" ");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private long Dijkstra(Vertice start, Vertice end)
        {
            try
            {
                Dictionary<Vertice, long> Distance_from_start = new Dictionary<Vertice, long>();
                Dictionary<Vertice, Vertice> Previous_Vertice = new Dictionary<Vertice, Vertice>();
                SimplePriorityQueue<Vertice> Q = new SimplePriorityQueue<Vertice>();

                IP_Addreess_3v xx = new IP_Addreess_3v("0.0.0.0");
                Vertice x = new Vertice(xx);

                foreach(Vertice v in vertices)
                {
                    if(v!=null)
                    {
                        Distance_from_start[v] = int.MaxValue;
                        Previous_Vertice[v] = x;
                        Distance_from_start[start] = 0;
                        if (Q.Contains(v) == false)
                        {
                            Q.Enqueue(v, Distance_from_start[v]);
                        }
                    }
                }

                while(Q.Count != 0)
                {
                    Vertice pos = Q.Dequeue();

                    foreach(Vertice e in vertices)
                    {
                        if (e != null)
                        {
                            foreach (Edge ee in e.edges)
                            {
                                if (ee != null)
                                {
                                    long alt = Distance_from_start[e] + ee.Speed;
                                    if (alt < Distance_from_start[ee.To])
                                    {
                                        Distance_from_start[ee.To] = alt;
                                        Previous_Vertice[ee.To] = pos;
                                        Q.UpdatePriority(ee.To, alt);
                                    }
                                }
                            }
                        }
                    }
                }
                return Distance_from_start[end];   
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            
            
        }


    }
}
