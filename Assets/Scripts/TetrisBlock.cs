using System;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float previousTime;
    private float fallTime = 0.8f;
    private static int width = 10;
    private static int height = 20;
    private static Transform[,] grid = new Transform[width, height];

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // local position -> global position
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90f);

            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90f);
            }
        }


        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                enabled = false;
                FindObjectOfType<Spawn>().NewTetromino();
            }

            previousTime = Time.time;
        }
    }

    private void CheckForLines()
    {
        for (int row = height - 1; row >= 0; row--)
        {
            if (HasLine(row))
            {
                DeleteLine(row);
                RowDown(row);
            }
        }
    }

    private bool HasLine(int row)
    {
        for (int col = 0; col < width; col++)
        {
            if (grid[col, row] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void DeleteLine(int row)
    {
        for (int col = 0; col < width; col++)
        {
            Destroy(grid[col, row].gameObject);
            grid[col, row] = null;
        }
    }

    private void RowDown(int currentRow)
    {
        for (int row = currentRow; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (grid[col, row] != null)
                {
                    grid[col, row - 1] = grid[col, row];
                    grid[col, row] = null;
                    grid[col, row - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    private void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            grid[roundedX, roundedY] = child;
        }
    }

    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }
}