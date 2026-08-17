using UnityEngine;

public class FurnitureTester : MonoBehaviour
{
    [SerializeField] private FurnitureManager furnitureManager;
    [SerializeField] private FurnitureDefinition furnitureDefinition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mouseWorldPosition.z = 0f;

            Vector2Int gridPosition =
                furnitureManager.GridManager
                    .WorldToGrid(mouseWorldPosition);

            furnitureManager.PlaceFurniture(
                furnitureDefinition,
                gridPosition
            );
        }
    }
}