using UnityEngine;

public class VisualTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TileRenderData renderData;
    public void Initialize(Vector2Int coords)
    {
        _spriteRenderer.sprite = renderData.GetSprite();
        _spriteRenderer.color = renderData.GetColor(coords.x, coords.y);
    }
}