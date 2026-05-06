using UnityEngine;

public class SaleValueManager : MonoBehaviour
{
    public static SaleValueManager Instance;

    [SerializeField] private GameObject floatingTextPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnFloatingText(int amount, Counter counter)
    {
        var go = Instantiate(floatingTextPrefab, this.transform);

        go.GetComponent<FloatingText>().Setup($"+£{amount}");

        // Attach to this counter position
        var follow = go.GetComponent<WorldToUIFollow>();
        if (follow != null)
        {
            follow.SetTarget(counter.gameObject.transform);
        }
    }
}
