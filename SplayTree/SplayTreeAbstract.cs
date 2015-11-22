using System;
using System.Collections.Generic;
using System.Text;

namespace SplayTree
{
    public abstract class SplayTreeAbstract<T> where T : IComparable<T>
    {
        public SplayNode<T> Root;

        public Dictionary<SplayNode<T>, bool> WasRead = new Dictionary<SplayNode<T>, bool>();

        public class SplayNode<TU> where TU : IComparable<TU>
        {
            public TU Key;
            public SplayNode<TU> Left;
            public SplayNode<TU> Right;

            public override string ToString()
            {
                return string.Join(", ", Key.ToString());
            }
        }

        protected SplayNode<T> CreateNode()
        {
            return new SplayNode<T>();
        }

        protected SplayNode<T> CreateNode(T key)
        {
            var n = CreateNode();
            n.Key = key;
            return n;
        }

        protected SplayNode<T> RotateRight(SplayNode<T> u)
        {
            var x = u.Left;
            u.Left = x.Right;
            x.Right = u;
            return x;
        }

        protected SplayNode<T> RotateLeft(SplayNode<T> x)
        {
            var u = x.Right;
            x.Right = u.Left;
            u.Left = x;
            return u;
        }

        protected abstract SplayNode<T> Splay(SplayNode<T> node, T key);

        public T Find(T key)
        {
            Root = Splay(Root, key);
            var cmp = key.CompareTo(Root.Key);

            if (cmp == 0)
                return Root.Key;

            return default(T);
        }

        public void Insert(T key)
        {
            if (Root == null)
            {
                Root = CreateNode(key);
                return;
            }

            Root = Splay(Root, key);

            var cmp = key.CompareTo(Root.Key);
            if (cmp < 0)
            {
                var newNode = CreateNode(key);
                newNode.Left = Root.Left;
                Root.Left = newNode;
            }
            else if (cmp > 0)
            {
                var newNode = CreateNode(key);
                newNode.Right = Root.Right;
                Root.Right = newNode;
            }
            else
            {
                // key exists, do nothing
            }

        }

        public void Clear()
        {
            Root = null;
            WasRead.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var queue = new Queue<SplayNode<T>>();

            queue.Enqueue(Root);
            if (Root == null) { return "Empty tree"; }

            while (queue.Count > 0)
            {
                var n = queue.Dequeue();
                sb.Append("[").Append(n.Key).Append("] ");

                if (n.Left != null) { queue.Enqueue(n.Left); }
                if (n.Right != null) { queue.Enqueue(n.Right); }
            }

            return sb.ToString();
        }


    }
}
