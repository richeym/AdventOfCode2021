using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day12
{
    public class PassagePathing
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PassagePathing(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Test1(bool allowMultipleSmallCaveVisits)
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var caveMap = new Dictionary<string, HashSet<string>>();

            foreach (var line in data)
            {
                var connection = line.Split('-');
                var from = connection[0];
                var to = connection[1];

                if (!caveMap.ContainsKey(from)) caveMap[from] = new HashSet<string>();
                if (!caveMap.ContainsKey(to)) caveMap[to] = new HashSet<string>();

                caveMap[from].Add(to);
                caveMap[to].Add(from);
            }

            var numberOfRoutes = 0;

            var currentCave = caveMap["start"];
            var path = new Stack<string>(new List<string> { "start" });

            do
            {
                numberOfRoutes += CheckRoute(caveMap, currentCave, path, allowMultipleSmallCaveVisits);
            } while (path.Any());

            _testOutputHelper.WriteLine(numberOfRoutes.ToString());
        }


        private int CheckRoute(
            Dictionary<string, HashSet<string>> caveMap,
            HashSet<string> currentCave,
            Stack<string> path, 
            bool allowMultipleCaveVisits)
        {
            var routesThisPath = 0;

            foreach (var destination in currentCave)
            {
                if (destination == "end")
                {
                    routesThisPath++;
                }
                else if (CanVisit(path, destination, allowMultipleCaveVisits))
                {
                    path.Push(destination);
                    routesThisPath += CheckRoute(caveMap, caveMap[destination], path, allowMultipleCaveVisits);
                }
            }

            path.Pop();
            
            return routesThisPath;
        }

        private static bool CanVisit(Stack<string> path, string destination, bool allowMultipleSmallCaveVisits)
        {
            if (destination == "start")
            {
                return false;
            }

            if (!path.Contains(destination))
            {
                return true;
            }

            if (destination.All(char.IsUpper))
            {
                return true;
            }

            if (allowMultipleSmallCaveVisits)
            {
                var anySmallCavesVisitedMoreThanOnce = path
                    .Where(x => x.All(char.IsLower))
                    .GroupBy(x => x)
                    .Select(x => new
                    {
                        x.Key,
                        Count = x.Count()
                    })
                    .Any(x => x.Count > 1);

                return !anySmallCavesVisitedMoreThanOnce;
            }
            else
            {
                return false;
            }
        }
    }
}