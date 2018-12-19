using TaskManager.DAO;

namespace TaskManager.Services.App_Start
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.AddRegistry<ServicesRegistry>();
                //x.For<ILogger>().Use<TextFileLogger>().Singleton();
            });

            var log = ObjectFactory.Container.WhatDoIHave();
        }
    }
    public class ServicesRegistry : StructureMap.Registry
    {
        public ServicesRegistry()
        {
            Scan(x =>
            {
                x.Assembly("TaskManager.BL");
                x.Assembly("TaskManager.DAO");
                x.Assembly("TaskManager.API");
                x.WithDefaultConventions();
            });

            For(typeof(IRepositoryDAO<>)).Use(typeof(RepositoryDAO<>));
        }
    }
}