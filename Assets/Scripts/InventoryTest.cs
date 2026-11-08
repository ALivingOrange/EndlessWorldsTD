
/***************************************************************
* file: InventoryTest.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/7/2026
*
* purpose: Tests the ResourceInventory class by adding,
* checking, and spending resources.
*
****************************************************************/

using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    private ResourceInventory inventory;

    // function: Awake
    // purpose: Gets a reference to the ResourceInventory
    // component attached to this GameObject.
    private void Awake()
    {
        inventory = GetComponent<ResourceInventory>();
    }

    // function: Start
    // purpose: Tests adding, checking, and spending resources
    // when the game begins.
    private void Start()
    {
        inventory.addResource("Iron", 10);

        Debug.Log("Iron after collecting: " +
            inventory.getResource("Iron"));

        bool crafted = inventory.spendResource("Iron", 5);

        Debug.Log("Craft successful: " + crafted);

        Debug.Log("Iron remaining: " +
            inventory.getResource("Iron"));

        bool secondCraft = inventory.spendResource("Iron", 20);

        Debug.Log("Second craft successful: " + secondCraft);

        Debug.Log("Final iron amount: " +
            inventory.getResource("Iron"));
    }
}
