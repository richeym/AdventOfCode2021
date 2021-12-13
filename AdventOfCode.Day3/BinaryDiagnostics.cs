using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day3
{
    public class BinaryDiagnostics
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BinaryDiagnostics(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            int sequences = data[0].Length;

            string gammaRate = string.Empty;
            string epsilonRate = string.Empty;

            for (int x = 0; x < sequences; x++)
            {
                int zeros = 0;
                int ones = 1;

                foreach (var t in data)
                {
                    if (t[x] == '0')
                    {
                        zeros++;
                    }
                    else
                    {
                        ones++;
                    }
                }

                gammaRate += zeros > ones ? '0' : '1';
                epsilonRate += zeros < ones ? '0' : '1';
            }

            _testOutputHelper.WriteLine($"Gamma rate: {gammaRate}");
            _testOutputHelper.WriteLine($"Epsilon rate: {epsilonRate}");
            _testOutputHelper.WriteLine(
                $"Power consumption: {Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)}");
        }

        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            int numberOfBits = data[0].Length;
            var remainingSequences = new List<string>(data);

            for (int x = 0; x < numberOfBits; x++)
            {
                var matchedChar = CountMostMatches(remainingSequences, x);
                remainingSequences.RemoveAll(item => item[x] != matchedChar);

                if (remainingSequences.Count == 1) break;
            }

            var sequence = remainingSequences.First();
            
            var oxygenGeneratorRating = Convert.ToInt32(sequence, 2);
            _testOutputHelper.WriteLine($"Oxygen generator rating: {oxygenGeneratorRating}");
            
            remainingSequences = new List<string>(data);
            
            for (int x = 0; x < numberOfBits; x++)
            {
                var matchedChar = CountLeastMatches(remainingSequences, x);
                remainingSequences.RemoveAll(item => item[x] != matchedChar);

                if (remainingSequences.Count == 1) break;
            }
            
            var co2ScrubberRating = Convert.ToInt32(remainingSequences.First(), 2);
            _testOutputHelper.WriteLine($"CO2 scrubber rating: {co2ScrubberRating}");
            
            _testOutputHelper.WriteLine($"Life support rating: {oxygenGeneratorRating * co2ScrubberRating}");
        }

        private char CountMostMatches(List<string> data, int x)
        {
            int zeros = 0;
            int ones = 0;

            foreach (var t in data)
            {
                if (t[x] == '0')
                {
                    zeros++;
                }
                else
                {
                    ones++;
                }
            }

            return zeros > ones ? '0' : '1';
        }
        
        private static char CountLeastMatches(List<string> data, int x)
        {
            int zeros = 0;
            int ones = 0;

            foreach (var t in data)
            {
                if (t[x] == '0')
                {
                    zeros++;
                }
                else
                {
                    ones++;
                }
            }

            return ones < zeros ? '1' : '0';
        }
    }
}