using UnityEngine;

public class TouchManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);

                if (hit.collider.gameObject.GetComponent<Machine>())
                {
                    hit.collider.gameObject.GetComponent<Machine>().Interact();
                }
            }
        }
    }
}
