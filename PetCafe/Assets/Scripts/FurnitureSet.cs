using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Furniture/Furniture Set")]
public class FurnitureSet : ScriptableObject
{
    public GridLayer layer; // "FLOOR", "MACHINES"
    public Categories.FurnitureType furnitureType;
    public List<PlaceableData> placeables;
    public int initialUnlockLevel;

    //Hmm I also need to be able to specify how many extra machines can be built when unlocked
    //And when each machine type can be unlocked.
    //And like at level 5, you can get an extra fryer for example
}
