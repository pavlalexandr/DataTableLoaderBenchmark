using BenchmarkDotNet.Attributes;
using MinimalKDemo.Services;
using System.Data;

namespace MinimalKDemo
{
    [MemoryDiagnoser]
    public class DataLoaderBenchmark
    {
        private readonly DataTable _dataTable;

        public DataLoaderBenchmark()
        {
            _dataTable = CreateDataTable();
        }

        private static DataTable CreateDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("user_id", typeof(int));
            dataTable.Columns.Add("user_value", typeof(decimal));
            for (int i = 0; i < 1000000; i++)
            {
                dataTable.Rows.Add(i, null);
            }

            dataTable.Rows.Add(int.MaxValue, null);
            return dataTable;
        }

        [BenchmarkCategory("concurrent")]
        [Benchmark]
        public void Concurrent()
        {
            var loader = new DataTableLoader();

            loader.FillData(_dataTable, new SimpleUserDataGetter());
        }

        [BenchmarkCategory("singleThread")]
        [Benchmark]
        public void SingleThread()
        {
            var loader = new DataTableLoader();

            loader.FillTableWithoutConcurrency(_dataTable, new SimpleUserDataGetter());
        }

        [BenchmarkCategory("concurrent", "async")]
        [Benchmark]
        public async Task ConcurrentAsync()
        {
            var loader = new DataTableLoader();

            await loader.FillDataAsync(_dataTable, new SimpleUserDataGetter());
        }


        [BenchmarkCategory("concurrent", "slepp")]
        [Benchmark]
        public void ConcurrentSleep()
        {
            var loader = new DataTableLoader();

            loader.FillData(_dataTable, new ThreadSleepUserDataGetter());
        }

        [BenchmarkCategory("singleThread", "sleep")]
        [Benchmark]
        public void SingleThreadSleep()
        {
            var loader = new DataTableLoader();

            loader.FillTableWithoutConcurrency(_dataTable, new ThreadSleepUserDataGetter());
        }

        [BenchmarkCategory("concurrent", "async", "sleep")]
        [Benchmark]
        public async Task ConcurrentAsyncSleep()
        {
            var loader = new DataTableLoader();

            await loader.FillDataAsync(_dataTable, new ThreadSleepUserDataGetter());
        }

        [BenchmarkCategory("concurrent", "async", "sleep", "class")]
        [Benchmark]
        public async Task ConcurrentClassAsyncSleep()
        {
            var loader = new DataTableLoader();

            await loader.FillClassDataAsync(_dataTable, new ThreadSleepUserDataGetter());
        }

        [Benchmark]
        public void WithLockedDataTable()
        {
            var loader = new DataTableLoader();

            loader.FillDataTable(_dataTable, new ThreadSleepUserDataGetter());
        }
    }
}
