
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


[Serializable]
public struct MovementDirection
{
    public Vector3 Direction;
    public int Distance; 
}
public class Piece : MonoBehaviour
{
    public List<MovementDirection> Movement;
    public List<MovementDirection> Attack;
    public int MaxHealth;
    public int AttackDamage;
    public bool AoEDamage;

    public Vector3 Position => new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));

    public void SelectedPieceUp(Piece pieceUp)
    {
        Vector3 newPosition = new Vector3(Position.x, Position.y + (float)0.5, Position.z); 
        pieceUp.transform.position = newPosition;
    }

    public void SelectedPieceDown(Piece pieceDown)
    {
        Vector3 newPosition = new Vector3(Position.x, Position.y, Position.z);
        pieceDown.transform.position = newPosition;
    }
}

