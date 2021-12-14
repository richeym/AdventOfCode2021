using System;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day7
{
    public class TreacheryOfWhales
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TreacheryOfWhales(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var crabs = data.First().Split(',').Select(int.Parse).ToList();

            var bestTotalMoves = int.MaxValue;

            for (var desiredPosition = crabs.Min(); desiredPosition <= crabs.Max(); desiredPosition++)
            {
                var totalMoves = crabs.Sum(crab => Math.Max(desiredPosition, crab) - Math.Min(desiredPosition, crab));

                if (totalMoves < bestTotalMoves)
                {
                    bestTotalMoves = totalMoves;
                }
            }

            _testOutputHelper.WriteLine($"{bestTotalMoves}");
        }
        
        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var crabs = data.First().Split(',').Select(int.Parse).ToList();

            var bestTotalMoves = int.MaxValue;

            for (var desiredPosition = crabs.Min(); desiredPosition <= crabs.Max(); desiredPosition++)
            {
                var totalMoves = 0;

                foreach (var spacesMoved in crabs.Select(crab => Math.Max(desiredPosition, crab) - Math.Min(desiredPosition, crab)))
                {
                    for (var i = 1; i <= spacesMoved; i++)
                    {
                        totalMoves += i;
                    }
                }

                if (totalMoves < bestTotalMoves)
                {
                    bestTotalMoves = totalMoves;
                }
            }

            _testOutputHelper.WriteLine($"{bestTotalMoves}");
        }
    }
}