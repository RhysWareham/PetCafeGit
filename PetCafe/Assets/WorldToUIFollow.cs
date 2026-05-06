using UnityEngine;

public class WorldToUIFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 baseOffset;

    private RectTransform rectTransform;
    private Camera cam;

    public Vector3 Offset { get; set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;

        Offset = baseOffset;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            //gameObject.SetActive(false);
            return;
        }

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + Offset);

        // If behind camera, hide
        if (screenPos.z < 0)
        {
            //gameObject.SetActive(false);
            return;
        }

        rectTransform.position = screenPos;
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