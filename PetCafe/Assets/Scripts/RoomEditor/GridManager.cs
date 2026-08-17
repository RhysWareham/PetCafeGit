using UnityEngine;

public enum GridLayer
{
    FLOOR,
    FURNITURE,
    FOOD
}

public class GridManager : MonoBehaviour
{
    [Header("Grid Size")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 8;

    [Header("Grid Settings")]
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero;

    [Header("Room")]
    [SerializeField] private RoomCell cellPrefab;
    [SerializeField] private Transform cellParent;

    private RoomCell[,] cells;

    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;

    private void Awake()
    {
        CreateGrid();
    }

    private void Start()
    {
        GetCell(new Vector2Int(4, 3))
            .SetOccupied(true);

        GetCell(new Vector2Int(5, 3))
            .SetOccupied(true);
    }

    private void CreateGrid()
    {
        cells = new RoomCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);

                Vector2 worldPosition = GridToWorld(gridPosition);

                RoomCell cell = Instantiate(
                    cellPrefab,
                    worldPosition,
                    Quaternion.identity,
                    cellParent
                );

                cell.Initialise(gridPosition);

                cells[x, y] = cell;
            }
        }
    }

    /// <summary>
    /// Converts a grid coordinate into a world position.
    /// </summary>
    public Vector2 GridToWorld(Vector2Int gridPosition)
    {
        return gridOrigin + new Vector2(
            gridPosition.x * cellSize + cellSize / 2f,
            gridPosition.y * cellSize + cellSize / 2f
        );
    }

    public Vector2 GridToWorld(Vector2Int gridPosition, Vector2Int size)
    {
        Vector2 bottomLeft = GridToWorld(gridPosition);

        return bottomLeft + new Vector2(
            (size.x - 1) * cellSize / 2f,
            (size.y - 1) * cellSize / 2f
        );
    }

    /// <summary>
    /// Converts a world position into the grid coordinate it is inside.
    /// </summary>
    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        Vector2 localPosition = worldPosition - gridOrigin;

        int x = Mathf.FloorToInt(localPosition.x / cellSize);
        int y = Mathf.FloorToInt(localPosition.y / cellSize);

        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Returns the nearest valid grid position.
    /// </summary>
    public Vector2Int GetClampedGridPosition(Vector2Int gridPosition)
    {
        return new Vector2Int(
            Mathf.Clamp(gridPosition.x, 0, width - 1),
            Mathf.Clamp(gridPosition.y, 0, height - 1)
        );
    }

    /// <summary>
    /// Checks whether a grid coordinate exists inside the room.
    /// </summary>
    public bool IsInsideGrid(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 &&
               gridPosition.x < width &&
               gridPosition.y >= 0 &&
               gridPosition.y < height;
    }

    public RoomCell GetCell(Vector2Int position)
    {
        if (!IsInsideGrid(position))
            return null;

        return cells[position.x, position.y];
    }

    public bool PreviewPlacement(Vector2Int origin, Vector2Int size)
    {
        bool isValid = CanPlace(origin, size);

        ClearHighlights();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int cellPosition =
                    origin + new Vector2Int(x, y);

                if (!IsInsideGrid(cellPosition))
                    continue;

                cells[cellPosition.x, cellPosition.y]
                    .SetHighlight(isValid);
            }
        }

        return isValid;
    }

    public bool CanPlace(Vector2Int origin, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int cellPosition =
                    origin + new Vector2Int(x, y);

                // Outside the room
                if (!IsInsideGrid(cellPosition))
                    return false;

                // Already occupied
                if (cells[cellPosition.x, cellPosition.y].IsOccupied)
                    return false;
            }
        }

        return true;
    }

    public void ClearHighlights()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y].ClearHighlight();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.gray;

    //    for (int x = 0; x <= width; x++)
    //    {
    //        Vector3 start = new Vector3(
    //            gridOrigin.x + x * cellSize,
    //            gridOrigin.y,
    //            0f
    //        );

    //        Vector3 end = new Vector3(
    //            gridOrigin.x + x * cellSize,
    //            gridOrigin.y + height * cellSize,
    //            0f
    //        );

    //        Gizmos.DrawLine(start, end);
    //    }

    //    for (int y = 0; y <= height; y++)
    //    {
    //        Vector3 start = new Vector3(
    //            gridOrigin.x,
    //            gridOrigin.y + y * cellSize,
    //            0f
    //        );

    //        Vector3 end = new Vector3(
    //            gridOrigin.x + width * cellSize,
    //            gridOrigin.y + y * cellSize,
    //            0f
    //        );

    //        Gizmos.DrawLine(start, end);
    //    }
    //}
}