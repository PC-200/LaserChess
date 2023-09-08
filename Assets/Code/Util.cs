
using UnityEngine;

public static class Util
{
    public static Vector2Int ToV2I(this Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.z));
    }
}

