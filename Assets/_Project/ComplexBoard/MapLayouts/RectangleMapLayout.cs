using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapLayout/Rectangle", fileName = "RectangleMapLayout", order = 0)]
public class RectangleMapLayout : MapLayout
{
    [SerializeField] private Vector2Int size = new Vector2Int(11, 11);

    public override List<Vector2Int> GetPositions()
    {
        var positions = new List<Vector2Int>(size.x * size.y);
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                positions.Add(new Vector2Int(x, y));
            }
        }
        return positions;
    }

    public override Vector2 GetCenter()
    {
        return new Vector2((size.x - 1f) / 2f, (size.y - 1f) / 2f);
    }
}