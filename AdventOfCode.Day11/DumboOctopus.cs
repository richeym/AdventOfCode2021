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

            void UpdateGrid(Func<int, int, int> func)
            {
                for (var row = 0; row < gridSize; row++)
                {
                    for (var col = 0; col < gridSize; col++)
                    {
                        grid[row, col] = func(row, col);
                    }
                }
            }

            UpdateGrid((r, c) => int.Parse(data[r][c].ToString()));

            var totalFlashes = 0;

            var flashedThisStep = new List<Tuple<int, int>>();

            const int stepToCheck = 100;
            bool foundAnswerToPart1 = false, foundAnswerToPart2 = false;

            for (var step = 1; !foundAnswerToPart1 || !foundAnswerToPart2; step++)
            {
                flashedThisStep.Clear();

                UpdateGrid((r, c) => grid[r, c] + 1);

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

                UpdateGrid((r, c) => grid[r, c] > 9 ? 0 : grid[r, c]);

                if (step == stepToCheck)
                {
                    _testOutputHelper.WriteLine($"Flashes after step {step}: {totalFlashes}");
                    foundAnswerToPart1 = true;
                }

                if (flashedThisStep.Count == gridSize * gridSize)
                {
                    _testOutputHelper.WriteLine($"Synchronised on step {step}");
                    foundAnswerToPart2 = true;
                }
            }
        }
    }
}