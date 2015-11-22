using System;

namespace SplayTree
{
    public class SplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override SplayNode<T> Splay(SplayNode<T> node, T key)
        {
            if (node == null)
            {
                return null;
            }

            if (!WasRead.ContainsKey(node)) { WasRead.Add(node, true); }

            var cmpGrandpa = key.CompareTo(node.Key);

            if (cmpGrandpa < 0)
            {
                if (node.Left == null) { return node; }

                var cmpFather = key.CompareTo(node.Left.Key);
                if (cmpFather < 0)
                {
                    node.Left.Left = Splay(node.Left.Left, key);
                    node = RotateRight(node);
                }
                else if (cmpFather > 0)
                {
                    node.Left.Right = Splay(node.Left.Right, key);
                    if (node.Left.Right != null)
                    {
                        node.Left = RotateLeft(node.Left);
                    }
                }

                if (node.Left == null)
                {
                    return node;
                }

                return RotateRight(node);
            }
            else if (cmpGrandpa > 0)
            {
                if (node.Right == null) { return node; }

                var cmpFather = key.CompareTo(node.Right.Key);
                if (cmpFather < 0)
                {
                    node.Right.Left = Splay(node.Right.Left, key);
                    if (node.Right.Left != null)
                    {
                        node.Right = RotateRight(node.Right);
                    }
                }
                else if (cmpFather > 0)
                {
                    node.Right.Right = Splay(node.Right.Right, key);
                    node = RotateLeft(node);
                }

                if (node.Right == null)
                {
                    return node;
                }

                return RotateLeft(node);
            }

            else return node;
        }
    }
}
