//using System.Collections.Generic;
//using UnityEngine;

//public enum GridLayer
//{
//    FLOOR,
//    FURNITURE,
//    FOOD
//}

//public class GridManager : MonoBehaviour
//{
//    public static GridManager Instance;

//    [SerializeField]
//    private int width = 20;

//    [SerializeField]
//    private int height = 20;

//    [SerializeField]
//    private float cellSize = 1f;

//    private GridCell[,] grid;

//    void Awake()
//    {
//        Instance = this;

//        grid = new GridCell[width, height];

//        for (int x = 0; x < width; x++)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                grid[x, y] = new GridCell();
//            }
//        }
//    }

//    public Vector2Int WorldToGrid(Vector3 world)
//    {
//        return new Vector2Int(
//            Mathf.RoundToInt(world.x / cellSize),
//            Mathf.RoundToInt(world.y / cellSize));
//    }

//    public Vector3 GridToWorld(Vector2Int gridPos)
//    {
//        return new Vector3(
//            gridPos.x * cellSize,
//            gridPos.y * cellSize,
//            0);
//    }

//    public bool CanPlace(Vector2Int pos, Vector2Int size)
//    {
//        for (int x = 0; x < size.x; x++)
//        {
//            for (int y = 0; y < size.y; y++)
//            {
//                int gx = pos.x + x;
//                int gy = pos.y + y;

//                if (gx < 0 || gy < 0 || gx >= width || gy >= height)
//                    return false;

//                if (grid[gx, gy].Occupant != null)
//                    return false;
//            }
//        }

//        return true;
//    }

//    public void PlaceObject(PlaceableObject obj, Vector2Int pos)
//    {
//        obj.GridPosition = pos;

//        for (int x = 0; x < obj.Size.x; x++)
//        {
//            for (int y = 0; y < obj.Size.y; y++)
//            {
//                grid[pos.x + x, pos.y + y].Occupant = obj;
//            }
//        }
//    }

//    public void RemoveObject(PlaceableObject obj)
//    {
//        for (int x = 0; x < width; x++)
//        {
//            for (int y = 0; y < height; y++)
//            {
//                if (grid[x, y].Occupant == obj)
//                    grid[x, y].Occupant = null;
//            }
//        }
//    }
//}