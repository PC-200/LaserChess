
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    private List<Piece> Pieces;

    void Start()
    {
        Piece[] pieces = transform.GetComponentsInChildren<Piece>();
        Pieces = pieces.ToList();
    }

    public void Update()
	{ 
		
	}

	public void NewTurn()
	{
		Debug.Log($"New Turn {Name}");
	}
}
