using CassandraDAL.Concrete;
using CassandraDAL.Interface;
using System;

using Unity;
using Unity.Injection;
using CassandraNetwork.Models;
using CassandraNetwork.Models.Concrete;
using CassandraNetwork.Models.Interfaces;

namespace CassandraNetwork
{
    public static class UnityConfig
    {
        public static IUser user = new AppUser();
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });
        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<IUser>(user);

            container.RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IPostManager, PostManager>()
                .RegisterType<IUserManager, UserManager>()
                .RegisterType<ISManager,SManager>();

            container.RegisterType<IUser, AppUser>()
                .RegisterType<IAppUserManager, AppUserManager>()
                .RegisterType<IAppAuthManager, AppAuthManager>()
                .RegisterType<IAppPostsManager, AppPostsManager>();


            container.RegisterType<IPostDALCassandra, PostDALCassandra>()
                .RegisterType<IUserDALCassandra, UserDALCassandra>()
                .RegisterType<IUserSDALCassandra, UserSDALCassandra>();

        }
    }
}