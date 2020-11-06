﻿using System;
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

            while (choice != -1)
            {
                Console.WriteLine("1) Display all blogs \n2) Add Blog \n3) Create Post \n4) Display Posts\n-1) exit the program");
                choice = Int32.Parse(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        Console.WriteLine("All blogs in the database:");
                        DisplayBlogs(db);

                        break;

                    case 2:
                        try
                        {
                            // Create and save a new Blog
                            Console.Write("Enter a name for a new Blog: ");
                            string name = Console.ReadLine();

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
                        try
                        {
                            // Create and save a new Blog
                            Console.WriteLine("Select the blog you would like to post to: ");
                            DisplayBlogs(db);

                            int blogId = Int32.Parse(Console.ReadLine());

                            var check = db.Blogs.Where(b => b.BlogId == blogId);

                            if (check.Count() == 1)
                            {
                                Console.WriteLine("Enter the post title: ");
                                string title = Console.ReadLine();
                                
                                Console.WriteLine("Enter the post content:");
                                string content = Console.ReadLine();

                                var post = new Post { Title = title, Content = content, BlogId = blogId};

                                db.AddPost(post);
                                logger.Info("Post added - {title}", title);
                            }
                            else
                            {
                                logger.Error("There are no Blogs saved with that ID");
                            }

                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                        break;

                    case 4:

                        break;
                }
            }

            logger.Info("Program ended");
        }

        //needing to display the blogs is in more than one place 
        private static void DisplayBlogs(BloggingContext db)
        {
            // Display all Blogs from the database
            var query = db.Blogs.OrderBy(b => b.Name);

            foreach (var item in query)
            {
                Console.WriteLine(item.BlogId + " - " + item.Name);
            }
        }


    }
}
