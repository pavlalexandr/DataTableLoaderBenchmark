|                    Method |       Mean |    Error |   StdDev |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|-------------------------- |-----------:|---------:|---------:|-----------:|-----------:|----------:|----------:|
|                Concurrent |   501.1 ms |  5.93 ms |  5.55 ms | 25000.0000 |  9000.0000 | 1000.0000 |    249 MB |
|              SingleThread |   382.4 ms |  3.53 ms |  3.13 ms | 12000.0000 |          - |         - |    170 MB |
|           ConcurrentAsync |   502.4 ms |  8.89 ms |  8.32 ms | 25000.0000 |  9000.0000 | 1000.0000 |    249 MB |
|           ConcurrentSleep |   571.2 ms |  7.40 ms |  6.92 ms | 25000.0000 |  9000.0000 | 1000.0000 |    249 MB |
|         SingleThreadSleep | 1,379.8 ms |  6.45 ms |  6.03 ms | 12000.0000 |          - |         - |    170 MB |
|      ConcurrentAsyncSleep |   578.6 ms |  9.67 ms |  9.05 ms | 25000.0000 |  9000.0000 | 1000.0000 |    249 MB |
| ConcurrentClassAsyncSleep |   656.3 ms | 12.96 ms | 12.13 ms | 30000.0000 | 11000.0000 | 1000.0000 |    272 MB |
