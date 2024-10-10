using TennisChallenge.Core.DTO.Player;
using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Core.DomainServices;

public interface IMatchService
{
    IReadOnlyCollection<Match> GenerateMatchesWithResults(List<Player> players, Tournament tournament);
}

internal sealed class MatchService
    : IMatchService
{
    public MatchService()
    {
    }

    private List<Match> GenerateInitialMatches(List<Player> players, Tournament tournament, int round)
    {
        var response = new List<Match>();
        var playerCount = players.Count;

        // Calculate time between matches based on tournament duration
        var tournamentDuration = tournament.EndDate - tournament.StartDate;
        var matchInterval = TimeSpan.FromTicks(tournamentDuration.Ticks / tournament.MatchesCount);

        for (var i = 0; i < playerCount / 2; i++) // Use playerCount/2 because each match needs 2 players
        {
            var matchDate = tournament.StartDate.Add(matchInterval * i);
            var match = Match.CreateInitial(
                Guid.NewGuid(),
                players[i * 2].Id, // Player 1
                players[i * 2 + 1].Id, // Player 2
                tournament.Id,
                matchDate,
                round
            );

            response.Add(match);
        }

        return response;
    }

    public IReadOnlyCollection<Match> GenerateMatchesWithResults(List<Player> players, Tournament tournament)
    {
        var allMatches = new List<Match>();

        var currentRound = 1;
        var remainingPlayers = new List<Player>(players);

        while (remainingPlayers.Count > 1)
        {
            var matches = GenerateInitialMatches(remainingPlayers, tournament, currentRound);
            allMatches.AddRange(matches);

            remainingPlayers = DetermineWinners(matches, remainingPlayers);
            currentRound++;
        }

        return allMatches.AsReadOnly();
    }

    private List<Player> DetermineWinners(List<Match> matches, List<Player> players)
    {
        var winners = new List<Player>();

        foreach (var match in matches)
        {
            var playerOne = players.Find(x => x.Id == match.PlayerOneId);
            var playerTwo = players.Find(x => x.Id == match.PlayerTwoId);

            int oneWinProbability;
            int twoWinProbability;

            if (playerOne is MalePlayer malePlayerOne && playerTwo is MalePlayer malePlayerTwo)
            {
                oneWinProbability = malePlayerOne.CalculateWinProbability();
                twoWinProbability = malePlayerTwo.CalculateWinProbability();

                match.WinnerId = oneWinProbability > twoWinProbability ? playerOne.Id : playerTwo.Id;
            }
            else if (playerOne is FemalePlayer femalePlayerOne && playerTwo is FemalePlayer femalePlayerTwo)
            {
                oneWinProbability = femalePlayerOne.CalculateWinProbability();
                twoWinProbability = femalePlayerTwo.CalculateWinProbability();

                match.WinnerId = oneWinProbability > twoWinProbability ? playerOne.Id : playerTwo.Id;
            }
            else
            {
                throw new InvalidOperationException("Players must be of the same gender.");
            }
            
            var winner = match.WinnerId == playerOne.Id ? playerOne : playerTwo;
            winners.Add(winner);
        }

        return winners;
    }
}