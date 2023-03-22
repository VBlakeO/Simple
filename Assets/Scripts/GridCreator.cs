using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public Vector2 gridProportion;
    public GameObject block;
    public Transform iniciaPosition;
    public GameObject[,] grid;

    public float verticalDistance = 0.65f;
    public float horizontalDistance = 0.76f;
    public float shear = 0.38f;

    public string m_name;

    public bool m_create = false;
    public bool m_update = false;


    private void OnValidate()
    {
        if (m_update)
        {
            Create();
        }
    }

    void Create()
    {
        if (m_create)
        {
            int cont = 0;

            grid = new GameObject[(int)gridProportion.y, (int)gridProportion.x];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    cont++; 
                    grid[i, j] = Instantiate(block, iniciaPosition);
                    grid[i, j].transform.name = m_name + "_" + cont;
                }
            }
        }

        
            Organize();
    }

    void Organize()
    {
        for (int i = 1; i < gridProportion.y; i++)
        {
            if(i%2 == 0)
                grid[i, 0].transform.position = new Vector3(grid[i, 0].transform.position.x, grid[i - 1, 0].transform.position.y - verticalDistance, 0);
            else
                grid[i, 0].transform.position = new Vector3(grid[0, 0].transform.position.x + shear, grid[i - 1, 0].transform.position.y - verticalDistance, 0);

        }

        for (int i = 1;i < gridProportion.x; i++)
        {
            grid[0, i].transform.position = new Vector3(grid[0, i - 1].transform.position.x + horizontalDistance, grid[0, i].transform.position.y, 0);
        }

        for (int i = 1; i < grid.GetLength(0); i++)
        {
            for (int j = 1; j < grid.GetLength(1); j++)
            {
                grid[i, j].transform.position = new Vector3(grid[i, j - 1].transform.position.x + horizontalDistance, grid[i - 1, j].transform.position.y - verticalDistance, 0);
            }
        }

    }
}
