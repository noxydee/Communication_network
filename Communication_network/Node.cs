using System;
using System.Collections.Generic;
using System.Text;

namespace Communication_network
{
    struct IP_Addreess
    {
        public int first_okt;
        public int second_okt;
        public int third_okt;
        public int fourth_okt;
        public string whole_addr;
        public string first_3_okts;
        public string whole_addr_with_zeros;

        static string[] segments = new string[4];

        public IP_Addreess(string whole_addr)
        {
            this.whole_addr = whole_addr;
            segments = whole_addr.Split(".");
            first_okt = Convert.ToInt32(segments[0]);
            second_okt = Convert.ToInt32(segments[1]);
            third_okt = Convert.ToInt32(segments[2]);
            fourth_okt = Convert.ToInt32(segments[3]);
            first_3_okts = "";
            whole_addr_with_zeros = "";
            first_3_okts = build_ip_v3();
            whole_addr_with_zeros = build_ip();
        }
        public string build_ip()
        {
            string[] zeros = new string[] { "", "0", "00" };
            string ip = zeros[count_zeros(first_okt)] + first_okt + "." + zeros[count_zeros(second_okt)] + second_okt + "." + zeros[count_zeros(third_okt)] + third_okt + "." + zeros[count_zeros(fourth_okt)] + fourth_okt;
            return ip;
        }
        public string build_ip_v3()
        {
            string[] zeros = new string[] { "", "0", "00" };
            string ip_3 = zeros[count_zeros(first_okt)] + first_okt + "." + zeros[count_zeros(second_okt)] + second_okt + "." + zeros[count_zeros(third_okt)] + third_okt;
            return ip_3;
        }

        public int count_zeros(int okt)
        {
            if(okt>99){
                return 0;
            }
            else if(okt>9){
                return 1;
            }
            else{
                return 2;
            }
        }
        public IP_Addreess_3v convert_to_ipv3(IP_Addreess addr)
        {
            string ip_addr = addr.first_okt+"."+addr.second_okt+"."+addr.third_okt;
            IP_Addreess_3v owo_addr = new IP_Addreess_3v(ip_addr);
            return owo_addr;
        }
    }
    class Node
    {
        public IP_Addreess ip_addr;
        public Node Left_Node;
        public Node Right_Node;
        public Node(string ip_addr)
        {
            this.ip_addr = new IP_Addreess(ip_addr);
        }
    }
}
