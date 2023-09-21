using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DreadnoughtBehaviour : MonoBehaviour, AiBehaviour
{
    public Tile GetMove(Piece piece)
    {
        Board board = FindObjectOfType<Board>();
        List<Tile> tiles = board.GetMovementTiles(piece.Position, piece.Movement);
        if (tiles.Count == 0)
        { 
            return null;
        }
        List<Piece> enemyPieces = board.Pieces.Where(x => x.Player != piece.Player).ToList();
        float minDistance = float.MaxValue;
        Tile closestTile = null;
        foreach (var tile in tiles)
        {
            foreach (var enemyPiece in enemyPieces)
            {
                float distance = (tile.Position - enemyPiece.Position).sqrMagnitude;
                if (distance < minDistance || closestTile == null)
                {
                    minDistance = distance;
                    closestTile = tile;
                }
            }
        }
        return closestTile;
    }

    public List<Piece> GetPiecesToAttack(Piece piece)
    {
        Board board = FindObjectOfType<Board>();
        List<Piece> pieces = board.GetAttackPieces(piece.Position, piece.Attack);
        if (pieces.Count == 0) { return null; }
        return pieces.Where(x => x.Player != piece.Player).ToList();
    }
}
