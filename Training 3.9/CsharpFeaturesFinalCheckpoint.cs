using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsharpFeaturesFinalCheckpoint
{
    [TestClass]
    public class TestsBeforeCsharpFeaturesFinalCheckpoint
    {
        readonly string[] GRADES = new string[]
        {
            "A", "B", "C", "D", "F"
        };

        [TestMethod]
        public void GetFirstName_NullableReference()
        {
            var student = new Student()
            {
                FirstName = "John"
            };

            string result = ($"The student is called {student.FirstName!}.");

            Assert.AreEqual("The student is called John.", result);
        }

        [TestMethod]
        public async Task GetStudentsAsync_WithFullName()
        {
            string result = "";
            foreach (var student in await GetStudentsAsync())
            {
                result = result + ($"{student.FirstName} {student.LastName} - ");
            }

            Assert.AreEqual("John Doe - Jane Doe - John Smith - ", result);
        }

        static async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            await Task.Delay(2000);
            return new List<Student>()
            {
                new Student() {FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com",Grade = 'A'},
                new Student() {FirstName = "Jane", LastName = "Doe", Email="jane.doe@galvanize.com", Grade = 'B'},
                new Student() {FirstName = "John", LastName = "Smith", Email = "john.smith@galvanize.com", Grade = 'C'}
            };
        }

        [TestMethod]
        public void GetGradeFromEnd_WhenCalledWithValidNumber_ShouldReturnGradeFromEnd()
        {
            string result = getGradeFromEnd(4);

            Assert.AreEqual("B", result);
        }

        private string getGradeFromEnd(int indexFromEnd)
        {
            string item = GRADES[GRADES.Length - indexFromEnd];
            return item;
        }

        [TestMethod]
        public void GetGradeRange_WhenCalledWithStartAndEndIndex_ShouldReturnGradeRange()
        {
            string[] result = getGradeRange(1, 3);

            CollectionAssert.AreEqual(new string[] { "B", "C", "D" }, result);
        }

        private string[] getGradeRange(int startIndex, int endIndex)
        {
            var length = endIndex - startIndex + 1;
            var result = GRADES.Skip(startIndex).Take(length).ToArray();
            return result;
        }

        [TestMethod]
        public void getPassOrFail_WithPropertyPatternMatching()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'B');
            var failingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByPropertyPatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByPropertyPatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        [TestMethod]
        public void getPassOrFail_WithTupleMatching()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'B');
            var failingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByTuplePatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByTuplePatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        [TestMethod]
        public void getPassOrFail_WithPositionalMatching()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'B');
            var failingStudent = new Student("Jane", "Doe", "Jane.Doe@gmail.com", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByPositionalPatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByPositionalPatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        public static bool getPassOrFail_ByPropertyPatternMatching(Student student)
        {
            // Use property pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
            return student switch
            {
                { Grade: 'A', TuitionPaid: true } => true,
                { Grade: 'B', TuitionPaid: true } => true,
                { Grade: 'C', TuitionPaid: true } => true,
                { Grade: 'D', TuitionPaid: true } => false,
                { Grade: 'F', TuitionPaid: true } => false,
                _ => false
            };
        }

        public static bool getPassOrFail_ByTuplePatternMatching(Student student)
        {
            // Use tuple pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
             return (student.TuitionPaid, student.Grade) switch
                {
                    (true, 'A') => true,
                    (true, 'B') => true,
                    (true, 'C') => true,
                    (true, 'D') => false,
                    (true, 'E') => false,
                    (_, _) => false
                };
        }

        public static bool getPassOrFail_ByPositionalPatternMatching(Student student)
        {
            // Use positional pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
            // Note: You will to define the Deconstruct method for Student
            return student switch
            {
                var (firstName, lastName, email, tuitionPaid, grade) when grade == 'A' && tuitionPaid == true => true,
                var (firstName, lastName, email, tuitionPaid, grade) when grade == 'B' && tuitionPaid == true => true,
                var (firstName, lastName, email, tuitionPaid, grade) when grade == 'C' && tuitionPaid == true => true,
                var (firstName, lastName, email, tuitionPaid, grade) when grade == 'D' && tuitionPaid == true => false,
                var (firstName, lastName, email, tuitionPaid, grade) when grade == 'F' && tuitionPaid == true => false,
                _ => false
            };
        }
    }

    public class Student
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool TuitionPaid { get; set; }
        public char Grade { get; set; }

        public Student(string firstName, string lastName, string email, bool tuitionPaid, char grade) =>
            (FirstName, LastName, Email, TuitionPaid, Grade) = (firstName, lastName, email, tuitionPaid, grade);

        public void Deconstruct(out string firstName, out string lastName, out string email, out bool tuitionPaid, out char grade) =>
           (firstName, lastName, email, tuitionPaid, grade) = (FirstName, LastName, Email, TuitionPaid, Grade);

        public Student()
        {
        }
    }
}