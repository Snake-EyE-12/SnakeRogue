using System.Collections.Generic;
using UnityEngine;

public class BoardConstructor : MonoBehaviour
{
    
    [SerializeField] private VisualTile _tilePrefab;
    public Board BuildBoard(MapLayout layout)
    {
        var board = new Board();
        List<Vector2Int> positions = layout.GetPositions();
        foreach (var pos in positions)
        {
            board.AddPosition(pos);
            Instantiate(_tilePrefab, (Vector2)pos, Quaternion.identity, transform).Initialize(pos);
        }
        board.Center = (layout.GetCenter());
        return board;
    }

}

public class Board
{
    public void AddPosition(Vector2 position) { }
    public Vector2 Center { get; set; }
}