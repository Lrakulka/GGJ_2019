using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleScript : MonoBehaviour
{
    [Serializable]
    public class Tile
    {
        public int height;
        public int width;
        public int pos;
        public Transform transform;
    }

    [SerializeField]
    Vector2 tileSize;

    [SerializeField]
    public Tile[] grid = new Tile[6];

    [SerializeField]
    public int length;

    bool[,] boolGrid;


    System.Random random = new System.Random(System.DateTime.Now.Second);

    private void Start()
    {
        Debug.Log(Time.realtimeSinceStartup);
        boolGrid = new bool[2, length];
        for(int i = 0; i < boolGrid.GetLength(0); i++)
        {
            for (int j = 0; j < boolGrid.GetLength(1); j++)
            {
                boolGrid[i, j] = false;
            }
        }

        for (int i = 0; i < grid.Length; i++)
        {
            AddFurniture(grid[i]);
        }
        Debug.Log(Time.realtimeSinceStartup);
    }

    bool AddFurniture(Tile tile)
    {
        
        GameObject go = Instantiate(tile.transform.gameObject, transform);
        int count = 10;
        int pos;
        Vector3 supplement = new Vector3(0,0, 0);
        do
        {
            pos = random.Next(0, length);
            if (1 == tile.width && 1 == tile.height && false == boolGrid[tile.pos, pos])
            {
                boolGrid[tile.pos, pos] = true;
                break;
            }
            else if (
                2 == tile.width && 1 == tile.height && pos < length - 1 && 
                false == boolGrid[tile.pos, pos] && false == boolGrid[tile.pos, pos + 1]
                )
            {
                boolGrid[tile.pos, pos] = true;
                boolGrid[tile.pos, pos + 1] = true;
                supplement.Set(tileSize.x / 2, 0, 0);
                break;
            }
            else if (
                1 == tile.width && 2 == tile.height &&
                false == boolGrid[0, pos] && false == boolGrid[1, pos]
                )
            {
                boolGrid[0, pos] = true;
                boolGrid[1, pos] = true;
                break;
            }
            count++;
        } while (true && count < 100);

        go.transform.position += new Vector3(tileSize.x * pos - tileSize.x * length / 2, -tileSize.y + (tile.pos) * tileSize.y, 0) + supplement;
        return true;
    }

    private void Update()
    {
        
    }

}
