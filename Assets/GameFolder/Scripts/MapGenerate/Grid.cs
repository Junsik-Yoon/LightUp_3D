

using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject groundPrefab;
    [HideInInspector]
    public GameObject parentGrid;

    public Vector2 gridWorldSize;

    Node[,] grid;

    private void Awake()
    {
    }

    public bool CreateGrid()
    {
        if (gridWorldSize.x < 2 || gridWorldSize.x > 101 || gridWorldSize.y < 2 || gridWorldSize.y > 51)
            return false;

        float cameraY = gridWorldSize.x * 0.42f > gridWorldSize.y * 0.87f ? gridWorldSize.x * 0.42f : gridWorldSize.y * 0.87f;
        transform.position = new Vector3(0, cameraY, 0);

        if (parentGrid != null)
            Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");

        grid = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];
        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        int randomNumX = Random.Range(1,(int)gridWorldSize.x-2);//스타트노드거나 엔드노드일시 예외처리하기
        int randomNumY = Random.Range(0,(int)gridWorldSize.y-2);
        int randomNumX2 = Random.Range(0,(int)gridWorldSize.x-2);//스타트노드거나 엔드노드일시 예외처리하기
        int randomNumY2 = Random.Range(1,(int)gridWorldSize.y-2);
        int randomNumX3 = Random.Range(1,(int)gridWorldSize.x-2);//스타트노드거나 엔드노드일시 예외처리하기
        int randomNumY3 = Random.Range(0,(int)gridWorldSize.y-2);
        int randomNumX4 = Random.Range(0,(int)gridWorldSize.x-2);//스타트노드거나 엔드노드일시 예외처리하기
        int randomNumY4 = Random.Range(1,(int)gridWorldSize.y-2);
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (y + 0.5f);
                GameObject obj = Instantiate(groundPrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform;
                if(x==randomNumX && y == randomNumY)
                {
                    grid[x, y] = new Node(obj, true, x, y,10);
                    grid[x, y].ChangeColor = Color.magenta;
                }
                else if(x==randomNumX2 && y == randomNumY2)
                {
                    grid[x, y] = new Node(obj, true, x, y,10);
                    grid[x, y].ChangeColor = Color.magenta;
                }
                else if(x==randomNumX3 && y == randomNumY3)
                {
                    grid[x, y] = new Node(obj, true, x, y,10);
                    grid[x, y].ChangeColor = Color.magenta;
                }
                else if(x==randomNumX4 && y == randomNumY4)
                {
                    grid[x, y] = new Node(obj, true, x, y,10);
                    grid[x, y].ChangeColor = Color.magenta;
                }
                else
                {
                     grid[x, y] = new Node(obj, true, x, y,1);
                }
                
                
            }
        }
        return true;
    }

    public void ResetGrid()
    {
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                if (grid[x, y].walkable && !grid[x, y].start && !grid[x,y].end)
                {
                    grid[x, y].ChangeNode = true;
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
        bool[] walkableUDLR = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            int checkX = node.gridX + temp[i, 0];
            int checkY = node.gridY + temp[i, 1];
            if (checkX >= 0 && checkX < (int)gridWorldSize.x && checkY >= 0 && checkY < (int)gridWorldSize.y)
            {
                if (grid[checkX, checkY].walkable)
                    walkableUDLR[i] = true;
                neighbours.Add(grid[checkX, checkY]);
            }
        }


        return neighbours;
    }

    public Node StartNode
    {
        get
        {
            grid[0,0].start=true;
            return grid[0,0];
        }
    }
    public Node EndNode
    {
        get
        {
            int randomIntX = Random.Range(1,(int)gridWorldSize.x - 1);
            int randomIntY = Random.Range(1,(int)gridWorldSize.y - 1);
            grid[randomIntX,randomIntY].end = true;
            return grid[randomIntX,randomIntY];
        }
    }
}
