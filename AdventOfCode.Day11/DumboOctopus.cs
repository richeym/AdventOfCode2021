using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day11
{
    public class DumboOctopus
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DumboOctopus(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt");
            int gridSize = data.Count;

            var grid = new int[gridSize, gridSize];

            for (var row = 0; row < gridSize; row++)
            {
                for (var col = 0; col < gridSize; col++)
                {
                    grid[row, col] = int.Parse(data[row][col].ToString());
                }
            }

            int totalFlashes = 0;

            var flashedThisStep = new List<Tuple<int, int>>();

            for (var step = 1; step <= 100; step++)
            {
                flashedThisStep.Clear();

                for (var row = 0; row < gridSize; row++)
                {
                    for (var col = 0; col < gridSize; col++)
                    {
                        grid[row, col]++;
                    }
                }

                var flashedThisEvaluation = new List<Tuple<int, int>>();

                do
                {
                    flashedThisEvaluation.Clear();
                    for (var row = 0; row < gridSize; row++)
                    {
                        for (var col = 0; col < gridSize; col++)
                        {
                            if (grid[row, col] > 9 && !flashedThisStep.Any(x => x.Item1 == row && x.Item2 == col))
                            {
                                flashedThisEvaluation.Add(new Tuple<int, int>(row, col));
                                flashedThisStep.Add(new Tuple<int, int>(row, col));
                                totalFlashes++;
                            }
                        }
                    }

                    foreach (var flashingOctopus in flashedThisEvaluation)
                    {
                        if (flashingOctopus.Item1 > 0)
                        {
                            // left
                            grid[flashingOctopus.Item1 - 1, flashingOctopus.Item2]++;

                            if (flashingOctopus.Item2 > 0)
                            {
                                // upper left
                                grid[flashingOctopus.Item1 - 1, flashingOctopus.Item2 - 1]++;
                            }

                            if (flashingOctopus.Item2 < gridSize - 1)
                            {
                                // lower left
                                grid[flashingOctopus.Item1 - 1, flashingOctopus.Item2 + 1]++;
                            }
                        }

                        if (flashingOctopus.Item2 > 0)
                        {
                            // up
                            grid[flashingOctopus.Item1, flashingOctopus.Item2 - 1]++;
                        }

                        if (flashingOctopus.Item2 < gridSize - 1)
                        {
                            // down
                            grid[flashingOctopus.Item1, flashingOctopus.Item2 + 1]++;
                        }

                        if (flashingOctopus.Item1 < gridSize - 1)
                        {
                            // right
                            grid[flashingOctopus.Item1 + 1, flashingOctopus.Item2]++;

                            if (flashingOctopus.Item2 > 0)
                            {
                                // upper right
                                grid[flashingOctopus.Item1 + 1, flashingOctopus.Item2 - 1]++;
                            }

                            if (flashingOctopus.Item2 < gridSize - 1)
                            {
                                // lower right
                                grid[flashingOctopus.Item1 + 1, flashingOctopus.Item2 + 1]++;
                            }
                        }
                    }
                } while (flashedThisEvaluation.Any());

                for (var row = 0; row < gridSize; row++)
                {
                    for (var col = 0; col < gridSize; col++)
                    {
                        if (grid[row, col] > 9)
                        {
                            grid[row, col] = 0;
                        }
                    }
                }
            }

            _testOutputHelper.WriteLine(totalFlashes.ToString());
        }

        private void WriteGrid(int gridSize, int[,] grid)
        {
            for (var row = 0; row < gridSize; row++)
            {
                var line = string.Empty;
                for (var col = 0; col < gridSize; col++)
                {
                    line = line + grid[row, col];
                }

                _testOutputHelper.WriteLine(line);
            }
        }

        // for each cycle
        //   increase every octopus energy level by 1
        //   while any octopuses have an energy level of 10
        //     boost surrounding octopus energy levels by 1
        //   count all octopuses with an energy level greater than 9
        //   add to the total number of flashes
        //   reset all octopuses with an energy level greater than 9 to 0
    }
}