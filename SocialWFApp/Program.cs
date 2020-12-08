using BusinessLogic.Concrete;
using BusinessLogic.Interface;
using MongoDal.Concrete;
using MongoDal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using SocialConsoleApp.AppUser;
using SocialConsoleApp.Menu;
using Neo4jDal.Concrete;
using Neo4jDal.Interface;

namespace SocialWFApp
{
    class ProgramApp
    {
        public static UnityContainer Container;
        public static string conctString;
        public static string DBname;

        public static string conctNeo4j, login, pasw;
        static void Main()
        {
            conctString = "mongodb://localhost:30177/";
            DBname = "SocialApp";
            conctNeo4j = "http://localhost:7474/db/data/";
            login = "neo4j";
            pasw = "123456";
            ConfigureContainer();


            AppMenu.ShowEntry();

        }

        private static void ConfigureContainer()
        {
            Container = new UnityContainer();
            Container.RegisterInstance<IAppUser>(new SocialWFApp.AppUser.AppUser());
            Container.RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IPostManager, PostManager>()
                .RegisterType<IUserManager, UserManager>();
            Container
                .RegisterType<IUserDal, UserDal>(new InjectionConstructor(conctString, DBname))
                .RegisterType<IPostDal, PostDal>(new InjectionConstructor(conctString, DBname));
            Container
              .RegisterType<IFollowerDal, FollowerDal>(new InjectionConstructor(conctNeo4j, login, pasw));
        }
    }
}
