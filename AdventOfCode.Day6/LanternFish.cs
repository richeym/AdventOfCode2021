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
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var lanternFish = data.First().Split(',').Select(int.Parse).ToList();

            for (int day = 0; day < 80; day++)
            {
                var totalFish = lanternFish.Count;
                for(int fish = 0; fish < totalFish; fish++)
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
    }
}