using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DroneBehaviour : MonoBehaviour, AiBehaviour
{
    public Tile GetMove(Piece piece)
    {
        Board board = FindObjectOfType<Board>();
        List<Tile> tiles = board.GetMovementTiles(piece.Position, piece.Movement);
        if (tiles.Count == 0)
        {
            return null;
        }
        return tiles[0];
    }

    public List<Piece> GetPiecesToAttack(Piece piece)
    {
        Board board = FindObjectOfType<Board>();
        List<Piece> pieces = board.GetAttackPieces(piece.Position, piece.Attack);
        if (pieces.Count == 0) { return null; }
        return new List<Piece>() { pieces[0] };
        
    }
}

