using UnityEngine;

public class XPValueManager : MonoBehaviour
{
    public static XPValueManager Instance;

    [SerializeField] private GameObject floatingTextPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnFloatingText(int amount, Workstation workstation)
    {
        var go = Instantiate(floatingTextPrefab, this.transform);

        go.GetComponent<FloatingText>().Setup($"+{amount}XP");

        // Attach to this counter position
        var follow = go.GetComponent<WorldToUIFollow>();
        if (follow != null)
        {
            follow.SetTarget(workstation.gameObject.transform);
        }
    }
}
