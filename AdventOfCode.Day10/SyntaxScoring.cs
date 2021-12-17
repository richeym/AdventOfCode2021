using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Util;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Day10
{
    public class SyntaxScoring
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly List<char> _openingOperators = new() { '(', '[', '{', '<', };
        private readonly List<char> _closingOperators = new() { ')', ']', '}', '>', };
        private readonly List<int> _illegalCharScores = new() { 3, 57, 1197, 25137 };
        private readonly List<int> _incompleteCharScores = new() { 1, 2, 3, 4 };

        public SyntaxScoring(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt");

            var firstIllegalChars = new List<char>();
            var incompleteSequenceScores = new List<long>();

            foreach (var record in data)
            {
                var stack = new Stack<char>();
                var validInput = true;

                foreach (var op in record.TakeWhile(_ => validInput))
                {
                    if (_openingOperators.Contains(op))
                    {
                        stack.Push(op);
                    }
                    else
                    {
                        var expectedOpeningOperator = _openingOperators[_closingOperators.IndexOf(op)];
                    
                        if (expectedOpeningOperator != stack.Pop())
                        {
                            firstIllegalChars.Add(op);
                            validInput = false;
                        }
                    }
                }

                if (validInput)
                {
                    long score = 0;
                    
                    while (stack.Any())
                    {
                        var unclosedTag = stack.Pop();
                        var matchedScore = _incompleteCharScores[_openingOperators.IndexOf(unclosedTag)];
                        score = score * 5 + matchedScore;
                    }

                    incompleteSequenceScores.Add(score);
                }
            }

            var errorSum = firstIllegalChars.Sum(x => _illegalCharScores[_closingOperators.IndexOf(x)]);
            var middleScore = incompleteSequenceScores
                .OrderBy(x => x)
                .Skip(incompleteSequenceScores.Count / 2)
                .Take(1)
                .Single();
            
            _testOutputHelper.WriteLine($"Error sum: {errorSum.ToString()}");
            _testOutputHelper.WriteLine($"Middle score: {middleScore.ToString()}");
        }
    }
}