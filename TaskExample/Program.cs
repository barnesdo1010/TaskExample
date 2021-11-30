using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample
{
    class Program
    {
        static CancellationTokenSource _CancelToken = new CancellationTokenSource();
        static Task _ProcessTask1;

        static void Main(string[] args)
        {
            _ = Start();
            Console.ReadLine();
        }

        public static async Task Start()
        {
            _ProcessTask1 = Task.Factory.StartNew
                (
                    Clock,
                    _CancelToken.Token,
                    TaskCreationOptions.LongRunning,
                    TaskScheduler.Default
                );


            var guid = await Task.Factory.StartNew(GetApplicationGuid);

            Console.WriteLine(guid);
            
            Task.WaitAll(_ProcessTask1);
        }

        private static void Clock()
        {
            while(true)
            {
                if (DateTime.Now.Second % 1 == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"Time  {DateTime.Now.Hour}:{DateTime.Now.Minute}.{DateTime.Now.Second}");
                    Thread.Sleep(1000);
                }
                if(DateTime.Now.Second >= 50)
                {
                    break;
                }
            }
        }

        private static Guid GetApplicationGuid()
        {
            Thread.Sleep(10000);
            return Guid.NewGuid();
        }
    }
}
