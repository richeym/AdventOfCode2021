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
                var output = entry.Split(" | ")[1];

                var outputValues = output.Split(' ').ToList();
                
                total += outputValues.Count(x => x.Length == 2);
                total +=  outputValues.Count(x => x.Length == 3);
                total +=  outputValues.Count(x => x.Length == 4);
                total +=  outputValues.Count(x => x.Length == 7);
            }
            
            _testOutputHelper.WriteLine(total.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            int total = 0;
            
            foreach (var entry in data)
            {
                var split = entry.Split(" | ");
                var inputs = split[0];
                var outputs = split[1];

                var signalPatterns = inputs.Split(' ').ToList();

                var digitPatterns = new string[10];

                // unique length
                digitPatterns[1] = signalPatterns.Single(x => x.Length == 2);
                signalPatterns.Remove(digitPatterns[1]);
                
                // unique length
                digitPatterns[4] = signalPatterns.Single(x => x.Length == 4);
                signalPatterns.Remove(digitPatterns[4]);
                
                // 6 chars sharing 1 char with digit 1
                digitPatterns[6] = signalPatterns.Single(x => x.Length == 6 && digitPatterns[1].Intersect(x).Count() == 1);
                signalPatterns.Remove(digitPatterns[6]);
                
                // 5 chars all in digit 6
                digitPatterns[5] = signalPatterns.Single(x => x.Length == 5 && x.All(digitPatterns[6].Contains));
                signalPatterns.Remove(digitPatterns[5]);
                
                // unique length
                digitPatterns[7] = signalPatterns.Single(x => x.Length == 3);
                signalPatterns.Remove(digitPatterns[7]);
                
                // unique length
                digitPatterns[8] = signalPatterns.Single(x => x.Length == 7);
                signalPatterns.Remove(digitPatterns[8]);
                
                // 5 chars sharing 3 with digit 5
                digitPatterns[2] = signalPatterns.Single(x => x.Length == 5 && digitPatterns[5].Intersect(x).Count() == 3);
                signalPatterns.Remove(digitPatterns[2]);
                
                // 5 chars sharing 4 with digit 5
                digitPatterns[3] = signalPatterns.Single(x => x.Length == 5 && digitPatterns[5].Intersect(x).Count() == 4);
                signalPatterns.Remove(digitPatterns[3]);
                
                // 6 chars sharing 5 with digit 5
                digitPatterns[9] = signalPatterns.Single(x => x.Length == 6 && digitPatterns[5].Intersect(x).Count() == 5);
                signalPatterns.Remove(digitPatterns[9]);

                // last man standing
                digitPatterns[0] = signalPatterns.Single();

                var outputPatterns = outputs.Split(' ');
                int outputValue = 0;
                
                for (var i = 0; i < outputPatterns.Length; i++)
                {
                    var digitPattern = digitPatterns.Single(x => x.Length == outputPatterns[i].Length && x.All(outputPatterns[i].Contains));
                    var digitValue = Array.IndexOf(digitPatterns, digitPattern);
                    outputValue += digitValue * (int)Math.Pow(10, outputPatterns.Length - i - 1);
                }

                total += outputValue;
            }
            
            _testOutputHelper.WriteLine(total.ToString());
        }
    }
}