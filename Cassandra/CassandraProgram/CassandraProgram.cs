using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic;
using DTO.Cassandra;


namespace CassandraProgram
{

    public class CassandraProgram
    {

        static void Main(string[] args)
        {
            CassandraUser cu = new CassandraUser();

            Console.WriteLine("Enter Email");
            var em = Console.ReadLine();
            cu.Login(em);

            cu.ReadAllPosts();
            Menu(em, cu);

            Console.ReadKey();

        }
        public static void Menu(string e, CassandraUser cu)
        {
            Console.WriteLine("Do you want to:\n 1-Find user \n " +
                "2-Write post\n" +
                "3-Find post\n" +
                "4-Exit");
            var x = Console.ReadLine();
            switch (x)
            {
                case "1":
                    Console.WriteLine("Write user's name");
                    string N = Console.ReadLine();
                    Console.WriteLine("Write user's surname");
                    string S = Console.ReadLine();

                    cu.ToFollow(N, S, e);
                    Menu(e, cu);

                    break;
                case "3":
                    Console.WriteLine("Write user's name");
                    N = Console.ReadLine();
                    Console.WriteLine("Write user's surname");
                    S = Console.ReadLine();
                    cu.PostReaction(N, S, e);
                    Menu(e, cu);
                    break;
                case "2":
                    var post = cu.CreatePost(e);
                    Menu(e, cu);
                    break;
                case "4":
                    Console.WriteLine("Exit");
                    Thread.Sleep(1000);
                    System.Environment.Exit(20);
                    break;
            }
        }

    }

}