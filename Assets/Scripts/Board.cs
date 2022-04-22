using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerType { NONE, BLUE, RED}

public struct GridPos { public int row, col; }


public class Board
{
    playerType[][] playerboard;
    GridPos currentPost;

    public Board()
    {
        playerboard = new playerType[6][];
        for (int i = 0; i < playerboard.Length; i++)
        {
            playerboard[i] = new playerType[7];
            for (int j = 0; j < playerboard[i].Length; j++)
            {
                playerboard[i][j] = playerType.NONE;
            }
        }
    }


    public void updateBoard(int col, bool isPlayer)
    {
        int updatePos = 6;
        for (int i = 5; i >= 0; i--)
        {
            if (playerboard[i][col] == playerType.NONE)
            {
                updatePos--;

            }
            else
            {
                break;
            }
        }

        playerboard[updatePos][col] = isPlayer ? playerType.BLUE : playerType.RED;
        currentPost = new GridPos { row = updatePos, col = col };

    }

    public bool result(bool isplayer)
    {
        playerType current = isplayer ? playerType.BLUE : playerType.RED;

        return Horizontal(current) || Vertical(current) || Diagonal(current) || RDiagonal(current);
    }

    bool Horizontal(playerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = 0, col = -1 });
        List<GridPos> searchlist = GetplayerList(start, new GridPos { row = 0, col = 1 });
        return searchresult(searchlist, current);

    }

    bool Vertical(playerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = 0 });
        List<GridPos> searchlist = GetplayerList(start, new GridPos { row = 1, col = 0 });
        return searchresult(searchlist, current);

    }

    bool Diagonal(playerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = -1 });
        List<GridPos> searchlist = GetplayerList(start, new GridPos { row = 1, col = 1 });
        return searchresult(searchlist, current);

    }

    bool RDiagonal(playerType current)
    {
        GridPos start = GetEndPoint(new GridPos { row = -1, col = 1 });
        List<GridPos> searchlist = GetplayerList(start, new GridPos { row = 1, col = -1 });
        return searchresult(searchlist, current);

    }

    GridPos GetEndPoint(GridPos diff)
    {
        GridPos result = new GridPos { row = currentPost.row, col = currentPost.col };
        while (result.row + diff.row < 6 &&
            result.col + diff.col < 6 &&
            result.row + diff.row >= 0 &&
            result.col + diff.col >= 0)
        {
            result.row += diff.row;
            result.col += diff.col;
        }

        return result;
    }

    List<GridPos> GetplayerList(GridPos start, GridPos diff)
    {
        List<GridPos> resList;
        resList = new List<GridPos> { start };
        GridPos result = new GridPos { row = start.row, col = start.col };
        while (result.row + diff.row < 6 &&
         result.col + diff.col < 6 &&
         result.row + diff.row >= 0 &&
         result.col + diff.col >= 0)
        {
            result.row += diff.row;
            result.col += diff.col;
            resList.Add(result);
        }


        return resList;
    }

    //bool searchresult(List<GridPos> searchlist, playerType current)
    //{
    //    int count = 0;
        
    //}

    bool searchresult(List<GridPos> searchlist, playerType current)
    {
        int count = 0;
        for (int i = 0; i < searchlist.Count; i++)
        {
            playerType compare = playerboard[searchlist[i].row][searchlist[i].col];
            if (compare == current)
            {
                count++;

                if (count == 4)
                    break;
            }

            else
            {
                count = 0;
            }
            
            
        }
        return count >= 4;


    }
}

