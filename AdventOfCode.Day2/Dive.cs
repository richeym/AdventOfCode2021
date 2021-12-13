using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day2
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            long distance = 0, depth = 0, aim = 0;

            foreach (var move in data)
            {
                var direction = move.Split(' ')[0];
                var units = int.Parse(move.Split(' ')[1]);
                
                switch (direction)
                {
                    case "forward":
                        distance += units;
                        depth += units * aim;
                        break;
                    case "up":
                        aim -= units;
                        break;
                    case "down":
                        aim += units;
                        break;
                }
            }

            _testOutputHelper.WriteLine((depth * distance).ToString());
        }
    }
}