using System;
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
        public void PlayerByPlayerNumber_playerNumber5_return_PlayerNumber5_from_SampleData()
        {
            //Arrange 
            var playerNumber = 5;
            var expected = "Michał Bąkiewicz";

            //Act
            var result = _statsCalculator.PlayerByPlayerNumber(playerNumber);
            var actual = result.Name;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayerByPlayerNumber_playerNumber0_return_null()
        {
            //Arrange 
            var playerNumber = 0;

            //Act
            var result = _statsCalculator.PlayerByPlayerNumber(playerNumber);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PlayerByPlayerNumber_return_Null()
        {
            //Arrange
            var nonExistentPlayerNumber = 123;

            //Act
            var actual = _statsCalculator.PlayerByPlayerNumber(nonExistentPlayerNumber);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void CalculateTeamWinsPercentage_return_10Percent()
        {
            //Arrange
            var team = new Team { Matches = 100, Victories = 10 };
            var expected = 10;

            //Act
            var actual = StatsCalculator.CalculateTeamWinsPercentage(team);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculatePlayerWinPercentage_Return_10Percent()
        {
            //Arrange
            var team = new Team
            {
                Id = 1
            };

            var player = new Player()
            {
                Matches = 100,
                TeamId = 1,
                Wins = 10

            };
            team.Players.Add(player);

            //Act
            var actual = Math.Round(StatsCalculator.CalculatePlayerWinPercentage(team.Players), 2);

            //Assert
            Assert.AreEqual(10, actual);
        }

        [TestMethod]
        public void TeamWinPercentage_return_EmptyTeamValueList()
        {
            //Arrange
            var nonExistentTeamId = 12;
            var expected = new List<TeamValue>();

            //Act
            var actual = _statsCalculator.TeamWinPercentage(nonExistentTeamId).ToList();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TeamWinPercentage_return_Single_in_TeamValueList()
        {
            //Arrange
            var team = new Team
            {
                Id = 1,
                Matches = 100,
                Victories = 10,
                Name = "DavidTeam",
            };

            var player = new Player()
            {
                Matches = 100,
                TeamId = 1,
                Wins = 10

            };
            team.Players.Add(player);
            var teams = new List<Team> {team};
            var statsCalculator = new StatsCalculator(teams, new StatsWeightingStub());
            var teamValue = new TeamValue
            {
                Id = 1,Name = "DavidTeam",
                TeamWinsPercentage = 10,
                PlayerWinPercentage = 10,
                PlayerWeighting = 1
            };
            var expected = new List<TeamValue>{ teamValue };

            //Act
            var actual = statsCalculator.TeamWinPercentage(1).ToList();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
