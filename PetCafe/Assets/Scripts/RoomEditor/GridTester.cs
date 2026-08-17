using UnityEngine;

public class GridTester : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [SerializeField] private Vector2Int testSize = new Vector2Int(2, 2);

    private void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );

        mouseWorldPosition.z = 0f;

        Vector2Int gridPosition =
            gridManager.WorldToGrid(mouseWorldPosition);

        gridPosition =
            new Vector2Int(
                gridPosition.x,
                gridPosition.y
            );

        bool canPlace =
            gridManager.PreviewPlacement(
                gridPosition,
                testSize
            );

        //gridPosition =
        //    gridManager.GetClampedGridPosition(gridPosition);

        //Vector2 snappedPosition =
        //    gridManager.GridToWorld(gridPosition);

        //transform.position = snappedPosition;

        Vector2 worldPosition =
            gridManager.GridToWorld(gridPosition);

        transform.position = worldPosition;
    }

    private void OnDisable()
    {
        if (gridManager != null)
            gridManager.ClearHighlights();
    }
}