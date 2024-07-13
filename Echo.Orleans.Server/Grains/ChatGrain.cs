namespace Echo.Orleans.Server.Grains;

public class UserChatGrain : Grain, IUserChatGrain
{
}

public class UserGrain : Grain, IUserGrain
{
    private IUserChatGrain _currentGame;

    // Game the player is currently in. May be null.
    public Task<IUserChatGrain> GetCurrentGame()
    {
        return Task.FromResult(_currentGame);
    }

    // Game grain calls this method to notify that the player has joined the game.
    public Task JoinGame(IUserChatGrain game)
    {
        _currentGame = game;

        Console.WriteLine(
            $"Player {GetPrimaryKey()} joined game {game.GetPrimaryKey()}");

        return Task.CompletedTask;
    }

    // Game grain calls this method to notify that the player has left the game.
    public Task LeaveGame(IUserChatGrain game)
    {
        _currentGame = null;

        Console.WriteLine(
            $"Player {GetPrimaryKey()} left game {game.GetPrimaryKey()}");

        return Task.CompletedTask;
    }
}

internal interface IUserGrain : IGrainWithGuidKey
{
}

internal interface IUserChatGrain : IGrain
{
}