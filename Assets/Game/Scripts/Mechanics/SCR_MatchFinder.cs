using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_MatchFinder : MonoBehaviour
{
    [SerializeField] SCR_BoardManager m_board;

    [SerializeField] List<SCR_TileBehaviour> m_currentMatches = new List<SCR_TileBehaviour>();

    SCR_TileBehaviour _currentTile;

    public void FindMatches()
    {
        FindAllMatches();
    }

    void FindAllMatches()
    {
        for (int i = 0; i < m_board.GetBoardWidth(); i++)
        {
            for (int j = 0; j < m_board.GetBoardHeight(); j++)
            {
                SCR_TileBehaviour tile = m_board.GetTileByPos(i, j);

                if (tile != null)
                {
                    if (i > 0 && i < m_board.GetBoardWidth() - 1)
                    {
                        SCR_TileBehaviour leftTile = m_board.GetTileByPos(i - 1, j);
                        SCR_TileBehaviour rightTile = m_board.GetTileByPos(i + 1, j);

                        if (leftTile != null && rightTile != null)
                            if (leftTile.GetMyTypeName() == tile.GetMyTypeName() && rightTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(leftTile);
                                AddToList(rightTile);

                                leftTile.SetIsMatched(true);
                                rightTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                    }
                    if (j > 0 && j < m_board.GetBoardHeight() - 1)
                    {
                        SCR_TileBehaviour upTile = m_board.GetTileByPos(i, j + 1);
                        SCR_TileBehaviour downTile = m_board.GetTileByPos(i, j - 1);

                        if (upTile != null && downTile != null)
                            if (upTile.GetMyTypeName() == tile.GetMyTypeName() && downTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(upTile);
                                AddToList(downTile);

                                upTile.SetIsMatched(true);
                                downTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                    }

                    if (j > 0 && i > 0 && i < m_board.GetBoardWidth() - 1)
                    {
                        SCR_TileBehaviour upTile = m_board.GetTileByPos(i, j - 1);
                        SCR_TileBehaviour leftTile = m_board.GetTileByPos(i - 1, j);
                        SCR_TileBehaviour rightTile = m_board.GetTileByPos(i + 1, j);

                        if (leftTile != null && rightTile != null && upTile != null)
                            if (leftTile.GetMyTypeName() == tile.GetMyTypeName() && rightTile.GetMyTypeName() == tile.GetMyTypeName() && upTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(upTile);
                                AddToList(leftTile);
                                AddToList(rightTile);

                                upTile.SetIsMatched(true);
                                leftTile.SetIsMatched(true);
                                rightTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                    }
                    if (j < m_board.GetBoardHeight() - 1 && i > 0 && i < m_board.GetBoardWidth() - 1)
                    {
                        SCR_TileBehaviour downTile = m_board.GetTileByPos(i, j + 1);
                        SCR_TileBehaviour leftTile = m_board.GetTileByPos(i - 1, j);
                        SCR_TileBehaviour rightTile = m_board.GetTileByPos(i + 1, j);

                        if (downTile != null && leftTile != null && rightTile != null)
                        {
                            if (downTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                leftTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                rightTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(downTile);
                                AddToList(leftTile);
                                AddToList(rightTile);

                                downTile.SetIsMatched(true);
                                leftTile.SetIsMatched(true);
                                rightTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                        }
                    }
                    if (j > 0 && j < m_board.GetBoardHeight() - 1 && i > 0)
                    {
                        SCR_TileBehaviour upTile = m_board.GetTileByPos(i, j - 1);
                        SCR_TileBehaviour downTile = m_board.GetTileByPos(i, j + 1);
                        SCR_TileBehaviour leftTile = m_board.GetTileByPos(i - 1, j);

                        if (upTile != null && downTile != null && leftTile != null)
                        {
                            if (upTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                downTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                leftTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(upTile);
                                AddToList(downTile);
                                AddToList(leftTile);

                                upTile.SetIsMatched(true);
                                downTile.SetIsMatched(true);
                                leftTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                        }
                    }
                    if (j > 0 && j < m_board.GetBoardHeight() - 1 && i < m_board.GetBoardWidth() - 1)
                    {
                        SCR_TileBehaviour upTile = m_board.GetTileByPos(i, j - 1);
                        SCR_TileBehaviour downTile = m_board.GetTileByPos(i, j + 1);
                        SCR_TileBehaviour rightTile = m_board.GetTileByPos(i + 1, j);

                        if (upTile != null && downTile != null && rightTile != null)
                        {
                            if (upTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                downTile.GetMyTypeName() == tile.GetMyTypeName() &&
                                rightTile.GetMyTypeName() == tile.GetMyTypeName())
                            {
                                AddToList(tile);
                                AddToList(upTile);
                                AddToList(downTile);
                                AddToList(rightTile);

                                upTile.SetIsMatched(true);
                                downTile.SetIsMatched(true);
                                rightTile.SetIsMatched(true);
                                tile.SetIsMatched(true);
                            }
                        }
                    }

                }
            }
        }

        m_board.DestroyMatches();
    }

    SCR_TileBehaviour.TileType GetHigher(int a, int b, int c, int d)
    {
        int[] val = { a, b, c, d };
        int high = a;
        int highpos = 0;

        for (int i = 0; i < val.Length; i++)
        {
            if (val[i] > high)
            {
                high = val[i];
                highpos = i;
            }
        }
        return (SCR_TileBehaviour.TileType)highpos;
    }
    void AddToList(SCR_TileBehaviour obj)
    {
        if (!m_currentMatches.Contains(obj))
            m_currentMatches.Add(obj);
    }
    public void RemoveFromList(SCR_TileBehaviour obj)
    {
        m_currentMatches.Remove(obj);
    }

    List<SCR_TileBehaviour> GetColumnTiles(int val)
    {
        List<SCR_TileBehaviour> tiles = new List<SCR_TileBehaviour>();

        for (int i = 0; i < m_board.GetBoardHeight(); i++)
        {
            if (m_board.GetTileByPos(val, i) != null)
            {
                tiles.Add(m_board.GetTileByPos(val, i));
                m_board.GetTileByPos(val, i).SetIsMatched(true);
            }
        }
        return tiles;
    }

    List<SCR_TileBehaviour> GetRowTiles(int val)
    {
        List<SCR_TileBehaviour> tiles = new List<SCR_TileBehaviour>();

        for (int i = 0; i < m_board.GetBoardWidth(); i++)
        {
            if (m_board.GetTileByPos(i, val) != null)
            {
                tiles.Add(m_board.GetTileByPos(i, val));
                m_board.GetTileByPos(i, val).SetIsMatched(true);
            }
        }
        return tiles;
    }
    public void SetCurrentTile(SCR_TileBehaviour ct)
    {
        _currentTile = ct;
    }

    public SCR_TileBehaviour GetCurrentTile()
    {
        return _currentTile;
    }

    public List<SCR_TileBehaviour> GetCurrentMatches()
    {
        return m_currentMatches;
    }
}

