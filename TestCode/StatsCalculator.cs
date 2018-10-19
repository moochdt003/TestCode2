using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TestCode.Models;

namespace TestCode
{
    public class StatsCalculator
    {
        public IEnumerable<Team> TeamReferenceData { get; set; }
        public IStatsWeighting StatsWeighting { get; set; }

        public StatsCalculator(IEnumerable<Team> teamReferenceData, IStatsWeighting statsWeighting)
        {
            TeamReferenceData = teamReferenceData;
            StatsWeighting = statsWeighting;
        }

        public Player PlayerByPlayerNumber(int playerNumber)
        {
            var players = TeamReferenceData.SelectMany(x => x.Players);
            return playerNumber > 0 ? players.FirstOrDefault(x => x.PlayerNumber == playerNumber) : null;
        }

        public IEnumerable<TeamValue> TeamWinPercentage(int teamId = 0)
        {
            var teams = teamId > 0 ? TeamReferenceData.Where(x => x.Id == teamId).ToList() : TeamReferenceData.ToList();

            return teams.Select(team => new TeamValue
            {
                Id = team.Id,
                Name = team.Name,
                TeamWinsPercentage = CalculateTeamWinsPercentage(team),
                // Assessment instructions were not very clear. I had to assume weighting is based on the first player with over 100 matches.
                // Assuming this because TeamValue object is team based not player based. Yet the method requires a player number of matches.
                PlayerWeighting = CalculatePlayerWeighting(team.Players),
                PlayerWinPercentage = CalculatePlayersWinPercentage(team.Players)
            });
        }

        public static double CalculatePlayersWinPercentage(IEnumerable<Player> teamPlayers)
        {
            var players = teamPlayers.ToList();
            return (double)players.Sum(x => x.Wins) / players.Sum(x => x.Matches) * 100;
        }


        public double CalculatePlayerWeighting(IEnumerable<Player> players)
        {
            var player = players.FirstOrDefault(x => x.Matches >= 100);
            if (player == null)
            {
                return 0;
            }
            var playerWinPercentage = (double)player.Wins / player.Matches * 100;
            return StatsWeighting.Apply(playerWinPercentage, player.Matches);
        }

        public static double CalculateTeamWinsPercentage(Team team)
        {
            return (double)team.Victories / team.Matches * 100;
        }

    }
}