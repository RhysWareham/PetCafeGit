using UnityEngine;

public class RoomCell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Vector2Int GridPosition { get; private set; }

    public int FloorType { get; private set; }

    public bool IsOccupied { get; private set; }

    public void Initialise(Vector2Int gridPosition)
    {
        GridPosition = gridPosition;
        FloorType = 0;
        IsOccupied = false;

        ClearHighlight();
    }

    public void SetFloor(int floorType)
    {
        FloorType = floorType;
    }

    public void SetOccupied(bool occupied)
    {
        IsOccupied = occupied;
    }

    public void SetHighlight(bool valid)
    {
        spriteRenderer.color = valid
            ? Color.green
            : Color.red;
    }

    public void ClearHighlight()
    {
        spriteRenderer.color = Color.black;
    }
}