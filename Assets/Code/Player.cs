
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    private List<Piece> Pieces;
    private Tile currentTile;
    private Piece previousPiece;
    private Board board;


    void Start()
    {
        Piece[] pieces = transform.GetComponentsInChildren<Piece>();
        Pieces = pieces.ToList();
        board = FindObjectOfType<Board>();
    }

    public void GameUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        #region Tile
        if (currentTile != null)
        {
            currentTile.EnableHighlight(false);
            currentTile = null;
        }
        if (Physics.Raycast(ray, out hit, 50, LayerMask.GetMask("Tiles")))
        {
            Tile tile = hit.transform.GetComponentInParent<Tile>();

            if (tile != null)
            {
                tile.EnableHighlight(true);
                currentTile = tile;
            }
        }
        #endregion
        #region Piece
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 50, LayerMask.GetMask("Pieces")))
            {
                Piece piece = hit.transform.GetComponentInParent<Piece>();
                if (previousPiece != null)
                {
                    previousPiece.SelectedPieceDown();
                    foreach (var tile in board.Tiles)
                    {
                        tile.EnableMovementMarker(false);
                    }
                }
                if (piece != null)
                {
                    piece.SelectedPieceUp();
                    previousPiece = piece;
                    var movementTiles = board.GetMovementTiles(piece.Position, piece.Movement);
                    foreach (var tile in movementTiles)
                    {
                        tile.EnableMovementMarker(true);
                    }
                }
            }
            else 
            {
                if (previousPiece != null)
                {
                    previousPiece.SelectedPieceDown();
                    previousPiece = null;
                    foreach (var tile in board.Tiles)
                    {
                        tile.EnableMovementMarker(false);
                    }
                }
            }
        }
        #endregion
    }

    public void NewTurn()
	{
		Debug.Log($"New Turn {Name}");
	}
}
