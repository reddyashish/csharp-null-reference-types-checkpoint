using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Training_3._9
{
    class Program
    {
        static public string DefaultConnectionString { get; } = @"Server=(localdb)\\mssqllocaldb;Database=SampleData-0B3B0919-C8B3-481C-9833-36C21776A565;Trusted_Connection=True;MultipleActiveResultSets=true";

        static IReadOnlyDictionary<string, string> DefaultConfigurationStrings { get; } = new Dictionary<string, string>()
        {
            ["Profile:UserName"] = Environment.UserName,
            [$"AppConfiguration:ConnectionString"] = DefaultConnectionString,
            ["Window:Height"] = "30",
            ["Window:Width"] = "30",
        };
        static public IConfiguration Configuration { get; set; }

        public class Person
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? PayRate { get; set; }
        }

        static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(DefaultConfigurationStrings);
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();

           // Console.SetWindowSize(Int32.Parse(Configuration["AppConfiguration:MainWindow:Width"]), (Int32.Parse(Configuration["AppConfiguration:MainWindow:Height"])));
            Console.WriteLine($"Hello {Configuration.GetValue<string>("Profile:UserName")}");
            Console.WriteLine($"Width: {Configuration.GetValue<string>("Window:Width")}");
            Console.WriteLine($"Height: {Configuration.GetValue<string>("Window:Height")}");

            int w = Int32.Parse(Configuration.GetValue<string>("Window:Width"));
            int h = Int32.Parse(Configuration.GetValue<string>("Window:Height"));

            Console.SetWindowSize(w, h);

            // Console.WriteLine($"{Configuration.GetValue<string>("AppConfiguration:MainWindow:Width")}");

            var person = new Person()
            { 
                FirstName = "Joe"
            };

            Console.WriteLine($"length: {person.FirstName.Length + person.LastName!.Length}");

            Console.ReadKey();
        }
    }
}
