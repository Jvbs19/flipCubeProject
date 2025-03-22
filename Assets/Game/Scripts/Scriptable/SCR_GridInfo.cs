using UnityEngine;

[CreateAssetMenu(fileName = "Board", menuName = "Board/New Grid")]
public class SCR_GridInfo : ScriptableObject
{
   [Header("Grid Config")]
   [SerializeField] int width;
   [SerializeField] int height;

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
}
