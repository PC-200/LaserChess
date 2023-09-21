
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    private Tile currentTile;
    private Piece currentPiece;
    private Board board;
    public bool IsAiPlayer;
    public List<Piece> AiPieces = new List<Piece>();    

    void Awake()
    {
        Piece[] pieces = transform.GetComponentsInChildren<Piece>();
        foreach (Piece piece in pieces)
        {
            piece.Player = this;
        }
        board = FindObjectOfType<Board>();
    }

    public void AiPlayerUpdate()
    {
        if (AiPieces.Count == 0)
        {
            FindObjectOfType<Game>().NextPlayer();
            return;
        }
        Piece piece = AiPieces[0];
        if (piece.IsBusy())
        {
            return;
        }
        AiBehaviour behaviour = piece.GetComponent<AiBehaviour>();
        if (behaviour == null) 
        { 
            piece.IsMoved= true;
            piece.IsAttacked = true;
            return;
        }
        if (!piece.IsMoved)
        {
            Tile tile = behaviour.GetMove(piece);
            if (tile != null)
            {
                piece.MoveTo(tile.Position);
            }
            else
            {
                piece.IsMoved = true;
            }
            return;
        }
        if (!piece.IsAttacked)
        {
            List<Piece> attackPieces = behaviour.GetPiecesToAttack(piece);
           
            if (attackPieces != null) 
            {
                attackPieces = attackPieces.Where(x => x.Player != this).ToList();
                foreach (var p in attackPieces)
                {
                    piece.ShootAt(p);
                }

            }
            else
            {
                piece.IsAttacked = true;
            }
            AiPieces.RemoveAt(0);
            return;
        }

    }

    public void GameUpdate()
    {
        if (IsAiPlayer) 
        {
            AiPlayerUpdate();
        }
        else
        {
            HumanPlayerUpdate();
        }
    }

    public void HumanPlayerUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool currentTileMarkedForAttack = false;
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
                currentTileMarkedForAttack = tile.IsMarkedForAttack;
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
                        tile.EnableAttackMarker(false);
                    }
                }
                if (piece != null)
                { 
                    if (piece.Player == this)
                    {
                        piece.SelectedPieceUp();
                        currentPiece = piece;
                        if (!piece.IsMoved)
                        {
                            var movementTiles = board.GetMovementTiles(piece.Position, piece.Movement);
                            foreach (var tile in movementTiles)
                            {
                                tile.EnableMovementMarker(true);
                            }
                        }
                        if (!piece.IsAttacked) 
                        {
                            var attackPieces = board.GetAttackPieces(piece.Position, piece.Attack).Where(p => p.Player != this);
                            foreach (var ap in attackPieces)
                            {
                                board.GetTile(ap.Position).EnableAttackMarker(true);
                            }
                        }
                    }
                    else
                    {
                        if (currentTileMarkedForAttack)
                        {
                            if (currentPiece.AoEDamage)
                            {
                                var attackPieces = board.GetAttackPieces(currentPiece.Position, currentPiece.Attack).Where(p => p.Player != this);
                                foreach (var ap in attackPieces)
                                {
                                    currentPiece.ShootAt(board.GetPiece(ap.Position));
                                }
                            }
                            else
                            {
                                currentPiece.ShootAt(board.GetPiece(currentTile.Position));
                            }

                        }

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
                        tile.EnableAttackMarker(false);
                    }
                }
            }
        }
        #endregion
    }

    public void NewTurn()
	{
        foreach (var piece in board.Pieces.Where(p => p.Player == this))
        { 
            piece.IsMoved = false;
            piece.IsAttacked = false;
        }
        if (IsAiPlayer)
        {
            AiPieces.Clear();
            AiPieces.AddRange(board.Pieces.Where(p=> p.Player == this));
            AiPieces.Sort((a, b) => a.AiMovePriority - b.AiMovePriority);
        }
	}

    public void OnActionFinished()
    { 
        FindObjectOfType<Game>().CheckForGameOver();
    }
}
