
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
    public GameObject VisualObj;
    public Vector2Int Position => transform.position.ToV2I();

    public void SelectedPieceUp()
    {
        Vector3 newPosition = new Vector3(0, 0.5f, 0);
        VisualObj.transform.localPosition = newPosition;
    }

    public void SelectedPieceDown()
    {
        VisualObj.transform.localPosition = Vector3.zero;
    }
}

