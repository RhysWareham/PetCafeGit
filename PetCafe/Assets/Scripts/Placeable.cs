using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField]
    private PlaceableData placeableData;

    private Vector2Int gridPosition;

    private int rotation;

    public PlaceableData Data => placeableData;

    public Vector2Int GridPosition
    {
        get => gridPosition;
        set => gridPosition = value;
    }

    public Vector2Int Size
    {
        get
        {
            if (rotation % 180 == 0)
                return placeableData.size;

            return new Vector2Int(
                placeableData.size.y,
                placeableData.size.x);
        }
    }

    public void Rotate()
    {
        rotation += 90;

        if (rotation >= 360)
            rotation = 0;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    //void OnMouseDown()
    //{
    //    if (!RoomEditorManager.Instance.IsEditing)
    //        return;

    //    RoomEditorManager.Instance.PickUpObject(this);
    //}
}