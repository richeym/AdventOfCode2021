using System;
using System.Linq;
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

                if (isFirstFold)
                {
                    
                }
                
                isFirstFold = false;
            }
            
            ShowGrid(paper, rowLength, colLength);
        }

        private void ShowGrid(char[,] paper, int rowLength, int colLength)
        {
            for (var row = 0; row <= rowLength; row++)
            {
                var line = string.Empty;
                for (var col = 0; col <= colLength; col++)
                {
                    line += paper[row, col] != '#' ? '.' : paper[row, col];
                }
                _testOutputHelper.WriteLine(line);
            }
        }
    }
}