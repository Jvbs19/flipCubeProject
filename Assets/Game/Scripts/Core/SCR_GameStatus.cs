public static class SCR_GameStatus
{
    static int redPoints;
    static int bluePoints;
    static int greenPoints;
    static int yellowPoints;
    static int gameMovements;
    public static void AddRedPoints()
    {
        redPoints++;
    }
    public static void AddBluePoints()
    {
        bluePoints++;
    }
    public static void AddGreenPoints()
    {
        greenPoints++;
    }
    public static void AddYellowPoints()
    {
        yellowPoints++;
    }

    public static void RemoveRedPoints()
    {
            redPoints--;
    }
    public static void RemoveBluePoints()
    {
            bluePoints--;
    }
    public static void RemoveGreenPoints()
    {
            greenPoints--;
    }
    public static void RemoveYellowPoints()
    {
            yellowPoints--;
    }

    public static int GetRedPoints()
    {
        return redPoints;
    }
    public static int GetBluePoints()
    {
        return bluePoints;
    }
    public static int GetGreenPoints()
    {
        return greenPoints;
    }
    public static int GetYellowPoints()
    {
        return yellowPoints;
    }
    public static bool CheckRedPoints(int val)
    {
        if (redPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckBluePoints(int val)
    {
        if (bluePoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckYellowPoints(int val)
    {
        if (yellowPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static bool CheckGreenPoints(int val)
    {
        if (greenPoints >= val)
        {
            return true;
        }
        return false;
    }
    public static void ResetAllPoints()
    {
        redPoints = 0;
        bluePoints = 0;
        greenPoints = 0;
        yellowPoints = 0;
    }

    public static void ResetAll()
    {
        redPoints = 0;
        bluePoints = 0;
        greenPoints = 0;
        yellowPoints = 0;
        gameMovements = 0;
    }

    public static void SetMovementLimit(int val)
    {
        gameMovements = val;
    }
    public static void DecreaseMovement()
    {
        gameMovements--;
    }
    public static void IncreaseMovement()
    {
        gameMovements++;
    }

    public static int CheckMovimentsLeft()
    {
        return gameMovements;
    }
}
