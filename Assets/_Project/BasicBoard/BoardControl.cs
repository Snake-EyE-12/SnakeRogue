using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    [SerializeField] private VisualTile tilePrefab;
    private List<Vector2Int> boardSpaces = new();
    public void AddSpace(Vector2Int space)
    {
        Instantiate(tilePrefab, (Vector2)space, Quaternion.identity, transform).Initialize(space);
        boardSpaces.Add(space);
    }
}