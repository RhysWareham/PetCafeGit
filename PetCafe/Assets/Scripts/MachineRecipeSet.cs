using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Cooking/Machine Recipe Set")]
public class MachineRecipeSet : ScriptableObject
{
    public Categories.MachineType machineType; // "Oven", "Fryer" etc
    public List<FoodRecipe> recipes;
    public int initialUnlockLevel;

    //Hmm I also need to be able to specify how many extra machines can be built when unlocked
    //And when each machine type can be unlocked.
    //And like at level 5, you can get an extra fryer for example
}
