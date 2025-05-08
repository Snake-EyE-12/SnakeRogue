using UnityEngine;

[CreateAssetMenu(menuName = "TileRenderData", fileName = "TileRenderData", order = 0)]
public class TileRenderData : ScriptableObject
{
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private Color evenColor;
    [SerializeField] private Color oddColor;

    public Sprite GetSprite()
    {
        return tileSprite;
    }

    public Color GetColor(int x, int y)
    {
        return ((x + y) % 2 == 0) ? evenColor : oddColor;
    }
}