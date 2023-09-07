
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


    void Start()
    {
        Piece[] pieces = transform.GetComponentsInChildren<Piece>();
        Pieces = pieces.ToList();
    }

    public void Update()
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
                Debug.Log(tile.Position);
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
                    previousPiece.SelectedPieceDown(previousPiece);
                }
                if (piece != null)
                {
                    piece.SelectedPieceUp(piece);
                    previousPiece = piece;
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
