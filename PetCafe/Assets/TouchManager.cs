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

        Counter clickedCounter = hit.collider
            ? hit.collider.GetComponent<Counter>()
            : null;

        if (MoveManager.Instance.IsMoving)
        {
            // only counters should respond
            if (clickedCounter == null)
            {
                MoveManager.Instance.ExitMoveMode();
            }
            //return;
        }

        // If on UI, don't change anything
        if (isOverUI)
        {
            return;
        }

        // If clicking an empty space, close menus
        if (clickedMachine == null && clickedCounter == null)
        {
            UIManager.Instance.CloseAll();
            return;
        }

        // If clicking a current machine, do nothing
        if ((clickedMachine != null && clickedMachine == UIManager.Instance.CurrentMachine) || 
            (clickedCounter != null && clickedCounter == UIManager.Instance.currentCounter))
            return;

        // If clicking a new machine
        if (clickedMachine != null)
        {
            clickedMachine.Interact();
        }
        else if (clickedCounter != null)
        {
            clickedCounter.Interact();
        }
    }
}
