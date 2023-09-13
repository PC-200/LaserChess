using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MovementDirection
{
    public Vector3 Direction;
    public int Distance; 
}

public class Piece : MonoBehaviour
{
    const float MovementTime = 0.3f;
    public List<MovementDirection> Movement;
    public List<MovementDirection> Attack;
    public int MaxHealth;
    public int AttackDamage;
    public bool AoEDamage;
    public GameObject VisualObj;
    public bool IsMoved;
    public bool IsAttacked;
    public AnimationCurve MovementCurve;
    private bool moving;
    private Vector3 startPos, targetPos;
    private float movementStartTime;
    public Player Player;
    private int currentHealth;

    
    public Vector2Int Position => transform.position.ToV2I();

    public void Start()
    {
        currentHealth = MaxHealth;
    }

    public void SelectedPieceUp()
    {
        Vector3 newPosition = new Vector3(0, 0.5f, 0);
        VisualObj.transform.localPosition = newPosition;
    }

    public void SelectedPieceDown()
    {
        VisualObj.transform.localPosition = Vector3.zero;
    }

    public void MoveTo(Vector2Int pos)
    {
        IsMoved = true;
        moving= true;
        startPos = transform.position;
        targetPos = new Vector3(pos.x, 0, pos.y);
        movementStartTime = Time.time;
    }
    public void AttackPiece(Piece piece)
    {
        Debug.Log($"Current health is: { piece.currentHealth}, {AttackDamage}");
        piece.currentHealth -= AttackDamage;
        if (piece.currentHealth <= 0)
        {
            Destroy(piece.gameObject);
        }
        IsAttacked = true;
        IsMoved = true;
    }

    public void Update()
    {
        if (moving)
        {
            float t = (Time.time - movementStartTime) / MovementTime;
            t = MovementCurve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            if (t > 1)
            {
                moving = false;
            }
        }
    }
}

