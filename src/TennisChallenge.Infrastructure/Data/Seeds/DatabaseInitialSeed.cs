using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Infrastructure.Data.Seeds;

public static class DatabaseInitialSeed
{
    public static Player[] GetPlayersSeed => new Player[]
    {
        MalePlayer.Create(
            id: new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"),
            name: "John Doe",
            country: "USA",
            level: 5,
            age: 25,
            gender: "Male",
            force: 90,
            velocity: 85,
            wins: 30,
            losses: 10
        ),
        FemalePlayer.Create(
            id: new Guid("def78e39-829c-4463-ba33-ac51c85d20fc"),
            name: "Jane Smith",
            country: "UK",
            level: 6,
            age: 24,
            gender: "Female",
            reactionTime: 80,
            wins: 40,
            losses: 5
        ),
        FemalePlayer.Create(
            id: new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"),
            name: "Alice Johnson",
            country: "Canada",
            level: 4,
            age: 22,
            gender: "Female",
            reactionTime: 75,
            wins: 25,
            losses: 15
        ),
        MalePlayer.Create(
            id: new Guid("4b9d627f-836e-4c43-954c-baaf1053a035"),
            name: "Bob Brown",
            country: "Australia",
            level: 7,
            age: 28,
            gender: "Male",
            force: 95,
            velocity: 90,
            wins: 50,
            losses: 8
        ),
        MalePlayer.Create(
            id: new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"),
            name: "Michael Green",
            country: "Germany",
            level: 6,
            age: 26,
            gender: "Male",
            force: 88,
            velocity: 80,
            wins: 35,
            losses: 12
        ),
        FemalePlayer.Create(
            id: new Guid("8d8e4428-ac41-47ab-a50f-11001b45ccfa"),
            name: "Maria Rodriguez",
            country: "Spain",
            level: 5,
            age: 23,
            gender: "Female",
            reactionTime: 82,
            wins: 28,
            losses: 18
        ),
        MalePlayer.Create(
            id: new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"),
            name: "Alex Kim",
            country: "South Korea",
            level: 8,
            age: 30,
            gender: "Male",
            force: 92,
            velocity: 87,
            wins: 60,
            losses: 10
        ),
        FemalePlayer.Create(
            id: new Guid("71b73c9f-1912-434d-9e8f-e22f8ba6a4e9"),
            name: "Sophie Laurent",
            country: "France",
            level: 7,
            age: 27,
            gender: "Female",
            reactionTime: 85,
            wins: 45,
            losses: 12
        ),
        MalePlayer.Create(
            id: new Guid("01416867-5fa7-406b-8509-5e00c0d95291"),
            name: "Liam O'Connor",
            country: "Ireland",
            level: 5,
            age: 24,
            gender: "Male",
            force: 83,
            velocity: 79,
            wins: 27,
            losses: 20
        ),
        FemalePlayer.Create(
            id: new Guid("d500a7ce-3b77-43d3-81f6-61d31bde1447"),
            name: "Isabella Rossi",
            country: "Italy",
            level: 6,
            age: 25,
            gender: "Female",
            reactionTime: 78,
            wins: 30,
            losses: 14
        )
    };

    public static Match[] GetMatchesSeed => new Match[]
    {
        Match.Create(
            id: new Guid("ad88611c-c2e2-4c47-ab2d-0a550bf29f95"),
            playerOneId: new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"), // John Doe
            playerTwoId: new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"), // Bob Brown
            tournamentId: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"), // Spring Championship
            round:1,
            playerOnePoints: 4,
            playerTwoPoints: 2,
            winnerId: new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"), // John Doe wins
            matchDate: DateTime.UtcNow.AddDays(-5)
        ),
        Match.Create(
            id: new Guid("e164dd7e-862e-4124-8f0b-60f9b59c660a"),
            playerOneId:new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"), // Michael Green
            playerTwoId: new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"), // Alex Kim
            tournamentId: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"), // Summer Showdown
            playerOnePoints: 3,
            playerTwoPoints: 5,
            round:1,
            winnerId: new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"), // Alex Kim wins
            matchDate: DateTime.UtcNow.AddDays(-3)
        ),
        Match.Create(
            id: new Guid("d1f17fcd-edea-424c-95c5-d9b5f4188506"),
            playerOneId: new Guid("def78e39-829c-4463-ba33-ac51c85d20fc"), // Jane Smith
            playerTwoId: new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"),// Alice Johnson
            tournamentId: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"), // Spring Championship
            playerOnePoints: 2,
            playerTwoPoints: 4,
            round:1,
            winnerId:new Guid("c03723d9-369a-4a0b-bbab-f9215222a6d3"), // Alice Johnson wins
            matchDate: DateTime.UtcNow.AddDays(-4)
        ),
        Match.Create(
            id: new Guid("cdab142a-b165-4869-88a3-a40d594e5dc0"),
            playerOneId: new Guid("8d8e4428-ac41-47ab-a50f-11001b45ccfa"), // Maria Rodriguez
            playerTwoId: new Guid("71b73c9f-1912-434d-9e8f-e22f8ba6a4e9"), // Sophie Laurent
            tournamentId: new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"), // Summer Showdown
            playerOnePoints: 5,
            round:1,
            playerTwoPoints: 3,
            winnerId:new Guid("8d8e4428-ac41-47ab-a50f-11001b45ccfa"), // Maria Rodriguez wins
            matchDate: DateTime.UtcNow.AddDays(-2)
        ),
        Match.Create(
            id: new Guid("6dbec3cb-8f67-4e45-abd9-3b811de6dfbf"),
            playerOneId: new Guid("71b73c9f-1912-434d-9e8f-e22f8ba6a4e9"), // Liam O'Connor
            playerTwoId: new Guid("d500a7ce-3b77-43d3-81f6-61d31bde1447"), // Isabella Rossi
            tournamentId: new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"), // Summer Showdown
            playerOnePoints: 4,
            round:1,
            playerTwoPoints: 6,
            winnerId:new Guid("d500a7ce-3b77-43d3-81f6-61d31bde1447"), // Isabella Rossi wins
            matchDate: DateTime.UtcNow.AddDays(-1)
        ),
    };

    public static Tournament[] GetTournamentsSeed => new Tournament[]
    {
        Tournament.Create(
            id: new Guid("6102ca09-c32a-4b55-9938-f4d39ec8afad"),
            startedDate: DateTime.UtcNow.AddDays(-15),
            endedDate: DateTime.UtcNow.AddDays(-10),
            name: "Spring Championship",
            tournamentType: "Male",
            matchesCount: 8
        ),
        Tournament.Create(
            id: new Guid("1f783aab-2a28-4371-917a-330b56f6bf1a"),
            startedDate: DateTime.UtcNow.AddDays(-8),
            endedDate: DateTime.UtcNow.AddDays(-2),
            name: "Summer Showdown",
            tournamentType: "Male",
            matchesCount: 8
        ),
        Tournament.Create(
            id: new Guid("8d62d922-192d-40cf-acc4-8430b030c37a"),
            startedDate: DateTime.UtcNow.AddDays(-5),
            endedDate: DateTime.UtcNow,
            name: "Female Autumn Invitational",
            tournamentType: "Female",
            matchesCount: 8
        ),
        Tournament.Create(
            id: new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83"),
            startedDate: DateTime.UtcNow.AddDays(-5),
            endedDate: DateTime.UtcNow.AddDays(1),
            name: "Male Autumn Invitational",
            tournamentType: "Male",
            matchesCount: 8
        ),
    };
    
    public static PlayerTournament[] GetPlayerTournament => new PlayerTournament[]
    {
        PlayerTournament.Create(
            new Guid("b5e2c59e-d935-45b6-b087-1d74e4e11378"),
            new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83")
        ),
        PlayerTournament.Create(
            new Guid("4b9d627f-836e-4c43-954c-baaf1053a035"),
            new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83")
        ),
        PlayerTournament.Create(
            new Guid("8354cd01-71fa-47c2-a66f-6042ee0907ac"),
            new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83")
        ),
        PlayerTournament.Create(
            new Guid("02891735-bd59-4c05-b5bd-9e203ec8f45e"),
            new Guid("c3d4097b-d01e-4e28-aa64-56233b158d83")
        ),
    };
}