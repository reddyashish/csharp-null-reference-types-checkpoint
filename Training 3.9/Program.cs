using System;
using System.Collections.Generic;
using System.Drawing;
using CsharpIndicesRangesCheckpoint;
using CsharpPatternMatchingCheckpoint;
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
        static public IConfiguration? Configuration { get; set; }

        public class Person
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? PayRate { get; set; }
        }

        public enum Rainbow
        {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet
        }

        public static int FromRainbow(Rainbow colorBand) =>
        colorBand switch
        {
            Rainbow.Red => Color.Red.ToArgb(),
            Rainbow.Orange => Color.Orange.ToArgb(),
            Rainbow.Yellow => Color.Yellow.ToArgb(),
            Rainbow.Green => Color.Green.ToArgb(),
            Rainbow.Blue => Color.Blue.ToArgb(),
            Rainbow.Indigo => Color.Indigo.ToArgb(),
            Rainbow.Violet => Color.Violet.ToArgb(),
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
        };

        public class Address
        {
            public string? Street { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Zip { get; set; }
        }

        public static decimal ComputeSalesTax(Address location, decimal salePrice) =>
        location switch
        {
            { State: "WA" } => salePrice * 0.06M,
            { State: "MN" } => salePrice * 0.75M,
            { State: "MI" } => salePrice * 0.05M,
            { State: "TX" } => salePrice * 0.08M,
            _ => 0M
        };

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

           // Console.WriteLine($"length: {person.FirstName.Length + person.LastName!.Length}");


            TestsBefore testsBefore = new TestsBefore();
            string[] result = testsBefore.getGradeRange(1, 3);

            foreach (var word in result)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine(FromRainbow(Rainbow.Blue));

            var locationObject = new Address() { City="Deluth", State = "TX", Zip="12345" };
            var result1 = locationObject is { State: "MN" };  // <-- Sets result to True
            Console.WriteLine(result1);
            result1 = locationObject is { State: "TX" };
            Console.WriteLine(result1);

            Console.WriteLine(ComputeSalesTax(locationObject,10.00M)) ;

            var passingStudent = new Student("Jane", "Doe", true, 'C');
            bool passResult = TestsBeforePatternMatching.getPassOrFail_ByPropertyPatternMatching(passingStudent);
            Console.WriteLine("Pass Result");
            Console.WriteLine(passResult);

            Console.ReadKey();
        }
    }
}
