using UnityEngine;

public class WorldToUIFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            //gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + offset);

        // If behind camera, hide
        if (screenPos.z < 0)
        {
            //gameObject.SetActive(false);
            return;
        }

        transform.position = screenPos;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        //gameObject.SetActive(true);
    }

    public void ClearTarget()
    {
        target = null;
        //gameObject.SetActive(false);
    }
}