using UnityEngine;
using TMPro;

public class SCR_DebugManager : MonoBehaviour
{
    [Header("Debug Screen")]
    [SerializeField] GameObject m_debugScreen;

    [Header("FPS Controller")]
    [SerializeField] TMP_Text m_fps;
    [SerializeField] float m_pollingTime = 1f;

    float _time;
    int _frameCount;
    int _frameRate;

    [Header("Game Status")]
    [SerializeField] SCR_ConditionManager m_conditionManager;

    void Start()
    {
        
    }
     
    void Update()
    {
        FPSCount();
    }
    #region UI
    public void DisplayScreen()
    {
        if (SCR_GameState.IsGameStateMove() || SCR_GameState.IsGameStateDebug())
        {
            m_debugScreen.SetActive(!m_debugScreen.activeSelf);

            if (m_debugScreen.activeSelf == true)
                SCR_GameState.SetCurrentGameState(GameState.debug);
            else
                SCR_GameState.SetCurrentGameState(GameState.move);
        }
    }
    #endregion

    #region FPS
    void FPSCount()
    {
        _time += Time.deltaTime;

        _frameCount++;

        if(_time >= m_pollingTime)
        {
            _frameRate = Mathf.RoundToInt(_frameCount / _time);
            m_fps.text = _frameRate.ToString() + " FPS";


            _time -= m_pollingTime;
            _frameCount = 0;
            _frameRate = 0;
        }
    }
    #endregion

    #region Score
    public void IncreaseGameMovement()
    {
        m_conditionManager.ChangeRequiredMovement(1);
    }
    public void DecreaseGameMovement()
    {
        m_conditionManager.ChangeRequiredMovement(0);
    }
    public void IncreaseGameScore(int color = 0)
    {
        m_conditionManager.ChangeRequiredPoints(color, 1);
    }
    public void DecreaseGameScore(int color = 0)
    {
        m_conditionManager.ChangeRequiredPoints(color, -1);
    }
    public void IncreasePlayerScore(int color = 0)
    {
        m_conditionManager.ChangePlayerPoints(color, 1);
    }
    public void DecreasePlayerScore(int color = 0)
    {
        m_conditionManager.ChangePlayerPoints(color, 0);
    }
    #endregion
}
