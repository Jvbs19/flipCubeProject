using UnityEngine;
using TMPro;

public class SCR_UIManager : MonoBehaviour
{
    [Header("TMP Texts")]
    [SerializeField] TMP_Text[] m_myScore;
    [SerializeField] TMP_Text[] m_requiredScore;

    [SerializeField] TMP_Text m_movesLeft;

    [Header("Game Screen")]
    [SerializeField] GameObject[] m_endScreen;
    public void SetupRequiredPoints(int r, int b, int g, int y)
    {
        m_requiredScore[0].text = "" + r;
        m_requiredScore[1].text = "" + b;
        m_requiredScore[2].text = "" + g;
        m_requiredScore[3].text = "" + y;
    }
    public void UpdateActualPoints()
    {

        m_myScore[0].text = "" + SCR_GameStatus.GetRedPoints();
        m_myScore[1].text = "" + SCR_GameStatus.GetBluePoints();
        m_myScore[2].text = "" + SCR_GameStatus.GetGreenPoints();
        m_myScore[3].text = "" + SCR_GameStatus.GetYellowPoints();
        m_movesLeft.text = "" + SCR_GameStatus.CheckMovimentsLeft();
    }

    public void SetVictoryScreen()
    {
        m_endScreen[0].SetActive(false);
        m_endScreen[1].SetActive(true);
    }
    
    public void SetLoseScreen()
    {
        m_endScreen[0].SetActive(true);
        m_endScreen[1].SetActive(false);
    }
}
