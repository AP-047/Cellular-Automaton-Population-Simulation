using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using oompe.lib;

public class HView : MonoBehaviour
{
    private GameObject cellRoot; // Root object for cell GameObjects
    private Rectangle[,] rectangles; // Grid of rectangle objects
    private int gridSize = 40; // Set size of the grid

    void Awake()
    {
        // Initialize cellRoot GameObject
        if (cellRoot == null)
        {
            cellRoot = new GameObject("CellRoot");
        }

        Initialize(gridSize, gridSize); // Initialize grid view
    }

    // Initialize the grid with rectangles
    public void Initialize(int width, int height)
    {
        rectangles = new Rectangle[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                rectangles[i, j] = new Rectangle(cellRoot, i, j, 1, 1);
                rectangles[i, j].Draw(Color.clear);
            }
        }
    }

    // Update cell color based on its alive state
    public void SetCellColor(int x, int y, bool isAlive)
    {
        Color cellColor = isAlive ? Color.yellow : Color.grey; // Determine cell color
        rectangles[x, y].SetColor(cellColor); // Set color
        rectangles[x, y].Draw(); // Redraw cell
    }
}