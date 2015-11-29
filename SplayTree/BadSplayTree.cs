using System;

namespace SplayTree
{
    public class BadSplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override void Splay(SplayNode<T> node)
        {
            while (node.Parent != null)
            {
                if (node.Parent.Left == node)
                    RotateRight(node.Parent);
                else
                    RotateLeft(node.Parent);
            }
        }
    }
}
