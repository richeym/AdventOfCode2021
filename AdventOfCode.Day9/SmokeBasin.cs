using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day9
{
    public class SmokeBasin
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SmokeBasin(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            var map = new Map(data);

            var riskLevel = 0;

            for (var row = 0; row < map.Rows; row++)
            {
                for (var col = 0; col < map.Cols; col++)
                {
                    if (map.IsLowPoint(row, col))
                    {
                        riskLevel += 1 + map[row, col];
                    }
                }
            }

            _testOutputHelper.WriteLine(riskLevel.ToString());
        }

        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            var map = new Map(data);

            var basinSizes = new List<int>();
            
            for (var row = 0; row < map.Rows; row++)
            {
                for (var col = 0; col < map.Cols; col++)
                {
                    if (map.IsLowPoint(row, col))
                    {
                        var basinSize = map.GetBasinSize(row, col);
                        basinSizes.Add(basinSize);
                    }
                }
            }

            var top3 = basinSizes.OrderByDescending(x => x).Take(3).ToList();
            _testOutputHelper.WriteLine($"{top3[0] * top3[1] * top3[2]}");
        }
        
        public class Map
        {
            private readonly int[,] _map;

            public int Rows { get; }

            public int Cols { get;  }

            public Map(IList<string> data)
            {
                Rows = data.Count;
                Cols = data.First().Length;

                _map = new int[Rows, Cols];

                for (var row = 0; row < Rows; row++)
                {
                    for (var col = 0; col < Cols; col++)
                    {
                        _map[row, col] = int.Parse(data[row][col].ToString());
                    }
                }
            }

            public int this[int row, int col] => _map[row, col];

            public bool IsLowPoint(int row, int col)
            {
                int currentHeight = _map[row, col];
                
                if (row > 0)
                {
                    if (currentHeight >= _map[row - 1, col]) return false;
                }

                if (row < Rows - 1)
                {
                    if (currentHeight >= _map[row + 1, col]) return false;
                }

                if (col > 0)
                {
                    if (currentHeight >= _map[row, col - 1]) return false;
                }

                if (col < Cols - 1)
                {
                    if (currentHeight >= _map[row, col + 1]) return false;
                }

                return true;
            }

            public int GetBasinSize(int row, int col)
            {
                var trackedLocations = new bool[Rows, Cols];
                
                trackedLocations[row, col] = true;

                return CalculateBasinSize(ref trackedLocations, row, col);
            }

            private int CalculateBasinSize(ref bool[,] trackedLocations, int row, int col)
            {
                int basinSize = 1;

                trackedLocations[row, col] = true;
                
                // move left
                if (col > 0 && _map[row, col - 1] < 9 && !trackedLocations[row, col - 1])
                {
                    basinSize += CalculateBasinSize(ref trackedLocations, row , col - 1);
                }

                // move up
                if (row > 0 && _map[row - 1, col] < 9  && !trackedLocations[row -1 , col])
                {
                    basinSize += CalculateBasinSize(ref trackedLocations,row - 1 , col);
                }
                
                // move down
                if (row < Rows - 1 && _map[row + 1, col] < 9  && !trackedLocations[row + 1, col])
                {
                    basinSize += CalculateBasinSize(ref trackedLocations,row + 1 , col);
                }
                
                // move right
                if (col < Cols - 1 && _map[row, col + 1] < 9  && !trackedLocations[row , col + 1])
                {
                    basinSize += CalculateBasinSize(ref trackedLocations,row  , col + 1);
                }
                
                return basinSize;
            }
        }
    }
}