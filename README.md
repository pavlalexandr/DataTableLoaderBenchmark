``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1586 (21H1/May2021Update)
Intel Core i9-10980HK CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.201
  [Host]     : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT
  DefaultJob : .NET 6.0.3 (6.0.322.12309), X64 RyuJIT


```
|                    Method |       Mean |    Error |   StdDev |      Gen 0 |      Gen 1 |     Gen 2 | Allocated |
|-------------------------- |-----------:|---------:|---------:|-----------:|-----------:|----------:|----------:|
|                Concurrent |   542.3 ms |  7.60 ms |  7.11 ms | 24000.0000 |  8000.0000 | 1000.0000 |    248 MB |
|              SingleThread |   432.3 ms |  6.81 ms |  5.68 ms | 12000.0000 |          - |         - |    170 MB |
|           ConcurrentAsync |   541.6 ms |  5.66 ms |  5.29 ms | 24000.0000 |  8000.0000 | 1000.0000 |    248 MB |
|           ConcurrentSleep |   664.9 ms |  6.90 ms |  6.45 ms | 24000.0000 |  8000.0000 | 1000.0000 |    248 MB |
|         SingleThreadSleep | 1,442.7 ms |  5.16 ms |  4.83 ms | 12000.0000 |          - |         - |    170 MB |
|      ConcurrentAsyncSleep |   604.9 ms | 11.42 ms | 11.22 ms | 24000.0000 |  8000.0000 | 1000.0000 |    248 MB |
| ConcurrentClassAsyncSleep |   705.3 ms | 13.80 ms | 16.94 ms | 29000.0000 | 11000.0000 | 1000.0000 |    271 MB |
|       WithLockedDataTable |   890.1 ms | 16.66 ms | 26.42 ms | 58000.0000 |          - |         - |    535 MB |
