using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            int choice = 0;
            
            
            var db = new BloggingContext();

            while (choice != -1){
                Console.WriteLine("1) Display all blogs \n2) Add Blog \n3) Create Post \n4) Display Posts\n-1) exit the program");
                choice = Int32.Parse(Console.ReadLine());


                switch (choice)
            {
                case 1:
                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                    break;

                case 2:
                    try
                    {
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);

                        
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    break;

                case 3:

                    break;

                case 4:

                    break;


            }
            }
            



            logger.Info("Program ended");
        }
    }
}
