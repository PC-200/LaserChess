
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandUnitBehaviour : MonoBehaviour, AiBehaviour
{
    public Tile GetMove(Piece piece)
    {
        Board board = FindObjectOfType<Board>();
        List<Tile> tiles = board.GetMovementTiles(piece.Position, piece.Movement);
        if (tiles.Count == 0)
        {
            return null;
        }
        Vector2Int originalPos = piece.Position;
        tiles.Add(board.GetTile(originalPos));
        List<Piece> enemyPieces = board.Pieces.Where(x => x.Player != piece.Player).ToList();
        int minDamage = 0;
        Tile minDamageTile = null;
        foreach (var tile in tiles) 
        {
            int currentTileDamage = 0;
            piece.Teleport(tile.Position);
            foreach (var enemyPiece in enemyPieces)
            {
                List<Tile> enemyMovementTiles = board.GetMovementTiles(enemyPiece.Position, enemyPiece.Movement);
                enemyMovementTiles.Add(board.GetTile(enemyPiece.Position));
                Vector2Int enemyOriginalPos = enemyPiece.Position;
                foreach (var enemyMovementTile in enemyMovementTiles)
                {
                    enemyPiece.Teleport(enemyMovementTile.Position);
                    List<Piece> attackPieces = board.GetAttackPieces(enemyPiece.Position, enemyPiece.Attack);
                    if (attackPieces.Contains(piece))
                    {
                        currentTileDamage += enemyPiece.AttackDamage;
                    }
                }
                enemyPiece.Teleport(enemyOriginalPos);
            }
            if (currentTileDamage <= minDamage || minDamageTile == null )
            {
                minDamage = currentTileDamage;
                minDamageTile = tile;
            }
        }
        piece.Teleport(originalPos);
        return minDamageTile;
    }

    public List<Piece> GetPiecesToAttack(Piece piece)
    {
        return null;
    }
}

