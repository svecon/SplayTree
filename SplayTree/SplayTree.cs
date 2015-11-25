using System;

namespace SplayTree
{
    public class SplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override void Splay(SplayNode<T> node)
        {
            if (node == null) throw new Exception("null to splay");

            var x = node;
            while (x.Parent != null)
            {
                if (x.Parent.Parent == null)
                {
                    if (x.Parent.Left == x) RotateRight(x.Parent);
                    else RotateLeft(x.Parent);
                }
                else if (x.Parent.Left == x && x.Parent.Parent.Left == x.Parent)
                {
                    RotateRight(x.Parent.Parent);
                    RotateRight(x.Parent);
                }
                else if (x.Parent.Right == x && x.Parent.Parent.Right == x.Parent)
                {
                    RotateLeft(x.Parent.Parent);
                    RotateLeft(x.Parent);
                }
                else if (x.Parent.Left == x && x.Parent.Parent.Right == x.Parent)
                {
                    RotateRight(x.Parent);
                    RotateLeft(x.Parent);
                }
                else
                {
                    RotateLeft(x.Parent);
                    RotateRight(x.Parent);
                }
            }
        }
    }
}
