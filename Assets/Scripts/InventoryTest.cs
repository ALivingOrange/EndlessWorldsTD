
/***************************************************************
* file: InventoryTest.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/16/2026
*
* purpose: Tests the resource inventory using
* Resource ScriptableObjects for Wood and Ore.
*
****************************************************************/

using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField]
    private Resource wood;

    [SerializeField]
    private Resource ore;

    public static ResourceInventory inventory;

    // function: Awake
    // purpose: Gets the ResourceInventory component.
    private void Awake()
    {
        inventory = GetComponent<ResourceInventory>();
    }

    // function: Start
    // purpose: Tests adding, checking, and spending resources.
    private void Start()
    {
        if (inventory == null || wood == null || ore == null)
        {
            Debug.LogError("Inventory test references are missing.");
            return;
        }

        inventory.addResource(wood, 20);
        inventory.addResource(ore, 15);

        Debug.Log("Wood after collecting: " +
            inventory.getResource(wood));

        Debug.Log("Ore after collecting: " +
            inventory.getResource(ore));

        bool crafted = inventory.spendResource(wood, 5);

        Debug.Log("Craft successful: " + crafted);

        Debug.Log("Wood remaining: " +
            inventory.getResource(wood));

        bool secondCraft = inventory.spendResource(ore, 20);

        Debug.Log("Second craft successful: " + secondCraft);

        Debug.Log("Ore remaining: " +
            inventory.getResource(ore));
    }
}
