using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        // 1. If clicking UI
        if (UIUtility.IsPointerOverUIWithTag("MidCookingUI"))
            return;

        MidCookingUI.Instance.HideMenu();
        PostCookingUI.Instance.HideMenu();

        // 2. World click
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        Machine clickedMachine = hit.collider
            ? hit.collider.GetComponent<Machine>()
            : null;

        // 3. Same machine = do nothing
        if (clickedMachine == MidCookingUI.Instance.CurrentMachine)
            return;

        // Optional: open new one immediately
        if (clickedMachine != null)
        {
            clickedMachine.Interact();
        }
    }
}
