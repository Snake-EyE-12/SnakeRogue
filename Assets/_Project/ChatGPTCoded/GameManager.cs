using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject snakeSegmentPrefab;
    public GameObject foodPrefab;

    private Vector2Int gridSize = new Vector2Int(20, 20);
    private List<Transform> snake = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private float moveTimer;
    private float moveInterval = 0.2f;
    private Vector2Int foodPosition;

    void Start()
    {
        Vector2Int startPosition = new Vector2Int(gridSize.x / 2, gridSize.y / 2);
        GameObject head = Instantiate(snakeSegmentPrefab, new Vector3(startPosition.x, startPosition.y, 0), Quaternion.identity);
        snake.Add(head.transform);
        SpawnFood();
    }

    void Update()
    {
        HandleInput();
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0;
            Move();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2Int.down) direction = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2Int.up) direction = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2Int.right) direction = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2Int.left) direction = Vector2Int.right;
    }

    void Move()
    {
        Vector2Int headPos = Vector2Int.RoundToInt(snake[0].position);
        Vector2Int newHeadPos = headPos + direction;

        // Check collision
        if (newHeadPos.x < 0 || newHeadPos.x >= gridSize.x || newHeadPos.y < 0 || newHeadPos.y >= gridSize.y)
        {
            GameOver();
            return;
        }

        foreach (Transform segment in snake)
        {
            if (Vector2Int.RoundToInt(segment.position) == newHeadPos)
            {
                GameOver();
                return;
            }
        }

        // Move body
        for (int i = snake.Count - 1; i > 0; i--)
        {
            snake[i].position = snake[i - 1].position;
        }

        // Move head
        snake[0].position = new Vector3(newHeadPos.x, newHeadPos.y, 0);

        // Check food
        if (newHeadPos == foodPosition)
        {
            Grow();
            SpawnFood();
        }
    }

    void Grow()
    {
        GameObject segment = Instantiate(snakeSegmentPrefab, snake[snake.Count - 1].position, Quaternion.identity);
        snake.Add(segment.transform);
    }

    void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));
        } while (snake.Exists(s => Vector2Int.RoundToInt(s.position) == foodPosition));

        Instantiate(foodPrefab, new Vector3(foodPosition.x, foodPosition.y, 0), Quaternion.identity);
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
