using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Memo;

namespace Memo.Test
{
    [TestClass]
    public class ExtendedPath_Test
    {
        [TestMethod]
        public void Extension_ShouldWorkOnProperValue()
        {
            // arrange
            string path = @"D:/folder/folder/name.jpg";
            string expected = @"D:/folder/folder/name_add.jpg";

            // act

            string actual = path.ExtendPath("_add");

            // assert 

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Extension_ShouldWorkOnAddingADot()
        {
            // arrange
            string path = @"D:/folder/folder/name.jpg";
            string expected = @"D:/folder/folder/name_a.dd.jpg";

            // act

            string actual = path.ExtendPath("_a.dd");

            // assert 

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Extension_ShouldWorkAddingThreeDots()
        {
            // arrange
            string path = @"D:/folder/folder/name.jpg";
            string expected = @"D:/folder/folder/name....jpg";

            // act

            string actual = path.ExtendPath("...");

            // assert 

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Extension_ShouldFailOnEmptyString()
        {
            // arrange

            // act

            // assert 

            Assert.ThrowsException<System.ArgumentOutOfRangeException>( () => string.Empty.ExtendPath("add") );
        }
    }
}
