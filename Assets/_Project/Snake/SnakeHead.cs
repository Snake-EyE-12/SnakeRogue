using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float tickSpeed;
    private Vector2Int currentDirection = Vector2Int.right;
    private Vector2Int lastDirection = Vector2Int.right;
    [SerializeField] private int length = 10;
    private List<Vector3> storedPositions = new();

    private void Update()
    {
        ProcessInput();
        AttemptTick();
        if (Input.GetKeyDown(KeyCode.R)) length++;
    }

    private void ProcessInput()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        if (horizontal == 0 && vertical == 0) return;
        if(MovingHorizontally() && vertical != 0) currentDirection = new Vector2Int(0, vertical);
        if(MovingVertically() && horizontal != 0) currentDirection = new Vector2Int(horizontal, 0);
    }

    private bool MovingHorizontally()
    {
        return lastDirection.x != 0;
    }
    private bool MovingVertically()
    {
        return lastDirection.y != 0;
    }

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
        transform.Translate((Vector2)currentDirection);
        StorePosition(transform.position);
        lastDirection = currentDirection;
    }

    private void StorePosition(Vector2 pos)
    {
        storedPositions.Add(pos);
        if (storedPositions.Count > length)
        {
            storedPositions.RemoveAt(0);
        }

        line.positionCount = length;
        line.SetPositions(storedPositions.ToArray());
    }

    [SerializeField] private LineRenderer line;
    
}
