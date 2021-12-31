using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day13
{
    public class TransparentOrigami
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TransparentOrigami(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var coords = data.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var rowLength = coords.Max(x => int.Parse(x.Split(',')[1]));
            var colLength = coords.Max(x => int.Parse(x.Split(',')[0]));

            var foldingInstructions = data
                .SkipWhile(x => !string.IsNullOrWhiteSpace(x))
                .Skip(1)
                .ToList();
            
            _testOutputHelper.WriteLine($"Row Length: {rowLength}");
            _testOutputHelper.WriteLine($"Col  Length: {colLength}");

            var paper = new char[rowLength + 1, colLength + 1];

            foreach (var coord in coords)
            {
                var colRow = coord.Split(',');
                paper[int.Parse(colRow[1]), int.Parse(colRow[0])] = '#';
            }

            bool isFirstFold = true;
                
            foreach (var instruction in foldingInstructions)
            {
                var match = Regex.Match(instruction, @"^.*([xy])=(\d*)$");
                var axis = match.Groups[1].Value;
                var length = int.Parse(match.Groups[2].Value);

                if (axis == "x")
                {
                    var offset = 0;

                    for (var col = colLength; col > length; col--)
                    {
                        for (var row = 0; row <= rowLength; row++)
                        {
                            if (paper[row, col] == '#')
                            {
                                paper[row, offset] = paper[row, col];
                                paper[row, col] = ' ';
                            }
                        }
                        
                        offset++;
                    }
                    
                    colLength -= length + 1;
                }

                if (axis == "y")
                {
                    var offset = 0;
                    
                    for (var row = rowLength; row > length; row--)
                    {
                        for (var col = 0; col <= colLength; col++)
                        {
                            if (paper[row, col] == '#')
                            {
                                paper[offset, col] = paper[row, col];    
                                paper[row, col] = ' ';
                            }
                        }

                        offset++;
                    }

                    rowLength -= length + 1;
                }

                if (isFirstFold)
                {
                    var count = Flatten(paper, rowLength, colLength).Count(x => x == '#');
                    _testOutputHelper.WriteLine($"After first instruction{instruction}: {count}");
                }

                isFirstFold = false;
            }
        }

        private static IEnumerable<char> Flatten(char[,] paper, int rowLength, int colLength)
        {
            for (var row = 0; row <= rowLength; row++)
            {
                for (var col = 0; col <= colLength; col++)
                {
                    yield return paper[row, col];
                }
            }
        }

        private void ShowGrid(char[,] paper, int rowLength, int colLength)
        {
            _testOutputHelper.WriteLine("------------");

            for (var row = 0; row <= rowLength; row++)
            {
                var line = string.Empty;
                for (var col = 0; col <= colLength; col++)
                {
                    line += paper[row, col] != '#' ? '.' : paper[row, col];
                }

                _testOutputHelper.WriteLine(line);
            }
            
            _testOutputHelper.WriteLine("------------");
        }
    }
}