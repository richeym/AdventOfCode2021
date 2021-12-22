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

        [Fact]
        public void Test1()
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
                numberOfRoutes += CheckRoute(caveMap, currentCave, path);
            } while (path.Any());

            _testOutputHelper.WriteLine(numberOfRoutes.ToString());
        }


        private int CheckRoute(
            Dictionary<string, HashSet<string>> caveMap,
            HashSet<string> currentCave,
            Stack<string> path)
        {
            var routesThisPath = 0;

            foreach (var destination in currentCave)
            {
                if (destination == "end")
                {
                    routesThisPath++;
                }
                else if (destination.All(char.IsUpper) || !path.Contains(destination))
                {
                    path.Push(destination);
                    routesThisPath += CheckRoute(caveMap, caveMap[destination], path);
                }
            }

            path.Pop();
            
            return routesThisPath;
        }
    }
}