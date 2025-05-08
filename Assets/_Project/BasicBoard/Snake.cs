using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public List<Vector2Int> positions = new();
    public int length = 3;
    private Vector2Int headPosition;
    private Vector2Int lastRemovedPosition;
    public void Spawn(Vector2Int headPos)
    {
        headPosition = headPos;
        transform.position = (Vector2)headPos;
        positions.Add(headPos);
        for (int i = 1; i < length; i++)
        {
            positions.Add(headPos + new Vector2Int(-i, 0));
            if (i == length - 1) lastRemovedPosition = headPos + new Vector2Int(-i, 0);
        }
    }
    
    
    public Vector3[] GetPosArray()
    {
        var array = new Vector3[positions.Count + 1];

        // Interpolating the head position (between positions[1] and positions[0])
        array[0] = Vector3.Lerp((Vector2)positions[1], (Vector2)positions[0], TPercent);

        // Fill the body segments (snap to grid)
        for (int i = 1; i < positions.Count; i++)
        {
            array[i] = (Vector2)positions[i];  // These stay aligned to the grid
        }

        // Add the interpolated tail (between lastRemovedPosition and the last real position)
        array[positions.Count] = Vector3.Lerp((Vector2)lastRemovedPosition, (Vector2)positions[positions.Count - 1], TPercent);

        return array;
    }
    
    // public Vector3[] GetPosArray()
    // {
    //     var array = new Vector3[positions.Count + 2];
    //
    //     array[0] = Vector3.Lerp((Vector2)positions[1], (Vector2)positions[0], TPercent);
    //
    //     for (int i = 1; i < positions.Count; i++)
    //     {
    //         array[i] = (Vector2)positions[i];
    //     }
    //     array[positions.Count + 1] = Vector3.Lerp((Vector2)lastRemovedPosition, (Vector2)positions[positions.Count - 1], TPercent);
    //
    //     return array;
    // }

    // public Vector3[] GetPosArray()
    // {
    //     var array = new Vector3[positions.Count + 1];
    //     array[0] = Vector3.Lerp((Vector2)positions[1], (Vector2)positions[0], TPercent);
    //     for (int i = 1; i < positions.Count - 1; i++)
    //     {
    //         //array[i] = Vector3.Lerp((Vector2)positions[i + 1], (Vector2)positions[i], TPercent);
    //         array[i] = (Vector2)positions[i];
    //     }
    //     array[positions.Count - 1] = (Vector2)positions[positions.Count - 1];
    //     array[positions.Count] = Vector3.Lerp((Vector2)lastRemovedPosition, (Vector2)positions[positions.Count - 1], TPercent);
    //     return array;
    // }

    private float tickSpeed = 0.2f;
    private float TPercent => (Time.time - timeOfLastTick) / tickSpeed;
    private float timeOfLastTick = 0;
    public void Tick()
    {
        timeOfLastTick = Time.time;
        Move();
    }
    public void Move()
    {
        Vector2Int nextHeadPos = headPosition + nextDirection;
        positions.Insert(0, nextHeadPos);
        headPosition = nextHeadPos;
        transform.position = (Vector2)nextHeadPos;
        TryRemovePiece();
        if(nextDirection != Vector2Int.zero) ih.UpdateLastDirection(nextDirection);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) length++;
    }

    private void TryRemovePiece()
    {
        if (positions.Count < length) return;
        int last = positions.Count - 1;
        lastRemovedPosition = positions[last];
        positions.RemoveAt(last);
    }

    [SerializeField] private InputHandler ih;

    public Vector2Int nextDirection;
}