using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Core
{
    public class Benchmark
    {

        private const int ITER_COUNT = 100000000;
        private const int ROUNDS = 10;

        private int avgTime = 0;
        private int roundsDone = 0;

        public void RunBenchmark(int threads, Action<int> onUpdate, Action<int> onComplete)
        {
            avgTime = 0;
            ThreadPool.SetMaxThreads(threads, threads);
            for (int i = 0; i < ROUNDS; i++)
            {
                ThreadPool.QueueUserWorkItem((ob) =>
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    BenchWork();
                    stopWatch.Stop();
                    avgTime += (int)stopWatch.ElapsedMilliseconds;
                    onUpdate((int)stopWatch.ElapsedMilliseconds);
                    if (++roundsDone >= ROUNDS)
                    {
                        avgTime /= ROUNDS;
                        onComplete(avgTime);
                    }
                });
            }
        }


        private void BenchWork()
        {
            int t = 0;
            for (int i = 0; i < ITER_COUNT; i++)
            {
                t++;
            }
        }

    }
}
