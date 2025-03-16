# NestedDIContainer.Benchmark

BenchmarkCode => https://github.com/tanitaka-tech/NestedDIContainer.Benchmark/blob/develop/Assets/Tests/Benchmark.cs

## GC-Related Metrics Comparison

![Screenshot 2025-03-16 at 14 05 20](https://github.com/user-attachments/assets/15bae4cf-9d1c-4be5-89aa-c695a0cb8a62)

| LibraryName            | Min | Median | Max | Avg | StdDev | SampleCount | Sum |
|------------------------|-----|--------|-----|-----|--------|-------------|------|
| ManualDi.GC()          | 42 | 42 | 143 | 42.59 | 7.15 | 200 | 8,517 |
| Reflex.GC()            | 118 | 118 | 1,464 | 124.73 | 94.94 | 200 | 24,946 |
| NestedDIContainer.GC() | 16 | 16 | 97 | 16.51 | 5.73 | 200 | 3,302 |
| PinInject.GC()         | 25 | 25 | 43 | 25.09 | 1.27 | 200 | 5,018 |
| VContainer.GC()        | 198 | 210.5 | 598 | 212.38 | 30.05 | 200 | 42,475 |

## Execution Time Comparison (Nanoseconds)

![Screenshot 2025-03-16 at 14 06 08](https://github.com/user-attachments/assets/843affef-c0eb-4737-a921-10980ccc7dfd)

| LibraryName | Min | Median | Max | Avg | StdDev | SampleCount | Sum |
|-------------|-----|--------|-----|-----|--------|-------------|------|
| ManualDi | 29,200 | 42,300 | 61,700 | 41,025 | 6,703.33 | 100 | 4,102,500 |
| Reflex | 55,000 | 76,050 | 100,500 | 75,063 | 9,240.06 | 100 | 7,506,300 |
| NestedDIContainer | 15,700 | 22,200 | 81,400 | 23,367 | 8,230.10 | 100 | 2,336,700 |
| PinInject | 19,500 | 23,600 | 39,100 | 24,368 | 3,448.07 | 100 | 2,436,800 |
| VContainer | 108,000 | 122,500 | 158,700 | 123,959 | 8,496.22 | 100 | 12,395,900 |
