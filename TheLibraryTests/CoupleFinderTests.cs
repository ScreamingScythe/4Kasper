using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Couples.Tests
{
    [TestClass]
    public class CoupleFinderTests
    {
        public static void GetCouplesTest(int[] input, int sum, string dataName)
        {
            List<int[]> couples = CoupleFinder.GetCouples(input, sum);
            List<int> rest = input.ToList();

            Assert.IsNotNull(couples, dataName + " - couples null");

            foreach (int[] c in couples)
            {
                Assert.IsNotNull(couples, dataName + " - null couple");

                Assert.IsTrue(c.Length == 2, dataName + " - len 2");

                Assert.IsTrue(rest.Contains(c[0]), dataName + " - cont 0");
                rest.Remove(c[0]);

                Assert.IsTrue(rest.Contains(c[1]), dataName + " - cont 1");
                rest.Remove(c[1]);
            }

            for (int i = 0; i < rest.Count; ++i)
            {
                for (int j = 0; j < rest.Count; ++j)
                {
                    if (i == j) continue;
                    Assert.IsFalse(rest[i] + rest[j] == sum, dataName + " - missing sum");
                }
            }
        }

        [TestMethod]
        public void GetCouplesTest()
        {
            GetCouplesTest(new [] {1, 1, 2, 1, 1, 0, 1}, 2, "basic data");
            GetCouplesTest(new [] { -3, -3, -2, -2, -1, 0, 0, 1, 2, 3, 4, 5, 6, 7, 7, 8, 9, 10 }, 7, "some other data");
        }
    }
}