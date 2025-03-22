using UnityEngine;


[CreateAssetMenu(fileName = "Condition", menuName = "Condition/Game Condition")]
public class SCR_ConditionInfo : ScriptableObject
{
    [Header("Requirements")]
    [SerializeField] int redPoints = 0;
    [SerializeField] int bluePoints = 0;
    [SerializeField] int greenPoints = 0;
    [SerializeField] int yellowPoints = 0;
    [SerializeField] int maxMoviments = 0;
    [SerializeField] float maxTimer = 0;

    public int GetRequiredRedPoints()
    {
        return redPoints;
    }
    public int GetRequiredBluePoints()
    {
        return bluePoints;
    }
    public int GetRequiredGreenPoints()
    {
        return greenPoints;
    }
    public int GetRequiredYellowPoints()
    {
        return yellowPoints;
    }
    public int GetMaxMoviments()
    {
        return maxMoviments;
    }
    public float GetMaxTimer()
    {
        return maxTimer;
    }
}
