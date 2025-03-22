using UnityEngine;

public class SCR_ConditionManager : MonoBehaviour
{
    [SerializeField] SCR_ConditionInfo m_gameCondition;
    [SerializeField] SCR_UIManager m_uiManager;

    int _rRedPoints;
    int _rBluePoints;
    int _rGreenPoints;
    int _rYellowPoints;

    void SetUpInfo()
    {
        SCR_GameStatus.ResetAll();

        _rRedPoints = m_gameCondition.GetRequiredRedPoints();
        _rBluePoints = m_gameCondition.GetRequiredBluePoints();
        _rGreenPoints = m_gameCondition.GetRequiredGreenPoints();
        _rYellowPoints = m_gameCondition.GetRequiredYellowPoints();
        SCR_GameStatus.SetMovementLimit(m_gameCondition.GetMaxMoviments());


        m_uiManager.SetupRequiredPoints(_rRedPoints, _rBluePoints, _rGreenPoints, _rYellowPoints);
        m_uiManager.UpdateActualPoints();
    }
    void Start()
    {
        SetUpInfo();
    }

    public void CheckWinCondition()
    {
        if (!SCR_GameState.IsGameStateEnded())
        {
            if (SCR_GameStatus.CheckRedPoints(_rRedPoints) && SCR_GameStatus.CheckBluePoints(_rBluePoints) && SCR_GameStatus.CheckGreenPoints(_rGreenPoints) && SCR_GameStatus.CheckYellowPoints(_rYellowPoints))
            {
                SCR_GameState.SetCurrentGameState(GameState.ended);
                m_uiManager.SetVictoryScreen();
            }
            else
            {
                if (SCR_GameStatus.CheckMovimentsLeft() <= 0)
                {
                    SCR_GameState.SetCurrentGameState(GameState.ended);
                    m_uiManager.SetLoseScreen();
                }
            }
        }
    }

    #region Debug
    public void ChangeRequiredPoints(int index, int val)
    {
        AddRequiredPoints(index, val);
    }
    public void ChangeRequiredMovement(int index)
    {
        AddMovement(index);
    }
    public void ChangePlayerPoints(int index, int val)
    {
        if (val == 0)
            RemovePlayerPoints(index);
        else
            AddPlayerPoints(index);
    }
    void AddRequiredPoints(int index, int val)
    {
        if (index == 0)
            _rRedPoints += val;
        else if (index == 1)
            _rBluePoints += val;
        else if (index == 2)
            _rGreenPoints += val;
        else if (index == 3)
            _rYellowPoints += val;

        m_uiManager.SetupRequiredPoints(_rRedPoints, _rBluePoints, _rGreenPoints, _rYellowPoints);
        CheckWinCondition();
    }
    void AddPlayerPoints(int index)
    {
        if (index == 0)
            SCR_GameStatus.AddRedPoints();
        else if (index == 1)
            SCR_GameStatus.AddBluePoints();
        else if (index == 2)
            SCR_GameStatus.AddGreenPoints();
        else if (index == 3)
            SCR_GameStatus.AddYellowPoints();

        m_uiManager.UpdateActualPoints();
        CheckWinCondition();
    }
    void RemovePlayerPoints(int index)
    {
        if (index == 0)
            SCR_GameStatus.RemoveRedPoints();
        else if (index == 1)
            SCR_GameStatus.RemoveBluePoints();
        else if (index == 2)
            SCR_GameStatus.RemoveGreenPoints();
        else if (index == 3)
            SCR_GameStatus.RemoveYellowPoints();

        m_uiManager.UpdateActualPoints();
        CheckWinCondition();
    }
    void AddMovement(int index)
    {
        if (index == 0)
            SCR_GameStatus.DecreaseMovement();
        else
            SCR_GameStatus.IncreaseMovement();

        m_uiManager.UpdateActualPoints();
        CheckWinCondition();
    }
    #endregion
}
