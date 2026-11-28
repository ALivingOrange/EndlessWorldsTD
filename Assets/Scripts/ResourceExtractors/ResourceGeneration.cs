/***************************************************************
*file: ResourceGeneration.cs
* author: J.Johnson
*class: CS 4700 – Game Development
*assignment: Endless Worlds TD
*date last modified: 11 / 21 / 2026
*
*purpose: The script for rescource generation from extractors.
*
****************************************************************/
using System.Collections;
using UnityEngine;

public class RescourceGeneration : MonoBehaviour
{
    [SerializeField]
    public float rate;
    public Resource type; 
    [SerializeField] private ResourceInventory inventory;
    void Start()
    {
        if (inventory == null || type == null || rate <= 0f)
        {
            Debug.LogError("Resource extractor is missing setup.");
            return;
        }
        // Starts loop via Coroutine
        StartCoroutine(Repetition());
    }

    IEnumerator Repetition()
    {
        while (true)
        {
            // Increments rescource type by 1 on loop
            yield return new WaitForSeconds(rate);
            inventory.addResource(type, 1);
            Debug.Log(this.name + "Tick!");
        }
    }

}
