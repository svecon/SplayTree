using System;
using System.Collections.Generic;
using System.Text;

namespace SplayTree
{
    public abstract class SplayTreeAbstract<T> where T : IComparable<T>
    {
        public SplayNode<T> Root;

        public HashSet<SplayNode<T>> WasRead = new HashSet<SplayNode<T>>();

        public bool IgnoreInsert = false;

        public class SplayNode<TU> where TU : IComparable<TU>
        {
            public TU Key;
            public SplayNode<TU> Parent;
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

        protected void RotateRight(SplayNode<T> x)
        {
            var y = x.Left;
            if (y != null)
            {
                x.Left = y.Right;
                if (y.Right != null) y.Right.Parent = x;
                y.Parent = x.Parent;
            }

            if (x.Parent == null) Root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;

            if (y != null) y.Right = x;

            x.Parent = y;
        }

        protected void RotateLeft(SplayNode<T> x)
        {
            var y = x.Right;
            if (y != null)
            {
                x.Right = y.Left;
                if (y.Left != null) y.Left.Parent = x;
                y.Parent = x.Parent;
            }

            if (x.Parent == null) Root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;

            if (y != null) y.Left = x;

            x.Parent = y;
        }

        protected abstract void Splay(SplayNode<T> node);

        protected SplayNode<T> FindNode(T key)
        {
            var current = Root;
            var previous = current;
            while (current != null)
            {
                previous = current;
                if (!IgnoreInsert && !WasRead.Contains(current)) { WasRead.Add(current); }

                var cmp = key.CompareTo(current.Key);

                if (cmp < 0) current = current.Left;
                else if (cmp > 0) current = current.Right;
                else return current;
            }
            return previous;
        }

        public T Find(T key)
        {
            var node = FindNode(key);
            Splay(node);

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

            var node = FindNode(key);
            Splay(node);

            var cmp = key.CompareTo(Root.Key);
            if (cmp < 0)
            {
                var newNode = CreateNode(key);
                newNode.Left = Root.Left;
                Root.Left = newNode;

                newNode.Parent = Root;
                if (newNode.Left != null)
                {
                    newNode.Left.Parent = newNode;
                }
            }
            else if (cmp > 0)
            {
                var newNode = CreateNode(key);
                newNode.Right = Root.Right;
                Root.Right = newNode;

                newNode.Parent = Root;
                if (newNode.Right != null)
                {
                    newNode.Right.Parent = newNode;
                }
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
