using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [NonSerialized]
    public List<Tile> Tiles;
    [NonSerialized]
    public List<Piece> Pieces;
    

    void Start()
    {
        var tiles = GetComponentsInChildren<Tile>();
        Tiles = tiles.ToList();
        UpdatePiecesList();
    }

    public List<Tile> GetMovementTiles(Vector2Int startPos, List<MovementDirection> directions)
    {
        List<Tile> result = new List<Tile>();
        
        foreach (var md in directions)
        {
            for (int i = 1; i <= md.Distance; i++)
            {
                Vector2Int newPos = startPos + md.Direction.ToV2I() * i;
                var tile = GetTile(newPos);
                var piece = GetPiece(newPos);
                if (piece != null)
                {
                    break;
                }
                if (tile != null) 
                {
                    result.Add(tile);
                }
            }
        }
        return result;
    }

    public Tile GetTile(Vector2Int pos)
    {
        foreach (var tile in Tiles)
        {
            if (tile.Position == pos)
            {
                return tile;
            }
        }
        return null;
    }

    public Piece GetPiece(Vector2Int pos)
    {
        foreach (var piece in Pieces)
        {
            if (piece.Position == pos)
            {
                return piece;
            }
        }
        return null;
    }
    public List<Piece> GetAttackPieces(Vector2Int startPos, List<MovementDirection> directions)
    {
        List<Piece> result = new List<Piece>();

        foreach (var md in directions)
        {
            for (int i = 1; i <= md.Distance; i++)
            {
                Vector2Int newPos = startPos + md.Direction.ToV2I() * i;
                var piece = GetPiece(newPos);
                if (piece != null)
                {
                    result.Add(piece);
                    break;
                }
            }
        }
        return result;
    }

    public void UpdatePiecesList()
    {
        var game = FindObjectOfType<Game>();
        var pieces = game.GetComponentsInChildren<Piece>();
        Pieces = pieces.ToList();
    }
}
