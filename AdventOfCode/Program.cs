using BenchmarkDotNet.Running;

Console.WriteLine(DayTwo.ProcessMatchTwo());

Console.ReadKey();

var summary = BenchmarkRunner.Run<DayTwo>();