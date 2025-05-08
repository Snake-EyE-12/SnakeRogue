using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private InitialBoardPosition startingPositionsContainer = new InitialBoardPosition();
    public BoardControl controller;
    private void Awake()
    {
        foreach (var pos in startingPositionsContainer.GetPositions())
        {
            controller.AddSpace(pos);
        }
        snake.Spawn(Vector2Int.zero);
    }

    [SerializeField] private Snake snake;

    private void Update()
    {
        AttemptTick();
    }

    [SerializeField] private float tickSpeed = 0.2f;
    private float timeOfLastTick = 0;
    private void AttemptTick()
    {
        if (Time.time - timeOfLastTick > tickSpeed)
        {
            timeOfLastTick = Time.time;
            Tick();
        }
    }

    private void Tick()
    {
        snake.Tick();
    }
}

[Serializable]
public class InitialBoardPosition
{
    public int size;
    public List<Vector2Int> GetPositions()
    {
        var positions = new List<Vector2Int>();
        int alteredSize = size / 2;
        for(int i = -alteredSize; i <= alteredSize; i++)
        {
            for(int j = -alteredSize; j <= alteredSize; j++)
            {
                positions.Add(new Vector2Int(i, j));
            }
        }
        return positions;
    }
}