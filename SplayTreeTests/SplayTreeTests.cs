using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplayTree;

namespace SplayTreeTests
{
    [TestClass]
    public class SplayTreeTests
    {
        private SplayTreeAbstract<ulong>.SplayNode<ulong> createNode(ulong key)
        {
            return new SplayTreeAbstract<ulong>.SplayNode<ulong> { Key = key };
        }

        [TestMethod]
        public void TestChainFind()
        {
            var t = new SplayTree<ulong>();
            t.Root = createNode(7);
            t.Root.Left = createNode(6);
            t.Root.Left.Left = createNode(5);
            t.Root.Left.Left.Left = createNode(4);
            t.Root.Left.Left.Left.Left = createNode(3);
            t.Root.Left.Left.Left.Left.Left = createNode(2);
            t.Root.Left.Left.Left.Left.Left.Left = createNode(1);

            Assert.AreEqual("[7] [6] [5] [4] [3] [2] [1] ", t.ToString());

            t.Find(1);

            Assert.AreEqual("[1] [6] [4] [7] [2] [5] [3] ", t.ToString());
        }

        [TestMethod]
        public void TestInsert()
        {
            var t = new SplayTree<ulong>();

            t.Insert(1);
            t.Insert(2);
            t.Insert(3);
            t.Insert(4);
            t.Insert(5);
            t.Insert(6);
            t.Insert(7);

            Assert.AreEqual("[6] [5] [7] [4] [3] [2] [1] ", t.ToString());
        }
    }
}
