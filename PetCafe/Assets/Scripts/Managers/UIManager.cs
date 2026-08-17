using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private PreCookingUI recipeMenu;
    [SerializeField] private MidCookingUI midCookingMenu;
    [SerializeField] private PostCookingUI postCookingMenu;
    [SerializeField] private CounterUI counterMenu;

    public Machine currentMachine;
    public Counter currentCounter;

    public Machine CurrentMachine => currentMachine;
    public Counter CurrentCounter => currentCounter;

    void Awake()
    {
        Instance = this;
    }

    public void OpenRecipeMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        recipeMenu.Open(machine);
    }

    public void OpenMidCookingMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        midCookingMenu.Open(machine);
    }

    public void OpenPostCookingMenu(Machine machine)
    {
        CloseAll();

        currentMachine = machine;
        postCookingMenu.Open(machine);
    }

    public void OpenCounterMenu(Counter counter)
    {
        CloseAll();

        currentCounter = counter;
        counterMenu.Open(counter);
    }

    public void CloseAll()
    {
        currentMachine = null;
        currentCounter = null;

        recipeMenu.HideMenu();
        midCookingMenu.HideMenu();
        postCookingMenu.HideMenu();
        counterMenu.HideMenu();
    }
}