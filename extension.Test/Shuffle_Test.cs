using System;
using System.Collections.Generic;
using System.Diagnostics;
using Memo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memo.Test
{
    [TestClass]
    public class Shuffle_Test
    {
        [TestMethod]
        public void Shuffle_DifferentOrderInt()
        {
            // arrange
            List<int> notExpected = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
            };
            List<int> expected = new List<int>()
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
            };

            // act

            expected.Shuffle<int>();

            // assert

            CollectionAssert.AreNotEqual(notExpected, expected);
        }
        [TestMethod]
        public void Shuffle_DifferentOrderIntWithDuplicates()
        {
            // arrange
            List<int> notExpected = new List<int>()
            {
                0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7
            };
            List<int> expected = new List<int>()
            {
                0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7
            };

            // act
            
            expected.Shuffle<int>();


            // assert

            CollectionAssert.AreNotEqual(notExpected, expected);
        }

        
        [TestMethod]
        public void Shuffle_Len1_ShouldBeEqual()
        {
            // arrange
            List<int> notExpected = new List<int>()
            {
                0
            };
            List<int> expected = new List<int>()
            {
                0
            };

            // act

            expected.Shuffle<int>();

            // assert

            CollectionAssert.AreEqual(notExpected, expected);
        }
     
    }
}

