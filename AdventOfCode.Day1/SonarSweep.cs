using System;
using System.IO;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day1
{
    public class SonarSweep
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SonarSweep(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<int>("input.txt");

            int previous = int.MaxValue;
            int increases = 0;

            foreach (var t in data)
            {
                if (t > previous) increases++;

                previous = t;
            }

            _testOutputHelper.WriteLine(increases.ToString());
        }

        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<int>("input.txt");

            int previous = int.MaxValue;
            int increases = 0;

            for (var i = 0; i < data.Count - 2; i++)
            {
                var current = data[i] + data[i + 1] + data[i + 2];
                if (current > previous)
                {
                    increases++;
                }

                previous = current;
            }

            _testOutputHelper.WriteLine(increases.ToString());
        }
    }
}