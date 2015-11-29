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

        protected void RotateRight(SplayNode<T> father)
        {
            var son = father.Left;
            if (son != null)
            {
                if (son.Right != null) son.Right.Parent = father;
                father.Left = son.Right;
                son.Parent = father.Parent;
                son.Right = father;
            }

            if (father.Parent == null) Root = son;
            else if (father == father.Parent.Left) father.Parent.Left = son;
            else father.Parent.Right = son;

            father.Parent = son;
        }

        protected void RotateLeft(SplayNode<T> father)
        {
            var son = father.Right;
            if (son != null)
            {
                if (son.Left != null) son.Left.Parent = father;
                father.Right = son.Left;
                son.Parent = father.Parent;
                son.Left = father;
            }

            if (father.Parent == null) Root = son;
            else if (father == father.Parent.Left) father.Parent.Left = son;
            else father.Parent.Right = son;

            father.Parent = son;
        }

        protected abstract void Splay(SplayNode<T> node2);

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
