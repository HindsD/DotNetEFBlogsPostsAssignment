using System;
using System.IO;
using NLog.Web;
using System.Linq;


namespace DotNetEFLAB
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");
            string choice;
            var db = new BloggingContext();
            
            do
            {
                Console.WriteLine("\nEnter your selection:");
                Console.WriteLine("1) Display all blogs"); //asks the user what they'd like to do
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("4) Display Posts");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();

                if (choice == "1"){
                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);
                    Console.WriteLine("");
                    Console.WriteLine((query.Count()-1)+" Blogs returned:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                else if (choice == "2"){
                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();
                    if(name == null || name == ""){
                        throw new Exception("Blog name cannot be null");
                    }
                    else{
                        var blog = new Blog { Name = name };

                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                    }
                }

            }while(choice == "1" || choice == "2" || choice == "3" || choice == "4");

            logger.Info("Program ended");
        }
    }
}
