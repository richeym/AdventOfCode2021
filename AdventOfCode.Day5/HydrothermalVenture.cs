using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day5
{
    public class HydrothermalVenture
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public HydrothermalVenture(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            var segments = ReadSegments(data)
                .Where(segment => segment.Start.X == segment.End.X || segment.Start.Y == segment.End.Y)
                .ToList();

            var grid = SetupGrid(segments);

            DrawMap(segments, grid);

            var dangerousSquares = CalculateDangerousSquares(grid);

            _testOutputHelper.WriteLine($"{dangerousSquares}");
        }

        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();

            var segments = ReadSegments(data).ToList();

            var grid = SetupGrid(segments);

            DrawMap(segments, grid);

            var dangerousSquares = CalculateDangerousSquares(grid);

            _testOutputHelper.WriteLine($"{dangerousSquares}");
        }
        
        private static int CalculateDangerousSquares(int[,] grid)
        {
            int dangerousSquares = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] > 1)
                    {
                        dangerousSquares++;
                    }
                }
            }

            return dangerousSquares;
        }

        private static void DrawMap(List<Segment> segments, int[,] grid)
        {
            foreach (var segment in segments)
            {
                if (segment.Start.X == segment.End.X)
                {
                    var start = Math.Min(segment.Start.Y, segment.End.Y);
                    var end = Math.Max(segment.Start.Y, segment.End.Y);

                    for (var y = start; y <= end; y++)
                    {
                        grid[segment.Start.X, y]++;
                    }
                }
                else if(segment.Start.Y == segment.End.Y)
                {
                    var start = Math.Min(segment.Start.X, segment.End.X);
                    var end = Math.Max(segment.Start.X, segment.End.X);

                    for (var x = start; x <= end; x++)
                    {
                        grid[x, segment.Start.Y]++;
                    }
                }
                else
                {
                    var startSegment = segment.Start.X < segment.End.X ? segment.Start : segment.End;
                    var endSegment = segment.Start.X > segment.End.X ? segment.Start : segment.End;
                    var yOffsetMultiplier = startSegment.Y > endSegment.Y ? -1 : 1;
                    
                    int yOffset = 0;
                    for (int x = startSegment.X; x <= endSegment.X; x++)
                    {
                        grid[x, startSegment.Y + yOffset * yOffsetMultiplier]++;
                        yOffset++;
                    }
                }
            }
        }

        private static int[,] SetupGrid(List<Segment> segments)
        {
            var gridWidth = Math.Max(segments.Max(x => x.Start.X), segments.Max(x => x.End.X));
            var gridHeight = Math.Max(segments.Max(x => x.Start.Y), segments.Max(x => x.End.Y));
            return new int[gridWidth + 1, gridHeight + 1];
        }

        private static IEnumerable<Segment> ReadSegments(List<string> data)
        {
            foreach (var record in data)
            {
                var matches = Regex.Matches(record, @"^(\d+),(\d+) -> (\d+),(\d+).*$");
                var segment = new Segment
                {
                    Start = new Point
                    {
                        X = int.Parse(matches[0].Groups[1].Value),
                        Y = int.Parse(matches[0].Groups[2].Value)
                    },
                    End = new Point
                    {
                        X = int.Parse(matches[0].Groups[3].Value),
                        Y = int.Parse(matches[0].Groups[4].Value)
                    }
                };

                yield return segment;
            }
        }

        public struct Segment
        {
            public Point Start { get; set; }
            public Point End { get; set; }
        }

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}