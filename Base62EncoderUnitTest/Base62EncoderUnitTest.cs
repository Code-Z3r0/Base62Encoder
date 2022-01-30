using Base62;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Base62EncoderUnitTest
{
    [TestClass]
    public class Base62EncoderUnitTest
    {
        [TestMethod]
        public void TestEncode()
        {
            //assert word "Hello, World!" = "yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd"
            //yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd
            //yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd
            //48656C6C6F2C20576F726C6421
            var toEncode = "Hello, World!";
            var result = Base62Encoder.Encode(toEncode);
            Console.WriteLine(result);
            Assert.IsTrue(result.Equals("yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd"), "Encode correct");
            Assert.IsFalse(!result.Equals("yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd"), "Encode incorrect");
        }

        [TestMethod]
        public void TestDecode()
        {
            //assert word "Hello, World!" = "yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd"
            //yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd
            //yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd
            //48656C6C6F2C20576F726C6421
            var toDecode = "yBQxKgPtOePNYaHaffwHsQ2fjRgcmdmr8fd";
            var result = Base62Encoder.Decode(toDecode);
            Console.WriteLine(result);
            Assert.IsTrue(result.Equals("Hello, World!"), "Decode correct");
            Assert.IsFalse(!result.Equals("Hello, World!"), "Decode incorrect");
        }
    }
}