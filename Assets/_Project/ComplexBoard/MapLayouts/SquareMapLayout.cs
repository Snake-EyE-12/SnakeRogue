using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapLayout/Square", fileName = "SquareMapLayout", order = 0)]
public class SquareMapLayout : MapLayout
{
    [SerializeField, Min(0)] private int size = 11;

    public override List<Vector2Int> GetPositions()
    {
        var positions = new List<Vector2Int>(size * size);
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                positions.Add(new Vector2Int(x, y));
            }
        }
        return positions;
    }

    public override Vector2 GetCenter()
    {
        return Vector2.one * ((size - 1f) / 2f);
    }
}