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

        // TODO: Return the player for the specified player number, or null if not located.
        // The playerNumber parameter must be > 0. If it is not then return a null result.
        // Note
        //   Team.Players has the players for the team.
        //   Player.PlayerNumber is the field to be compared against
        public Player PlayerByPlayerNumber(int playerNumber)
        {
            var players = TeamReferenceData.SelectMany(x => x.Players);
            return playerNumber > 0 ? players.FirstOrDefault(x => x.PlayerNumber == playerNumber) : null;
        }

        // TODO: For each team return their win % as well as their players win %, sorted by the team 'win %' highest to lowest.
        // If a teamId is specified then return data for only that team i.e. result list will only contain a single entry
        // otherwise if the teamId=0 return item data for each team in TeamReferenceData supplied in the constructor.
        // If a team is specified and cannot be located then return a empty list (list must be empty and not null).
        // NB! If any player on the team has played 100 or more matches then IStatsWeighting must be invoked with the required parameters
        //    ONLY make this call if one or more of the player matches is >= 100.
        //    The result must be stored in the PlayerWeighting field inside the TeamValue result class.
        //    If all the players within the team has played less than 100 matches each then PlayerWeighting must be set to 0.
        // Note
        //   Team Win % is Team.Victories over Team.Matches
        //   Player Win % is Player.Wins over Player.Matches i.e. the sum of all players win / matches on the team.
        public IEnumerable<TeamValue> TeamWinPercentage(int teamId = 0)
        {
            var teams = teamId > 0 ? TeamReferenceData.Where(x => x.Id == teamId).ToList() : TeamReferenceData.ToList();

            return teams.Select(team => new TeamValue
            {
                Id = team.Id,
                Name = team.Name,
                TeamWinsPercentage = CalculateTeamWinsPercentage(team),
                PlayerWeighting = CalculatePlayerWeighting(team.Players.ToList()),
                PlayerWinPercentage = CalculatePlayerWinPercentage(team.Players)
            });
        }

        public double CalculatePlayerWinPercentage(IEnumerable<Player> teamPlayers)
        {
            var players = teamPlayers.ToList();
            return (players.Sum(x => x.Wins) / players.Sum(x => x.Matches)) * 100;
        }


        private double CalculatePlayerWeighting(List<Player> players)
        {
            var player = players.FirstOrDefault(x => x.Matches >= 100);
            var playerWinPercentage = (player?.Wins / player?.Matches) * 100;
            return playerWinPercentage != null ? StatsWeighting.Apply((double)playerWinPercentage, player.Matches) : 0;
        }

        public static double CalculateTeamWinsPercentage(Team team)
        {
            return (team.Victories / team.Matches) * 100;
        }

    }
}