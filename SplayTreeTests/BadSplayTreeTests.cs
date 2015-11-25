using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplayTree;

namespace SplayTreeTests
{
    [TestClass]
    public class BadSplayTreeTests
    {
        private SplayTreeAbstract<ulong>.SplayNode<ulong> createNode(ulong key)
        {
            return new SplayTreeAbstract<ulong>.SplayNode<ulong> { Key = key };
        }

        [TestMethod]
        public void BadTestChainFind()
        {
            var t = new BadSplayTree<ulong>();
            t.Root = createNode(7);
            t.Root.Left = createNode(6);
            t.Root.Left.Left = createNode(5);
            t.Root.Left.Left.Left = createNode(4);
            t.Root.Left.Left.Left.Left = createNode(3);
            t.Root.Left.Left.Left.Left.Left = createNode(2);
            t.Root.Left.Left.Left.Left.Left.Left = createNode(1);

            t.Root.Left.Parent = t.Root;
            t.Root.Left.Left.Parent = t.Root.Left;
            t.Root.Left.Left.Left.Parent = t.Root.Left.Left;
            t.Root.Left.Left.Left.Left.Parent = t.Root.Left.Left.Left;
            t.Root.Left.Left.Left.Left.Left.Parent = t.Root.Left.Left.Left.Left;
            t.Root.Left.Left.Left.Left.Left.Left.Parent = t.Root.Left.Left.Left.Left.Left;

            Assert.AreEqual("[7] [6] [5] [4] [3] [2] [1] ", t.ToString());

            t.Find(1);

            Assert.AreEqual("[1] [7] [6] [5] [4] [3] [2] ", t.ToString());

            t.Find(2);

            Assert.AreEqual("[2] [1] [7] [6] [5] [4] [3] ", t.ToString());
        }

        [TestMethod]
        public void BadTestInsert()
        {
            var t = new BadSplayTree<ulong>();

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
