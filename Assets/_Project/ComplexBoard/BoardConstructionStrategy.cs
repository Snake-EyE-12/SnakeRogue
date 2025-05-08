using UnityEngine;

public abstract class BoardConstructionStrategy : ScriptableObject
{
    protected Vector2 _origin;
    protected Vector2 _scale;
    public void SetOrigin(Vector2 origin) => this._origin = origin;
    public void SetScale(Vector2 scale) => this._scale = scale;
    protected void BuildTile(TileConstructionBuildData data)
    {
        //Instantiate(data._tilePrefab, data.GetPlacementPosition(), Quaternion.identity, data.parent).Initialize(data);
    }

    public abstract void ConstructAt(Vector2 pos);
}

[CreateAssetMenu(menuName = "BoardConstructionStrategy/PerfectGrid", fileName = "PerfectGrid", order = 0)]
public class BoardConstructionPerfectGrid : BoardConstructionStrategy
{
    private BoardConstructionBuildPattern pattern;
    
    TileConstructionBuildData data = 
        new TileConstructorBuilder()
            .SetX(0)
            .SetY(0)
            .Build();

    public override void ConstructAt(Vector2 pos)
    {
        BuildTile(data);
    }
}

public abstract class BoardConstructionBuildPattern : ScriptableObject
{
    public abstract TileConstructionBuildData Build();
}

public class BoardConstructionSimpleGrid : BoardConstructionBuildPattern
{
    public override TileConstructionBuildData Build()
    {
        return new TileConstructorBuilder()
            .SetX(0)
            .SetY(0)
            .Build();
    }
}

public class TileConstructionBuildData
{
    public int x;
    public int y;
    public Vector2 scale;
    public Vector2 offset;
    public VisualTile _tilePrefab;
    public Transform parent;
    
    public Vector2 GetPlacementPosition() => new Vector2(x * scale.x + offset.x, y * scale.y + offset.y);

    public TileConstructionBuildData()
    {
    }
}

public class TileConstructorBuilder
{
    private TileConstructionBuildData data;
    public TileConstructionBuildData Build() => data;
    public TileConstructorBuilder SetX(int x)
    {
        data.x = x;
        return this;
    }

    public TileConstructorBuilder SetY(int y)
    {
        data.y = y;
        return this;
    }

    public TileConstructorBuilder SetScale(Vector2 scale)
    {
        data.scale = scale;
        return this;
    }

    public TileConstructorBuilder SetOffset(Vector2 offset)
    {
        data.offset = offset;
        return this;
    }

    public TileConstructorBuilder SetPrefab(VisualTile prefab)
    {
        data._tilePrefab = prefab;
        return this;
    }

    public TileConstructorBuilder SetParent(Transform parent)
    {
        data.parent = parent;
        return this;
    }
}