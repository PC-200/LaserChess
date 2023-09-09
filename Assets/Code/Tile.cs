using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int Position => transform.position.ToV2I();
    public GameObject HighlightObject;
    public GameObject MovementMarkerObj;
    public bool IsMarkedForMovement => MovementMarkerObj.activeSelf;

    public void EnableHighlight(bool enable)
    {
        HighlightObject.SetActive(enable);
    }

    public void EnableMovementMarker(bool enable)
    {
        MovementMarkerObj.SetActive(enable);
    }

}
