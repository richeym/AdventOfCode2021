using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day8
{
    public class SevenSegmentSearch
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SevenSegmentSearch(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            var total = 0;
            
            foreach (var entry in data)
            {
                var split = entry.Split(" | ");
                var output = split[1];

                var outputValues = output.Split(' ').ToList();
                
                total += outputValues.Count(x => x.Length == 2);
                total +=  outputValues.Count(x => x.Length == 4);
                total +=  outputValues.Count(x => x.Length == 3);
                total +=  outputValues.Count(x => x.Length == 7);
            }
            
            _testOutputHelper.WriteLine(total.ToString());
        }
    }
}