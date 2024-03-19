using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HController : MonoBehaviour
{
    private HCell[,] grid; // 2D array of HCell for the grid
    public HView view; // View component for visual representation
    private int gridSize = 40; // Dimensions of the grid
    private int currentGeneration = 0; // Current generation count
    private int[] pauseGenerations = new int[] { 1, 5, 10, 20, 50 }; // Generations at which to pause
    private bool pauseSimulation = true; // Flag to pause  simulation

    void Start()
    {
        // Initialize HView component
        view = FindObjectOfType<HView>();
        if (view == null)
        {
            Debug.LogError("HView component not found. Please add HView to a GameObject.");
            return;
        }
        view.Initialize(gridSize, gridSize);
        InitializeGrid();
        PlacePatterns();
        UpdateView();
    }

    void Update()
    {
        // Handle space bar input for simulation control
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseSimulation = false;
            Debug.Log("Continuing simulation: Generation " + currentGeneration);
        }

        // Update simulation if not paused
        if (!pauseSimulation)
        {
            UpdateGeneration();
            UpdateView();
            Debug.Log("Current Generation: " + currentGeneration);

            // Pause simulation at specified generations
            if (System.Array.IndexOf(pauseGenerations, currentGeneration) >= 0)
            {
                pauseSimulation = true;
                Debug.Log("Paused at Generation " + currentGeneration);
            }

            currentGeneration++; // Increment in generation count
        }
    }

    // Initialize the grid with dead cells
    void InitializeGrid()
    {
        grid = new HCell[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                grid[i, j] = new HCell(false); // Initialize each cell
            }
        }
    }

    // Set initial patterns on the grid
    void PlacePatterns()
    {
        // Implement specific patterns
        /*
        // Placing Pattern 1
        grid[19, 19].IsAlive = true;
        grid[19, 20].IsAlive = true;
        grid[20, 19].IsAlive = true;
        grid[20, 20].IsAlive = true;

        
        // Placing Pattern 2
        grid[18, 18].IsAlive = true;
        grid[21, 18].IsAlive = true;
        grid[22, 19].IsAlive = true;
        grid[18, 20].IsAlive = true;
        grid[22, 20].IsAlive = true;
        grid[19, 21].IsAlive = true;
        grid[20, 21].IsAlive = true;
        grid[21, 21].IsAlive = true;
        grid[22, 21].IsAlive = true;
        */
        // Placing Pattern 3
        grid[18, 17].IsAlive = true;
        grid[19, 17].IsAlive = true;
        grid[18, 16].IsAlive = true;
        grid[19, 16].IsAlive = true;
        grid[19, 21].IsAlive = true;
        grid[20, 21].IsAlive = true;
        grid[21, 21].IsAlive = true;
        grid[19, 22].IsAlive = true;
        grid[20, 23].IsAlive = true;
        
    }

    // Update the grid for the next generation
    public void UpdateGeneration()
    {
    // Create a temporary grid to store the next generation states
    bool[,] nextGeneration = new bool[gridSize, gridSize];

    // Calculate the next generation without updating the current one
    for (int x = 0; x < gridSize; x++)
    {
        for (int y = 0; y < gridSize; y++)
        {
            int livingNeighbors = CountLivingNeighbors(x, y);
            if (grid[x, y].IsAlive)
            {
                // Apply the rules for living cells
                nextGeneration[x, y] = livingNeighbors == 2 || livingNeighbors == 3;
            }
            else
            {
                // Apply the rules for dead cells
                nextGeneration[x, y] = livingNeighbors == 3;
            }
        }
    }

    // Update the current grid with the new states
    for (int x = 0; x < gridSize; x++)
    {
    for (int y = 0; y < gridSize; y++)
    {
        grid[x, y].IsAlive = nextGeneration[x, y];
        // Update the view with the new state by passing the IsAlive boolean
        view.SetCellColor(x, y, grid[x, y].IsAlive);
    }
    }
    }   

    // Count living neighbors of a cell
    private int CountLivingNeighbors(int x, int y)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue; // Exclude the cell itself
                int nx = x + i, ny = y + j;
                // Count living neighbors within bounds
                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize)
                {
                    count += grid[nx, ny].IsAlive ? 1 : 0;
                }
            }
        }
        return count;
    }

    // Update the view to reflect the current grid state
    void UpdateView()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                view.SetCellColor(x, y, grid[x, y].IsAlive);
            }
        }
    }
}
