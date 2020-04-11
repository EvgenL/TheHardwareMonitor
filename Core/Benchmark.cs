using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Benchmark
    {

        private const int ITER_COUNT = 100000000;

        public async void RunBenchmark(int threads, Action<int> onComplete)
        {
            int start = DateTime.Now.Millisecond;
            BenchWork();
            int end = DateTime.Now.Millisecond - start;
            onComplete(end);
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
