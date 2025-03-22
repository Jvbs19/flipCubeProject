using System.Collections;
using UnityEngine;

public class SCR_BoardManager : MonoBehaviour
{
    int _width;
    int _height;
    [Header("Grid Objects")]
    [SerializeField] SCR_GridInfo m_gridInfo;
    [SerializeField] GameObject m_tilePrefab;
    [SerializeField] GameObject m_board;

    [Header("Settings")]
    [SerializeField] bool m_isScripted;
    [SerializeField] float m_decreaseHeightSpeed = 0.6f;
    [SerializeField] float m_fillBoardSpeed = 0.8f;
    [SerializeField] float m_releaseMoveCooldown = 0.4f;
    [SerializeField] float m_offset = 0.6f;

    [Header("Game Reference")]
    [SerializeField] SCR_MatchFinder m_matchFinder;
    [SerializeField] SCR_ConditionManager m_conditionManager;
    [SerializeField] SCR_UIManager m_uiManager;

    SCR_TileBehaviour[,] m_allTiles;

    Vector2 _pos;

    void SetUpInfo()
    {
        _width = m_gridInfo.GetWidth();
        _height = m_gridInfo.GetHeight();

        UpdateScreenSize(_width, _height);
    }

    void Start()
    {
        SetUpInfo();

        if (!m_isScripted)
        {
            SetUpInfo();
            SetUpRandomGrid();
        }
        else
        {
            SetUpScriptedGrid();
        }
    }
    void SetUpRandomGrid()
    {
        m_allTiles = new SCR_TileBehaviour[_width, _height];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                SpawnTile(i, j);
            }
        }
    }
    void SetUpScriptedGrid()
    {
        m_allTiles = new SCR_TileBehaviour[_width, _height];

        SCR_TileBehaviour currentTile;
        int x = 0;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                currentTile = m_board.transform.GetChild(x).GetComponent<SCR_TileBehaviour>();
                AddExistingTileToGrid(currentTile, i, j, false, false);
                x++;
            }
        }
    }
    void AddExistingTileToGrid(SCR_TileBehaviour tile, int i, int j, bool useOffset = false, bool random = true)
    {
        if (useOffset)
            _pos = new Vector2(i, j + m_offset);
        else
            _pos = new Vector2(i, j);

        tile.transform.parent = m_board.transform;
        tile.transform.name = "(" + i + "," + j + ")";

        TileSetup(tile, i, j, random);
    }
    void SpawnTile(int i, int j, bool useOffset = false, bool random = true)
    {
        if (useOffset)
            _pos = new Vector2(i, j + m_offset);
        else
            _pos = new Vector2(i, j);

        GameObject tile = Instantiate(m_tilePrefab, _pos, Quaternion.identity);

        tile.transform.parent = m_board.transform;
        tile.transform.name = "(" + i + "," + j + ")";

        SCR_TileBehaviour behaviour = tile.GetComponent<SCR_TileBehaviour>();

        TileSetup(behaviour, i, j, random);

    }
    void TileSetup(SCR_TileBehaviour behaviour, int i, int j, bool random = true)
    {
        m_allTiles[i, j] = behaviour;

        behaviour.SetBoard(this);
        behaviour.SetMatchFinder(m_matchFinder);
        behaviour.SetMyWidth(i);
        behaviour.SetMyHeight(j);
        behaviour.SetIsMatched(false);

        if (random)
        {
            int num = Random.Range(0, 4);
            behaviour.SetType(num);

            while (behaviour.CheckMatches(i, j))
            {
                num = Random.Range(0, 4);
                behaviour.SetType(num);
            }

            behaviour.SetUpColor(num);
        }
        else
        {
            behaviour.SetUpColor(behaviour.GetMyTypeNum());
        }
    }
    public void DestroyMatchesAt(int i, int j)
    {
        if (m_allTiles[i, j].GetIsMatched())
        {
            m_matchFinder.RemoveFromList(m_allTiles[i, j]);

            AddPointToTheRightPlace(m_allTiles[i, j]);

            Destroy(m_allTiles[i, j].gameObject);
            m_allTiles[i, j] = null;
        }
    }

    public void DestroyMatches()
    {
        StartCoroutine(MatchDestroyer());
    }
    IEnumerator MatchDestroyer()
    {
        yield return new WaitForSeconds(m_fillBoardSpeed);
        if (MatchesOnBoard())
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (m_allTiles[i, j] != null)
                        DestroyMatchesAt(i, j);
                }
            }
            StartCoroutine(DecreaseRow());
        }
        else
        {
            if (!SCR_GameState.IsGameStateEnded())
            {
                if (!MatchesOnBoard())
                    m_uiManager.UpdateActualPoints();
                m_conditionManager.CheckWinCondition();
            }
            StartCoroutine(MoveRelease());
        }

    }
    IEnumerator DecreaseRow()
    {
        int nullCount = 0;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] == null)
                {
                    nullCount++;
                }
                else
                {
                    m_allTiles[i, j].DecreaseHeight(nullCount);
                    m_allTiles[i, j] = null;
                }

            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(m_decreaseHeightSpeed);

        StartCoroutine(FillBoard());
    }
    void RefillBoard()
    {

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] == null)
                    SpawnTile(i, j, true);
            }
        }
    }
    public bool MatchesOnBoard()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (m_allTiles[i, j] != null)
                    if (m_allTiles[i, j].GetIsMatched())
                        return true;
            }
        }
        return false;
    }

    IEnumerator FillBoard()
    {
        RefillBoard();
        yield return new WaitForSeconds(m_fillBoardSpeed);
        if (!MatchesOnBoard())
            m_uiManager.UpdateActualPoints();
        m_matchFinder.FindMatches();

    }

    IEnumerator MoveRelease()
    {
        yield return new WaitForSeconds(m_releaseMoveCooldown);
        SCR_GameState.SetCurrentGameState(GameState.move);
    }

    public void AddPointToTheRightPlace(SCR_TileBehaviour tile)
    {
        if (tile.GetMyTypeNum() == 0)
            SCR_GameStatus.AddRedPoints();
        else if (tile.GetMyTypeNum() == 1)
            SCR_GameStatus.AddBluePoints();
        else if (tile.GetMyTypeNum() == 2)
            SCR_GameStatus.AddGreenPoints();
        else
            SCR_GameStatus.AddYellowPoints();
    }

    void UpdateScreenSize(int i, int j)
    {
        if(i <= 4)
        {
            Camera.main.orthographicSize = 5;
        }
        else if(i == 5)
        {
            Camera.main.orthographicSize = 6;
        }
        else
        {
            Camera.main.orthographicSize = 7;
        }
    }
    #region Get/Set
    public SCR_TileBehaviour GetTileByPos(int i, int j)
    {
        return m_allTiles[i, j];
    }
    public void SetTileByPos(int i, int j, SCR_TileBehaviour tile)
    {
        m_allTiles[i, j] = tile;
    }

    public int GetBoardWidth()
    {
        return _width;
    }
    public int GetBoardHeight()
    {
        return _height;
    }
    #endregion
}
