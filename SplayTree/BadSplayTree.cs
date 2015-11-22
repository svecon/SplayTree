using System;

namespace SplayTree
{
    public class BadSplayTree<T> : SplayTreeAbstract<T> where T : IComparable<T>
    {
        protected override SplayNode<T> Splay(SplayNode<T> node, T key)
        {
            if (node == null) return null;

            if (!WasRead.ContainsKey(node)) { WasRead.Add(node, true); }

            var cmp = key.CompareTo(node.Key);

            if (cmp < 0)
            {
                if (node.Left == null) { return node; }

                node.Left = Splay(node.Left, key);
                node = RotateRight(node);

                return node;
            }
            else if (cmp > 0)
            {
                if (node.Right == null)
                {
                    return node;
                }

                node.Right = Splay(node.Right, key);
                node = RotateLeft(node);
                return node;
            }
            else
            {
                return node;
            }
        }
    }
}
