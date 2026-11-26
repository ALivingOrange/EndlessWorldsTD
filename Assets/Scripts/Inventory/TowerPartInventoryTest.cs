
/***************************************************************
* file: TowerPartInventoryTest.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Temporarily tests adding and spending
* crafted tower parts in the inventory.
*
****************************************************************/

using UnityEngine;

public class TowerPartInventoryTest : MonoBehaviour
{
    [SerializeField]
    private TowerPartInventory towerPartInventory;

    [SerializeField]
    private TowerBaseData testBase;

    [SerializeField]
    private TowerCoreData testCore;

    [SerializeField]
    private TowerHeadData testHead;

    // function: Start
    // purpose: Adds sample tower parts to the inventory
    // and verifies that quantities update correctly.
    private void Start()
    {
        if (towerPartInventory == null)
        {
            Debug.LogWarning("Tower Part Inventory is not assigned.");
            return;
        }

        if (testBase != null)
        {
            towerPartInventory.addBase(testBase, 3);
        }

        if (testCore != null)
        {
            towerPartInventory.addCore(testCore, 2);
        }

        if (testHead != null)
        {
            towerPartInventory.addHead(testHead, 1);
        }
    }
}