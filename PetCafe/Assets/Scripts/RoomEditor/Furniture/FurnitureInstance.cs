using UnityEngine;

public class FurnitureInstance : MonoBehaviour
{
    public FurnitureDefinition Definition { get; private set; }

    public Vector2Int GridPosition { get; private set; }

    public int Rotation { get; private set; }

    public void Initialise(
        FurnitureDefinition definition,
        Vector2Int gridPosition,
        int rotation = 0)
    {
        Definition = definition;
        GridPosition = gridPosition;
        Rotation = rotation;

        transform.rotation =
            Quaternion.Euler(0f, 0f, Rotation);
    }

    public void SetGridPosition(Vector2Int position)
    {
        GridPosition = position;
    }

    public void SetRotation(int rotation)
    {
        Rotation = rotation;

        transform.rotation =
            Quaternion.Euler(0f, 0f, Rotation);
    }
}