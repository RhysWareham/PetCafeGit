using UnityEngine;

public class Counter : MonoBehaviour
{
    public bool counterOccupied { get; private set; }
    private int numOfMealsLeft = 0;

    private FoodRecipe foodOnCounter;
    [SerializeField] private float maxWidth = 1.5f;
    [SerializeField] private float maxHeight = 1.0f;
    [SerializeField] private SpriteRenderer foodSprite;
    [SerializeField] private GameObject highlight;

    public bool IsOccupied => counterOccupied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCounterHighlight(bool turnOn)
    {
        if (!counterOccupied)
        {
            highlight.SetActive(turnOn);
        }
        else if (!turnOn)
        {
            highlight.SetActive(turnOn);
        }
    }

    public void Interact()
    {
        if (MoveManager.Instance.IsMoving)
        {
            MoveManager.Instance.CompleteMove(this);
            return;
        }

        if (!counterOccupied)
            return;

        UIManager.Instance.OpenCounterMenu(this);
    }

    public void PlaceFood(FoodRecipe recipe)
    {
        foodOnCounter = recipe;
        counterOccupied = true;

        foodSprite.sprite = recipe.cookedSprite;
        FitSpriteToContainer(foodSprite);
        highlight.SetActive(false);
    }

    void FitSpriteToContainer(SpriteRenderer sr)
    {
        if (sr.sprite == null) return;

        //Bounds counterBounds = counterSprite.bounds;

        //float maxWidth = counterBounds.size.x * 0.8f;
        //float maxHeight = counterBounds.size.y * 0.5f;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = maxWidth / spriteSize.x;
        float scaleY = maxHeight / spriteSize.y;

        float scale = Mathf.Min(scaleX, scaleY);

        sr.transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void RemoveFood()
    {
        foodSprite.sprite = null;
        counterOccupied = false;
        foodOnCounter = null;
        numOfMealsLeft = 0;
    }
}
