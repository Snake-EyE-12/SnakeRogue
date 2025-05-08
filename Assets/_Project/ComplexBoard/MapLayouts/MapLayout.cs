using System.Collections.Generic;
using UnityEngine;

public abstract class MapLayout : ScriptableObject
{
    public abstract List<Vector2Int> GetPositions();
    public abstract Vector2 GetCenter();
}