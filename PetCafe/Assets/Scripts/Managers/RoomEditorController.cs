//using UnityEngine;

//public class RoomEditorManager : MonoBehaviour
//{
//    public static RoomEditorManager Instance;

//    [SerializeField]
//    private Camera mainCamera;

//    [SerializeField]
//    private Transform placedItemParent;

//    private PlaceableObject currentObject;

//    private bool editing;

//    public bool IsEditing => editing;

//    void Awake()
//    {
//        Instance = this;
//    }

//    public void EnterEditMode()
//    {
//        editing = true;
//    }

//    public void ExitEditMode()
//    {
//        editing = false;

//        currentObject = null;
//    }

//    void Update()
//    {
//        if (!editing)
//            return;

//        if (currentObject == null)
//            return;

//        Vector3 screenPos;

//#if UNITY_ANDROID || UNITY_IOS

//        if(Input.touchCount==0)
//            return;

//        screenPos = Input.GetTouch(0).position;

//#else

//        screenPos = Input.mousePosition;

//#endif

//        screenPos.z = -mainCamera.transform.position.z;

//        Vector3 world =
//            mainCamera.ScreenToWorldPoint(screenPos);

//        Vector2Int grid =
//            GridManager.Instance.WorldToGrid(world);

//        currentObject.transform.position =
//            GridManager.Instance.GridToWorld(grid);

//        bool canPlace =
//            GridManager.Instance.CanPlace(grid, currentObject.Size);

//        SetPreviewColour(canPlace);
//    }

//    public void StartPlacing(PlaceableData placeableItem)
//    {
//        GameObject obj =
//            Instantiate(
//                placeableItem.prefab,
//                placedItemParent);

//        currentObject =
//            obj.GetComponent<PlaceableObject>();
//    }

//    public void PickUpObject(PlaceableObject obj)
//    {
//        GridManager.Instance.RemoveObject(obj);

//        currentObject = obj;
//    }

//    public void ConfirmPlacement()
//    {
//        if (currentObject == null)
//            return;

//        Vector2Int grid =
//            GridManager.Instance.WorldToGrid(
//                currentObject.transform.position);

//        if (!GridManager.Instance.CanPlace(grid, currentObject.Size))
//            return;

//        GridManager.Instance.PlaceObject(currentObject, grid);

//        SetPreviewColour(true);

//        currentObject = null;
//    }

//    public void CancelPlacement()
//    {
//        Destroy(currentObject.gameObject);

//        currentObject = null;
//    }

//    public void RotateCurrent()
//    {
//        if (currentObject == null)
//            return;

//        currentObject.Rotate();
//    }

//    void SetPreviewColour(bool valid)
//    {
//        SpriteRenderer[] renderers =
//            currentObject.GetComponentsInChildren<SpriteRenderer>();

//        foreach (var r in renderers)
//        {
//            r.color =
//                valid
//                ? Color.white
//                : new Color(1f, 0.3f, 0.3f, 0.8f);
//        }
//    }
//}