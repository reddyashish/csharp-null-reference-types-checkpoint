using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsharpIndicesRangesCheckpoint
{
    [TestClass]
    public class TestsBefore
    {
        readonly string[] GRADES = new string[]
            {
                "A", "B", "C", "D", "F"
            };

        [TestMethod]
        public void GetGradeFromEnd_WhenCalledWithValidNumber_ShouldReturnGradeFromEnd()
        {
            string result = getGradeFromEnd(4);

            Assert.AreEqual("B", result);
        }

        public string getGradeFromEnd(int indexFromEnd)
        {
            string item = GRADES[^4];
            return item;
        }

        [TestMethod]
        public void GetGradeRange_WhenCalledWithStartAndEndIndex_ShouldReturnGradeRange()
        {
            string[] result = getGradeRange(1, 3);

            CollectionAssert.AreEqual(new string[] { "B", "C", "D" }, result);
        }

        public string[] getGradeRange(int startIndex, int endIndex)
        {
            var lastItem = endIndex + 1;
            var result = GRADES[startIndex..lastItem];
            return result;
        }
    }
}