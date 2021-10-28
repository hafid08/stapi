//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace STApi.Data
//{
//    public class DbInitializer : IDbInitializer
//    {

//        private readonly DataContext dataContext;
//        private readonly ApplicationContext applicationContext;
//        public DbInitializer(DataContext dataContext, ApplicationContext applicationContext)
//        {
//            this.dataContext = dataContext;
//            this.applicationContext = applicationContext;
//        }
//        public void ApplicationInitializeAsync()
//        {
//            Console.WriteLine("Application Initilization...");
//            applicationContext.Database.EnsureDeleted();
//            applicationContext.Database.Migrate();
//        }

//        public void DataInitializeAsync()
//        {
//            Console.WriteLine("Data Initilization...");
//            dataContext.Database.EnsureDeleted();
//            dataContext.Database.Migrate();
//        }
//    }
//}