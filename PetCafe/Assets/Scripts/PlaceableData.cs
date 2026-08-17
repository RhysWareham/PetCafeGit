using UnityEngine;

[CreateAssetMenu(menuName = "Furniture/Placeable Data")]
public class PlaceableData : ScriptableObject
{
    public string itemName;

    public GameObject prefab;

    public Sprite icon;

    public Vector2Int size = Vector2Int.one;

    public int UnlockLevel;

    public GridLayer layer;

    public int price;
    public int numInInventory;
}