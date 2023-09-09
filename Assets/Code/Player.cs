
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    private List<Piece> Pieces;
    private Tile currentTile;
    private Piece currentPiece;
    private Board board;


    void Awake()
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
                if (currentPiece != null)
                {
                    currentPiece.SelectedPieceDown();
                    foreach (var tile in board.Tiles)
                    {
                        tile.EnableMovementMarker(false);
                    }
                }
                if (piece != null && !piece.IsMoved && Pieces.Contains(piece))
                {
                    piece.SelectedPieceUp();
                    currentPiece = piece;
                    var movementTiles = board.GetMovementTiles(piece.Position, piece.Movement);
                    foreach (var tile in movementTiles)
                    {
                        tile.EnableMovementMarker(true);
                    }
                }
            }
            else 
            {
                if (currentPiece != null)
                {
                    if(currentTile != null && currentTile.IsMarkedForMovement)
                    {
                        currentPiece.MoveTo(currentTile.Position);
                        
                    }
                    currentPiece.SelectedPieceDown();
                    currentPiece = null;
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
        foreach (var piece in Pieces)
        { 
            piece.IsMoved = false;   
        }
	}
}
