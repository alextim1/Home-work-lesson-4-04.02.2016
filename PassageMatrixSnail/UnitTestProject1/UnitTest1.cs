using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassageMatrixSnail;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTrue()
        {
            // arrange
            string path1 = @"D:\matrix3.txt"; string path2 = @"D:\matrix2.txt";
            Matrix matrTest = new Matrix(path1);
            Matrix matrCheck = new Matrix(path2);
            int[] arr = new int[matrTest.Columns * matrTest.Rows];
            Direction direct = new Direction();

            //act
            matrTest.PassageMatrix(ref arr, 0, 0, 0, matrTest.Rows, matrTest.Columns, direct);

            // Assert
            CollectionAssert.AreEqual(matrCheck.GetMatrix, arr);
        }
    }
}
