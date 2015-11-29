using System;

namespace SplayTree
{
    public class SplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override void Splay(SplayNode<T> node)
        {
            while (node.Parent != null)
            {
                if (node.Parent.Parent == null)
                {
                    if (node.Parent.Left == node) RotateRight(node.Parent);
                    else RotateLeft(node.Parent);
                }
                else if (node.Parent.Left == node && node.Parent.Parent.Left == node.Parent)
                {
                    RotateRight(node.Parent.Parent);
                    RotateRight(node.Parent);
                }
                else if (node.Parent.Right == node && node.Parent.Parent.Right == node.Parent)
                {
                    RotateLeft(node.Parent.Parent);
                    RotateLeft(node.Parent);
                }
                else if (node.Parent.Left == node && node.Parent.Parent.Right == node.Parent)
                {
                    RotateRight(node.Parent);
                    RotateLeft(node.Parent);
                }
                else
                {
                    RotateLeft(node.Parent);
                    RotateRight(node.Parent);
                }
            }
        }
    }
}
