using UnityEngine;


public class SCR_TileBehaviour : MonoBehaviour
{
    public enum TileType { Red, Blue, Green, Yellow };

    [Header("Type Variables")]
    [SerializeField] TileType m_myType;

    [Header("Visual Variables")]
    [SerializeField] Color[] m_colors;
    [SerializeField] MeshRenderer m_tileVisual;

    [Header("Settings")]
    [SerializeField] protected float _lerpSpeed = 0.6f;

    SCR_MatchFinder _matchFinder;
    SCR_BoardManager _board;

    bool _isMatched = false;

    int _myWidth;
    int _myHeight;

    Vector3 _tempPos;

    public void SetUpColor(int color)
    {
        m_tileVisual.material.color = m_colors[color];
    }

    public void SwitchSide()
    {
        if (GetMyTypeName() == "Red")
        {
            m_myType = TileType.Green;
            SetUpColor(GetMyTypeNum());
        }
        else if (GetMyTypeName() == "Blue")
        {
            m_myType = TileType.Yellow;
            SetUpColor(GetMyTypeNum());
        }
        else if (GetMyTypeName() == "Green")
        {
            m_myType = TileType.Red;
            SetUpColor(GetMyTypeNum());
        }
        else
        {
            m_myType = TileType.Blue;
            SetUpColor(GetMyTypeNum());
        }

        FindMatches();
    }

    public virtual void FindMatches()
    {
        _matchFinder.FindMatches();
    }

    public bool CheckMatches(int width, int height)
    {
        if (width > 1 && height > 1)
        {
            if (_board.GetTileByPos(width - 1, height).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width - 2, height).GetMyTypeName() == GetMyTypeName())
            {
                return true;
            }
            if (_board.GetTileByPos(width, height - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width, height - 2).GetMyTypeName() == GetMyTypeName())
            {
                return true;
            }
            return false;
        }
        else if (width <= 1 || height <= 1)
        {
            if (height > 1)
                if (_board.GetTileByPos(width, height - 1).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width, height - 2).GetMyTypeName() == GetMyTypeName())
                    return true;
            if (width > 1)
                if (_board.GetTileByPos(width - 1, height).GetMyTypeName() == GetMyTypeName() && _board.GetTileByPos(width - 2, height).GetMyTypeName() == GetMyTypeName())
                    return true;
        }
        return false;
    }

    public void DecreaseHeight(int val)
    {
        _myHeight -= val;

    }
    private void Update()
    {
        MoveTile();
    }

    protected void MoveTile()
    {
        if (Mathf.Abs(_myWidth - transform.position.x) > 0.1f)
        {
            _tempPos = new Vector3(_myWidth, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, _tempPos, _lerpSpeed);

            if (_board.GetTileByPos(_myWidth, _myHeight) != this)
                _board.SetTileByPos(_myWidth, _myHeight, this);
        }
        else
        {
            _tempPos = new Vector3(_myWidth, transform.position.y, transform.position.z);
            transform.position = _tempPos;
            _board.SetTileByPos(_myWidth, _myHeight, this);
        }
        if (Mathf.Abs(_myHeight - transform.position.y) > 0.1f)
        {
            _tempPos = new Vector3(transform.position.x, _myHeight, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, _tempPos, _lerpSpeed);

            if (_board.GetTileByPos(_myWidth, _myHeight) != this)
                _board.SetTileByPos(_myWidth, _myHeight, this);
        }
        else
        {
            _tempPos = new Vector3(transform.position.x, _myHeight, transform.position.z);
            transform.position = _tempPos;
            _board.SetTileByPos(_myWidth, _myHeight, this);
        }

    }

    #region Get/Set
    public void SetType(int value)
    {
        m_myType = GetTypeFromInt(value);
    }
    public string GetMyTypeName()
    {
        return System.Enum.GetName(typeof(TileType), m_myType);
    }
    public int GetMyTypeNum()
    {
        return (int)m_myType;
    }
    TileType GetTypeFromInt(int value)
    {
        return (TileType)value;
    }
    public void SetMyWidth(int val)
    {
        _myWidth = val;
    }
    public int GetMyWidth()
    {
        return _myWidth;
    }
    public void SetMyHeight(int val)
    {
        _myHeight = val;
    }
    public int GetMyHeight()
    {
        return _myHeight;
    }

    public void SetBoard(SCR_BoardManager board)
    {
        _board = board;
    }
    public void SetMatchFinder(SCR_MatchFinder finder)
    {
        _matchFinder = finder;
    }
    public void SetIsMatched(bool val)
    {
        _isMatched = val;
    }
    public bool GetIsMatched()
    {
        return _isMatched;
    }
    #endregion
}
