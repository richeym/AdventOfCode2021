using System;
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

        public SyntaxScoring(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = FileUtil.ParseRecords<string>("input.txt");

            var firstIllegalChars = new List<char>();
            
            foreach (var record in data)
            {
                var stack = new Stack<char>();
                
                foreach (var op in record)
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
                            break;
                        }
                    }
                }
            }

            int errorSum = firstIllegalChars.Sum(x => _illegalCharScores[_closingOperators.IndexOf(x)]);
            _testOutputHelper.WriteLine(errorSum.ToString());
        }
    }
}