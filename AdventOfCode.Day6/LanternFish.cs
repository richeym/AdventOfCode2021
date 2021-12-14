using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day6
{
    public class LanternFish
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public LanternFish(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1_BruteForce()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var lanternFish = data.First().Split(',').Select(int.Parse).ToList();

            for (int day = 0; day < 80; day++)
            {
                var totalFish = lanternFish.Count;
                for (int fish = 0; fish < totalFish; fish++)
                {
                    if (lanternFish[fish] == 0)
                    {
                        lanternFish[fish] = 6;
                        lanternFish.Add(8);
                    }
                    else
                    {
                        lanternFish[fish]--;
                    }
                }
            }

            _testOutputHelper.WriteLine($"{lanternFish.Count}");
        }

        [Fact]
        public void Test2_Elegant()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var lanternFish = data.First().Split(',').Select(int.Parse).ToList();
            var fishPerDayCycle = new long[9];

            for (int i = 1; i <= 5; i++)
            {
                fishPerDayCycle[i] = lanternFish.Count(x => x == i);
            }
            
            for (var i = 0; i < 256; i++)
            {
                long day0 = fishPerDayCycle[0];
                Array.Copy(fishPerDayCycle, 1, fishPerDayCycle, 0, fishPerDayCycle.Length - 1);
                fishPerDayCycle[8] = day0;
                fishPerDayCycle[6] += day0;
            }
            
            _testOutputHelper.WriteLine($"{fishPerDayCycle.Sum()}");
        }
    }
}