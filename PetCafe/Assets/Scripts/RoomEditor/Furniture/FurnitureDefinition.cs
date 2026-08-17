using UnityEngine;

[CreateAssetMenu(
    fileName = "FurnitureDefinition",
    menuName = "Room Editor/Furniture Definition"
)]
public class FurnitureDefinition : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string furnitureID;
    [SerializeField] private string displayName;

    [Header("Visual")]
    [SerializeField] private GameObject prefab;

    [Header("Grid")]
    [SerializeField] private Vector2Int size = Vector2Int.one;

    [Header("Rotation")]
    [SerializeField] private bool canRotate = true;

    public string FurnitureID => furnitureID;
    public string DisplayName => displayName;
    public GameObject Prefab => prefab;
    public Vector2Int Size => size;
    public bool CanRotate => canRotate;
}