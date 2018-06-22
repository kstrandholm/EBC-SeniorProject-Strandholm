using DubuqueCodeCamp.Downloader;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DubuqueCodeCamp.Downloader.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod()]
        public void MainTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Test_FileParser_ParsesCorrectlyWrittenFile()
        {
            var stringReader =
                new StringReader(
                    "Kelly | Strandholm | 'kstrandholm@pltnm.com' | Programmer | 08/15/1995 | 8 | 9 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 18 | 10 | 11 | 12 | 13 | 14 | 15 | 16");

            FileParser.ParseFile();
        }
    }
}
