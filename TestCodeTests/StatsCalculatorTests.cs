using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCode;
using TestCode.Models;

namespace TestCodeTests
{
    [TestClass]
    public class StatsCalculatorTests
    {
        private StatsCalculator _statsCalculator;
        [TestInitialize]
        public void Initialize()
        {
            _statsCalculator = new StatsCalculator(SampleData.Teams, new StatsWeightingStub());
        }

        [TestMethod]
        public void PlayerByPlayerNumber_return_SelectedPlayer()
        {

            //Act
            var result = _statsCalculator.PlayerByPlayerNumber(5);

            //Assert
            Assert.AreEqual("Michał Bąkiewicz", result.Name);
        }

        [TestMethod]
        public void PlayerByPlayerNumber_return_Null()
        {
            //Act
            var result = _statsCalculator.PlayerByPlayerNumber(123);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CalculateTeamWinsPercentage_test()
        {
            //Arrange
            var team = new Team { Matches = 100, Victories = 100 };
            //Act
            var result = StatsCalculator.CalculateTeamWinsPercentage(team);
            //Assert
            Assert.AreEqual(100, result);
        }


        [TestMethod]
        public void CalculatePlayerWinPercentage_test()
        {
            //Arrange
            var team = _statsCalculator.TeamReferenceData.Single(x => x.Id == 1);
            //Act
            var _result = _statsCalculator.CalculatePlayerWinPercentage(team.Players);
            //Assert
            Assert.AreEqual(100, _result);
        }

        //[TestMethod]
        //public void LivingCell_HasZeroLivingNeighbours_Dies()
        //{
        //    //Arrange
        //    const int livingNeigbours = 0;

        //    //Act
        //    var result = GameOfLifeRules.ChangeCellState(true, livingNeigbours);

        //    //Assert
        //    Assert.AreEqual(false, result);
        //}
    }
}
