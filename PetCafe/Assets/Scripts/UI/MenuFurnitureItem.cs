using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuFurnitureItem : MonoBehaviour
{
    [SerializeField] private Image furnitureSprite;
    private FurnitureManagerUI furnitureUI;
    private PlaceableData currentFurniture;
    private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private GameObject lockedSprite;
    [SerializeField] private TextMeshProUGUI unlockAtLevel;
    [SerializeField] private Button button;

    public void SetupMenuItem(FurnitureManagerUI _furnitureUI, PlaceableData furniture, int currentLevel)
    {
        furnitureUI = _furnitureUI;
        furnitureSprite.sprite = furniture.icon;
        currentFurniture = furniture;
        price.text = "£" + furniture.price.ToString();
        if (CurrencyManager.Instance.CurrentMoney < furniture.price)
        {
            price.color = Color.red;
        }

        if (currentLevel < furniture.UnlockLevel)
        {
            lockedSprite.SetActive(true);
            unlockAtLevel.text = $"UNLOCKS AT\nLV {furniture.UnlockLevel}";
            button.enabled = false;
        }
    }

    public void OnPressed()
    {
        if (CurrencyManager.Instance.SpendMoney(currentFurniture.price))
        {
            furnitureUI.SelectItem(currentFurniture);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
