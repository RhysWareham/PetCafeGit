using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        bool isOverUI = EventSystem.current.IsPointerOverGameObject();

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        Machine clickedMachine = hit.collider
            ? hit.collider.GetComponent<Machine>()
            : null;

        // If on UI, don't change anything
        if (isOverUI)
        {
            return;
        }

        // If clicking an empty space, close menus
        if (clickedMachine == null)
        {
            UIManager.Instance.CloseAll();
            return;
        }

        // If clicking a current machine, do nothing
        if (clickedMachine == UIManager.Instance.CurrentMachine)
            return;

        // If clicking a new machine
        clickedMachine.Interact();
    }
}
