using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDrawer : MonoBehaviour
{
    public Snake snake;
    public LineRenderer line;
    [SerializeField] private int precision = 5;

    private void Awake()
    {
        line.widthMultiplier = 0.5f;
    }
    private void Update()
    {
        // Step 1: Get the current positions from GetPosArray()
        var positions = snake.GetPosArray();

        // Step 2: Add additional points between each position
        var smoothPositions = AddInterpolatedPoints(positions, precision);  // Here '5' controls how many points to add between each segment

        // Step 3: Set the LineRenderer's positions
        line.positionCount = smoothPositions.Length;
        line.SetPositions(smoothPositions);
    }

// Method to add interpolated points between each pair of positions
    private Vector3[] AddInterpolatedPoints(Vector3[] positions, int pointsBetweenSegments)
    {
        List<Vector3> newPositions = new List<Vector3>();

        // Loop through each pair of adjacent points
        for (int i = 0; i < positions.Length - 1; i++)
        {
            // Add the current position
            newPositions.Add(positions[i]);

            // Add interpolated points between this position and the next
            for (int j = 1; j <= pointsBetweenSegments; j++)
            {
                // Interpolate between positions[i] and positions[i + 1]
                float t = (float)j / (pointsBetweenSegments + 1);
                Vector3 interpolatedPoint = Vector3.Lerp(positions[i], positions[i + 1], t);
                newPositions.Add(interpolatedPoint);
            }
        }

        // Add the last position (no need to interpolate further)
        newPositions.Add(positions[positions.Length - 1]);

        return newPositions.ToArray();
    }
}