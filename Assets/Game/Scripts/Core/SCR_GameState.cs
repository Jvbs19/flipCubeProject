public enum GameState
{
    wait,
    move,
    ended,
    debug
}

public static class SCR_GameState
{

    static GameState _currentGameState = GameState.move;
    public static void SetCurrentGameState(GameState state)
    {
        _currentGameState = state;
    }

    public static GameState CheckCurrentGameState()
    {
        return _currentGameState;
    }

    public static bool IsGameStateWait()
    {
        if (_currentGameState == GameState.wait)
            return true;

        return false;
    }

    public static bool IsGameStateMove()
    {
        if (_currentGameState == GameState.move)
            return true;

        return false;
    }

    public static bool IsGameStateEnded()
    {
        if (_currentGameState == GameState.ended)
            return true;

        return false;
    }

    public static bool IsGameStateDebug()
    {
        if (_currentGameState == GameState.debug)
            return true;

        return false;
    }
}
