using System;
using System.Collections.Generic;

namespace SplayTree
{
    public class BadSplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override void Splay(SplayNode<T> node)
        {
            if (node == null) throw new Exception("null to splay");

            var x = node;
            while (x.Parent != null)
            {
                if (x.Parent.Left == x)
                    RotateRight(x.Parent);
                else
                    RotateLeft(x.Parent);
            }
        }
    }
}
