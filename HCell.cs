using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCell
{
    // Property to get set alive status of the cell
    public bool IsAlive { get; set; }

    // Initialize the cell with a specified alive status
    public HCell(bool initialState)
    {
        IsAlive = initialState;
    }

    // Updates the cell's state based on the number of living neighbors
    public void UpdateState(int livingNeighbors)
    {
        // If the cell is currently alive
        if (IsAlive)
        {
            // A living cell dies if it has fewer than 2 or more than 3 living neighbors
            if (livingNeighbors < 2 || livingNeighbors > 3)
            {
                IsAlive = false;
            }
        }
        // If the cell is currently dead
        else
        {
            // A dead cell becomes alive if it has exactly 3 living neighbors
            if (livingNeighbors == 3)
            {
                IsAlive = true;
            }
        }
    }
}