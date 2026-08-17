using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    public GridManager GridManager => gridManager;

    public FurnitureInstance PlaceFurniture(
        FurnitureDefinition definition,
        Vector2Int gridPosition)
    {
        if (definition == null)
        {
            Debug.LogError("Furniture definition is null.");
            return null;
        }

        if (definition.Prefab == null)
        {
            Debug.LogError(
                $"Furniture '{definition.DisplayName}' has no prefab."
            );

            return null;
        }

        if (!gridManager.CanPlace(
                gridPosition,
                definition.Size))
        {
            return null;
        }

        Vector2 worldPosition =
            gridManager.GridToWorld(gridPosition, definition.Size);

        GameObject furnitureObject =
            Instantiate(
                definition.Prefab,
                worldPosition,
                Quaternion.identity
            );

        FurnitureInstance instance =
            furnitureObject.GetComponent<FurnitureInstance>();

        if (instance == null)
        {
            Debug.LogError(
                $"Prefab '{definition.Prefab.name}' " +
                "does not have a FurnitureInstance component."
            );

            Destroy(furnitureObject);
            return null;
        }

        instance.Initialise(
            definition,
            gridPosition
        );

        SetOccupiedCells(
            gridPosition,
            definition.Size,
            true
        );

        return instance;
    }

    private void SetOccupiedCells(
        Vector2Int origin,
        Vector2Int size,
        bool occupied)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int position =
                    origin + new Vector2Int(x, y);

                RoomCell cell =
                    gridManager.GetCell(position);

                if (cell != null)
                    cell.SetOccupied(occupied);
            }
        }
    }
}