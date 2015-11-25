using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplayTree
{
    class Program
    {
        enum OpType { NewTree, Insert, Find }

        struct Op
        {
            public OpType Type;
            public ulong Key;

            public Op(OpType type, ulong key)
            {
                Key = key;
                Type = type;
            }
        }

        static IEnumerable<Op> ReadFile(string filename)
        {
            string line;
            StreamReader file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {

                OpType type = OpType.NewTree;
                switch (line[0])
                {
                    case '#':
                        type = OpType.NewTree;
                        break;
                    case 'I':
                        type = OpType.Insert;
                        break;
                    case 'F':
                        type = OpType.Find;
                        break;
                }

                ulong key = 0;
                for (int i = 2; i < line.Length; i++)
                {
                    if (line[i] < '0' || line[i] > '9') { break; }

                    key *= 10;
                    key += (ulong)(line[i] - '0');
                }

                yield return new Op(type, key);
            }

            file.Close();
        }

        static void TestTree<T>(SplayTreeAbstract<T> t) where T : IComparable<T>
        {
            if (t.Root == null) return;

            var queue = new Queue<SplayTreeAbstract<T>.SplayNode<T>>();

            queue.Enqueue(t.Root);

            while (queue.Count > 0)
            {
                var n = queue.Dequeue();

                if (n.Left != null)
                {
                    queue.Enqueue(n.Left);

                    if (n.Left.Key.CompareTo(n.Key) > 0)
                    {
                        throw new Exception("Children on the left are higher");
                    }
                }

                if (n.Right != null)
                {
                    queue.Enqueue(n.Right);

                    if (n.Right.Key.CompareTo(n.Key) < 0)
                    {
                        throw new Exception("Children on the right are higher");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            SplayTreeAbstract<ulong> t;
            if (args.Length >= 2 && args[1] == "-b")
            {
                t = new BadSplayTree<ulong>();
                Console.WriteLine("{0}: {1}", "Bad splay tree", args[0]);
            }
            else
            {
                t = new SplayTree<ulong>();
                Console.WriteLine("{0}: {1}", "Good splay tree", args[0]);
            }

            var statsTouched = 0;
            var nOps = 0;
            var n = 0UL;

            foreach (var op in ReadFile(args[0]))
            {
                switch (op.Type)
                {
                    case OpType.NewTree:
                        if (nOps > 0)
                        {
                            Console.WriteLine("{0};{1}", n, 1.0 * statsTouched / nOps);
                        }

                        statsTouched = 0;
                        nOps = 0;
                        n = op.Key;
                        t.Clear();
                        continue;
                    case OpType.Insert:
                        t.IgnoreInsert = true;
                        t.Insert(op.Key);
                        t.IgnoreInsert = false;
                        //TestTree(t);
                        break;
                    case OpType.Find:
                        t.Find(op.Key);
                        ++nOps;
                        statsTouched += t.WasRead.Count;
                        t.WasRead.Clear();
                        //TestTree(t);
                        break;
                }

            }

            if (nOps > 0)
            {
                Console.WriteLine("{0};{1}", n, 1.0 * statsTouched / nOps);
            }

            if (args.Length >= 3 && args[2] == "-print")
            {
                TestTree(t);
                Console.WriteLine(t);
            }
        }
    }
}
