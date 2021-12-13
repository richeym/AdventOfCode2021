using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day4
{
    public class GiantSquid
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GiantSquid(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var draws = GetDraws(data);
            var cards = GetCards(data);
            
            List<int> currentlyDrawn = new List<int>();
            bool gameWon = false;
            foreach (var draw in draws)
            {
                currentlyDrawn.Add(draw);

                foreach(var card in cards)
                {
                    if (!CardIsWinner(card, currentlyDrawn)) continue;
                    
                    var sum = CalculateWinningScore(card, currentlyDrawn);
                    _testOutputHelper.WriteLine($"Winning card score: {sum}");
                    gameWon = true;
                    break;
                }

                if (gameWon) break;
            }
        }

        [Fact]
        public void Test2()
        {
            var data = FileUtil.ParseRecords<string>("input.txt").ToList();
            var draws = GetDraws(data);
            var cards = GetCards(data);
            var winningCards = new List<int[,]>();
            
            List<int> currentlyDrawn = new List<int>();
            bool gameWon = false;
            
            foreach (var draw in draws)
            {
                currentlyDrawn.Add(draw);

                var remainingCards = cards.Where(x => !winningCards.Contains(x)).ToList();

                foreach (var card in remainingCards.Where(card => CardIsWinner(card, currentlyDrawn)))
                {
                    winningCards.Add(card);

                    if (winningCards.Count != cards.Count) continue;
                    
                    var sum = CalculateWinningScore(card, currentlyDrawn);
                    _testOutputHelper.WriteLine($"Winning card score: {sum}");
                    gameWon = true;
                    break;
                }

                if (gameWon) break;
            }
        }
        
        private int CalculateWinningScore(int[,] card, List<int> currentlyDrawn)
        {
            int sum = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (!currentlyDrawn.Contains(card[row, col]))
                    {
                        sum += card[row, col];
                    }
                }
            }

            return sum * currentlyDrawn.Last();
        }

        private bool CardIsWinner(int[,] card, List<int> currentlyDrawn)
        {
            for(int i = 0; i < 5; i++)
            {
                var rowCompleted = Enumerable.Range(0, 5)
                    .Select((x => card[i, x]))
                    .ToArray()
                    .All(currentlyDrawn.Contains);

                if (rowCompleted)
                {
                    return true;
                }
                
                var colCompleted = Enumerable.Range(0, 5)
                    .Select((x => card[x, i]))
                    .ToArray()
                    .All(currentlyDrawn.Contains);

                if (colCompleted)
                {
                    return true;
                }
            }

            return false;
        }

        private List<int> GetDraws(IEnumerable<string> data)
        {
            return data.First().Split(',').Select(int.Parse).ToList();
        }

        private List<int[,]> GetCards(IEnumerable<string> data)
        {
            var startOfCards = data.Skip(1);

            var cards = new List<int[,]>();
            int[,] card = null;
            int row = 0;
            foreach (var dataRow in startOfCards)
            {
                if (dataRow.Trim() == string.Empty)
                {
                    row = 0;
                    card = new int[5, 5];
                }
                else
                {
                    if(row == 0) cards.Add(card);

                    var columns = Regex.Split(dataRow.Trim(), @"\s+");
                    for (int col = 0; col < columns.Length; col++)
                    {
                        var no = int.Parse(columns[col]);
                        card[row, col] = no;
                    }

                    row++;
                }
            }

            return cards;
        }
    }

}