using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Sjerrul.NGrammer.Parser.Tests
{
    [TestClass]
    public class NgramParserTests
    {
        [TestMethod]
        public void NgramParser_Sentence1_ShouldCorrectlySplitStringInTrigramKeys()
        {
            NgramParser parser = new NgramParser(3);

            string s = "I wish I may I wish I might";
            parser.Read(s);

            Assert.AreEqual(4, parser.GetNumberOfNgrams());
        }

        [TestMethod]
        public void NgramParser_Sentence2_ShouldCorrectlySplitStringInTrigramKeys()
        {
            NgramParser parser = new NgramParser(3);

            string s = "I think I like what this project does";
            parser.Read(s);

            Assert.AreEqual(6, parser.GetNumberOfNgrams());
        }

        [TestMethod]
        public void NgramParser_Test()
        {
            NgramParser parser = new NgramParser(3);

            string s = File.ReadAllText("JipJanneke.txt");
            
            parser.Read(s);
            string t = parser.Write();

            File.WriteAllText("Result.txt", t);
        }


        
    }
}
