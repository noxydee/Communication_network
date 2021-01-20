using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Communication_network
{
    class AVL_TREE
    {
        public AVL_TREE()
        {

        }
        public Node Root;

        public void Add_ip_address(string ip_addr)
        {
            try
            {
                Node node_X = new Node(ip_addr);
                if (Root == null)
                {
                    Root = node_X;
                }
                else
                {
                    Root = Insert_ip_address(Root, node_X);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public int Check_if_bigger(IP_Addreess addr1, IP_Addreess addr2)
        {
            return String.Compare(addr1.whole_addr_with_zeros, addr2.whole_addr_with_zeros);
        }
        public int Check_if_bigger_v3(IP_Addreess addr1,IP_Addreess addr2)
        {
            return String.Compare(addr1.first_3_okts, addr2.first_3_okts);
        }
        public Node Insert_ip_address(Node position, Node node_X)
        {
            try
            {
                if (position == null)
                {
                    position = node_X;
                    return position;
                }
                else if (Check_if_bigger(node_X.ip_addr, position.ip_addr) < 0)
                {
                    position.Left_Node = Insert_ip_address(position.Left_Node, node_X);
                    position = balance(position);
                }
                else if (Check_if_bigger(node_X.ip_addr, position.ip_addr) > 0)
                {
                    position.Right_Node = Insert_ip_address(position.Right_Node, node_X);
                    position = balance(position);
                }
                return position;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private int height_branch(Node node_X)
        {
            try
            {
                int height = 0;
                if (node_X != null)
                {
                    height = Math.Max(height_branch(node_X.Left_Node), height_branch(node_X.Right_Node))+1;
                }
                return height;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        private int weight_branch(Node node_X)
        {
            try
            {
                return height_branch(node_X.Left_Node)- height_branch(node_X.Right_Node);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        private Node balance(Node node_X)
        {
            try
            {
                int weight = weight_branch(node_X);
                if (weight > 1)
                {
                    if(weight_branch(node_X.Left_Node)>0)
                    {
                        node_X = Rotate(node_X, "LL");
                    }
                    else
                    {
                        node_X = Rotate(node_X, "LR");
                    }
                }
                else if(weight<-1)
                {
                    if(weight_branch(node_X.Right_Node)>0)
                    {
                        node_X = Rotate(node_X, "RL");
                    }
                    else
                    {
                        node_X = Rotate(node_X, "RR");
                    }
                }
                return node_X;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private Node Rotate (Node node_X,string dir)
        {
            try
            {
                switch(dir)
                { 
                    case "RL":
                    {
                        Node piv = node_X.Right_Node;
                        node_X.Right_Node = Rotate(piv, "LL");
                        return Rotate(node_X, "RR"); 
                    }
                    case "RR":
                    {
                        Node piv = node_X.Right_Node;
                        node_X.Right_Node = piv.Left_Node;
                        piv.Left_Node = node_X;
                        return piv;   
                    }
                    case "LR":
                    {
                        Node piv = node_X.Left_Node;
                        node_X.Left_Node = Rotate(piv, "RR");
                        return Rotate(node_X, "LL");    
                    }
                    case "LL":
                    {
                        Node piv = node_X.Left_Node;
                        node_X.Left_Node = piv.Right_Node;
                        piv.Right_Node = node_X;
                        return piv;    
                    }
                    default:
                    {
                        return null;
                        throw new Exception("err");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //int count = 1;
        void print2D_fx(Node root, int space = 0,int count=1)
        { 
            if (root == null)
            {
                return;
            }
                
            space += count;
 
            print2D_fx(root.Right_Node, space);

            Console.Write("\n");
            for (int i = count; i < space; i++)
            {
                Console.Write("         ");
            }
            Console.Write(root.ip_addr.whole_addr + "\n");

            print2D_fx(root.Left_Node, space);
        }

        public void print2D()
        {
            print2D_fx(Root, 0);
        }

        public void Delete(string wanted_ip_addr)
        {
            IP_Addreess wanted_ip = new IP_Addreess(wanted_ip_addr);
            Root = Delete_n(Root, wanted_ip);
        }
        private Node Delete_n(Node position,IP_Addreess wanted_ip_addr)
        {
            try
            {
                Node node_X;
                if (position == null)
                {
                    return null;
                }
                else
                {
                    if(Check_if_bigger(wanted_ip_addr,position.ip_addr)<0)
                    {
                        position.Left_Node = Delete_n(position.Left_Node, wanted_ip_addr);
                        if(weight_branch(position)== -2)
                        {
                            if(weight_branch(position.Right_Node)<=0)
                            {
                                position = Rotate(position, "RR");
                            }
                            else
                            {
                                position = Rotate(position, "RL");
                            }
                        }
                    }
                    else if(Check_if_bigger(wanted_ip_addr,position.ip_addr)>0)
                    {
                        position.Right_Node = Delete_n(position.Right_Node, wanted_ip_addr);
                        if(weight_branch(position)==2)
                        {
                            if(weight_branch(position.Left_Node)>=0)
                            {
                                position = Rotate(position, "LL");
                            }
                            else
                            {
                                position = Rotate(position, "LR");
                            }
                        }
                    }
                    else
                    {
                        if(position.Right_Node != null)
                        {
                            node_X = position.Right_Node;
                            while (node_X.Left_Node != null)
                            {
                                node_X = node_X.Left_Node;
                            }
                            position.ip_addr = node_X.ip_addr;
                            position.Right_Node = Delete_n(position.Right_Node, node_X.ip_addr);
                            if(weight_branch(position)==2)
                            {
                                if(weight_branch(position.Left_Node)>=0)
                                {
                                    position = Rotate(position, "LL");
                                }
                                else
                                {
                                    position = Rotate(position, "LR");
                                }
                            }
                        }
                        else
                        {
                            return position.Left_Node;
                        }
                    }
                }
                return position;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public bool could_not_find;
        public int catfish = 0;
        public void Search_for(string ip_addr_y)
        {
            try
            {
                could_not_find = false;
                IP_Addreess ip_addr_x = new IP_Addreess(ip_addr_y);
                Node node_X = Search_for_ip(Root, ip_addr_x);
                if (node_X != null)
                {
                    if (node_X.ip_addr.whole_addr == ip_addr_x.whole_addr)
                    {
                        Console.WriteLine("TAK");
                    }
                }
                else if(could_not_find == true)
                {
                    Console.WriteLine("NIE");
                }         
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private Node Search_for_ip(Node position,IP_Addreess wanted_ip_addr)
        {
            try
            {
                if(Check_if_bigger(wanted_ip_addr,position.ip_addr)<0 && position.Left_Node!=null)
                {
                    return Search_for_ip(position.Left_Node, wanted_ip_addr);
                }
                else if(Check_if_bigger(wanted_ip_addr,position.ip_addr)>0 && position.Right_Node!=null)
                {
                    return Search_for_ip(position.Right_Node,wanted_ip_addr);
                }
                else if(Check_if_bigger(wanted_ip_addr,position.ip_addr)==0)
                {
                    return position;
                }
                else
                {
                    //throw new Exception("error search 03");
                    could_not_find = true;
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public void Count_ip(string ip_addr_y)
        {
            try
            {
                IP_Addreess ip_addr_x = new IP_Addreess(ip_addr_y);
                Node node_X = Search_for_ip_3_okts(Root, ip_addr_x);
                if(node_X != null)
                { 
                    Count_ip_addr_lower_than(node_X, ip_addr_x);
                    Console.WriteLine(catfish);
                    catfish = 0;
                }
                else
                {
                    Console.WriteLine("0");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private Node Search_for_ip_3_okts(Node position,IP_Addreess wanted_ip_addr)
        {
            try
            {
                if (Check_if_bigger_v3(wanted_ip_addr, position.ip_addr) < 0 && position.Left_Node != null)
                {
                    return Search_for_ip_3_okts(position.Left_Node, wanted_ip_addr);
                }
                else if (Check_if_bigger_v3(wanted_ip_addr, position.ip_addr) > 0 && position.Right_Node != null)
                {
                    return Search_for_ip_3_okts(position.Right_Node, wanted_ip_addr);
                }
                else if (Check_if_bigger_v3(wanted_ip_addr, position.ip_addr) == 0)
                {
                    return position;
                }
                else
                {
                    //throw new Exception("error search 03");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private void Count_ip_addr_lower_than(Node node_X,IP_Addreess ip_addr)
        {
            try
            {
                if (node_X == null)
                {
                    
                }
                else 
                {
                    if(node_X.ip_addr.first_3_okts==ip_addr.first_3_okts)
                    {
                        catfish += 1;
                    }
                    Count_ip_addr_lower_than(node_X.Right_Node, ip_addr);
                    Count_ip_addr_lower_than(node_X.Left_Node, ip_addr);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
