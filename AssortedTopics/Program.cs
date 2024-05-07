using System.Diagnostics;

namespace AssortedTopics
{
    public class MyClass : IDisposable
    {
        public MyClass()
        {
            Console.WriteLine("hello");
        }

        public void Dispose()
        {
            Console.WriteLine("goodbye");
        }
    }

    public class Disposable : IDisposable
    {
        private readonly Action end;

        private Disposable(Action start, Action end)
        {
            this.end = end;
            start();
        }

        public void Dispose()
        {
            end();
        }
    }

    public class SimpleTimer : IDisposable
    {
        private readonly Stopwatch str;

        public SimpleTimer()
        {
            str = new Stopwatch();
            str.Start();
        }

        public void Dispose()
        {
            str.Stop();
            Console.WriteLine($"{str.ElapsedMilliseconds} msec elapsed");
        }
    }


    class AssortedTopicsDemo
    {
        public static void Main(string[] args)
        {
            using (var mc = new MyClass())
            {

            }
            using (new MyClass())
            {

            }

            using (new SimpleTimer())
            {
                Thread.Sleep(1000);
            }

        }
    }
}

