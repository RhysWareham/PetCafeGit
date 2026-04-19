using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PreCookingUI recipeMenu;
    [SerializeField] private MidCookingUI midCookingMenu;
    [SerializeField] private PostCookingUI postCookingMenu;

    void Awake()
    {
        Instance = this;
    }

    public void OpenRecipeMenu(Machine machine)
    {
        CloseAll();

        recipeMenu.Open(machine);
    }

    public void OpenMidCookingMenu(Machine machine)
    {
        CloseAll();

        midCookingMenu.Open(machine);
    }

    public void OpenPostCookingMenu(Machine machine)
    {
        CloseAll();

        postCookingMenu.Open(machine);
    }

    public void CloseAll()
    {
        recipeMenu.HideMenu();
        midCookingMenu.HideMenu();
        postCookingMenu.HideMenu();
    }
}