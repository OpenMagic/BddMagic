using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BddMagic.Tests.Samples.BowlingKata.Features
{
    [TestClass]
    public class BowlingCalculatorFeature : BddFeature
    {
        private BowlingCalculator Calculator;

        public BowlingCalculatorFeature()
            : base(
                @"As a player
                  I want the system to calculate my total score
                  So that I know my performance"
        ) { }

        [TestMethod]
        public void GutterGame()
        {
            var s = Scenario();

            s["Given a new bowling game"] =
                parameters =>
                {
                    Calculator = new BowlingCalculator();
                };

            s["When all of my balls are landing in the gutter"] =
                parameters =>
                {
                    for (int frame = 1; frame <= 10; frame++)
                    {
                        Calculator.Roll(0);
                        Calculator.Roll(0);
                    }
                };

            s["Then my total score should be '0'"] =
                parameters =>
                {
                    Calculator.Score.Should().Be(int.Parse(parameters[0]));
                };

            s.Execute();
        }

        /*
Scenario: Beginners game
  Given a new bowling game
  When I roll 2 and 7
  And I roll 3 and 4
  And I roll 8 times 1 and 1
  Then my total score should be 32
  
Scenario: Another beginners game
  Given a new bowling game
  When I roll the following series:	2,7,3,4,1,1,5,1,1,1,1,1,1,1,1,1,1,1,5,1
  Then my total score should be 40
  
Scenario: All Strikes
  Given a new bowling game
  When all of my rolls are strikes
  Then my total score should be 300
  
Scenario: One single spare
   Given a new bowling game 
   When I roll the following series: 2,8,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
   Then my total score should be 29
   
Scenario: All spares
  Given a new bowling game
  When I roll 10 times 1 and 9
  And I roll 1
  Then my total score should be 110
         */
    }
}
