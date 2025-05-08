using System.Collections.Generic;
using UnityEngine;

public class GoogleSnakeGame : MonoBehaviour
{
    public GameObject snakeSegmentPrefab;
    public GameObject foodPrefab;
    public int gridWidth = 21;
    public int gridHeight = 21;
    public float moveDelay = 0.15f;
    public bool allowWrapping = true;

    private List<Transform> snake = new List<Transform>();
    private Vector2Int direction = Vector2Int.right;
    private float timer;
    private Vector2Int foodPosition;
    private bool grow;

    void Start()
    {
        Vector2Int center = new Vector2Int(gridWidth / 2, gridHeight / 2);
        GameObject head = Instantiate(snakeSegmentPrefab, GridToWorld(center), Quaternion.identity);
        snake.Add(head.transform);
        SpawnFood();
    }

    void Update()
    {
        HandleInput();
        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0;
            Move();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2Int.down) direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2Int.up) direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2Int.right) direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2Int.left) direction = Vector2Int.right;
    }

    void Move()
    {
        Vector2Int currentPos = WorldToGrid(snake[0].position);
        Vector2Int newHeadPos = currentPos + direction;

        // Wrap around screen edges
        if (allowWrapping)
        {
            newHeadPos.x = (newHeadPos.x + gridWidth) % gridWidth;
            newHeadPos.y = (newHeadPos.y + gridHeight) % gridHeight;
        }
        else if (newHeadPos.x < 0 || newHeadPos.x >= gridWidth || newHeadPos.y < 0 || newHeadPos.y >= gridHeight)
        {
            GameOver();
            return;
        }

        // Move body
        if (!grow)
        {
            Transform tail = snake[snake.Count - 1];
            snake.RemoveAt(snake.Count - 1);
            tail.position = GridToWorld(newHeadPos);
            snake.Insert(0, tail);
        }
        else
        {
            GameObject segment = Instantiate(snakeSegmentPrefab, GridToWorld(newHeadPos), Quaternion.identity);
            snake.Insert(0, segment.transform);
            grow = false;
        }

        // Check for collision with self
        for (int i = 1; i < snake.Count; i++)
        {
            if (WorldToGrid(snake[i].position) == newHeadPos)
            {
                GameOver();
                return;
            }
        }

        // Check for food
        if (newHeadPos == foodPosition)
        {
            grow = true;
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
        } while (snake.Exists(s => WorldToGrid(s.position) == foodPosition));

        Instantiate(foodPrefab, GridToWorld(foodPosition), Quaternion.identity);
    }

    Vector3 GridToWorld(Vector2Int gridPos) => new Vector3(gridPos.x - gridWidth / 2, gridPos.y - gridHeight / 2, 0);
    Vector2Int WorldToGrid(Vector3 worldPos) => new Vector2Int(Mathf.RoundToInt(worldPos.x + gridWidth / 2), Mathf.RoundToInt(worldPos.y + gridHeight / 2));

    void GameOver()
    {
        Debug.Log("Game Over!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
