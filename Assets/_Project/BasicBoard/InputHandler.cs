using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Vector2Int lastDirection = Vector2Int.right;
    [SerializeField] private Snake snake;
    private void Update()
    {
        ProcessInput();
    }
    
    private void ProcessInput()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");
        if (horizontal == 0 && vertical == 0) return;
        if(MovingHorizontally() && vertical != 0) snake.nextDirection = new Vector2Int(0, vertical);
        if(MovingVertically() && horizontal != 0) snake.nextDirection = new Vector2Int(horizontal, 0);
    }
    public void UpdateLastDirection(Vector2Int direction)
    {
        lastDirection = direction;
    }

    private bool MovingHorizontally()
    {
        return lastDirection.x != 0;
    }
    private bool MovingVertically()
    {
        return lastDirection.y != 0;
    }
}
