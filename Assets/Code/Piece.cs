
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

}

