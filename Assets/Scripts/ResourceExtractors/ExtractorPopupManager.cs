/***************************************************************
*file: ExtractorPopupManager.cs
*author: J. Johnson
*class: CS 4700 – Game Development
*assignment: Endless Worlds TD
*date last modified: 11/22/2026
*
*purpose: Script that manages the popup that's going to be used for the creation of every extractor.
*
****************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExtractorPopupManager : MonoBehaviour
{
    [Header("References")]
    public GameObject creationPrompt;
    public Button optionYes;
    public Button optionNo;

    [Header("Extractor Types")]
    public GameObject oreExtractor;
    public GameObject woodExtractor;

    private GameObject target;
    private string targetName;

    public static ExtractorPopupManager Popup;

    void Start()
    {
        if (creationPrompt != null)
        {
            creationPrompt.SetActive(false);
        }
        // Determines what occurs on click of each option
        optionYes.onClick.AddListener(BuildExtractor);
        optionNo.onClick.AddListener(CancelCreation);
    }

    public void Appear(GameObject selected)
    {
        // Code for the popup appearing and making sure the selected node is the one that is being the target
        target = selected;
        targetName = target.name;
        Debug.Log(target.name + " Selected!");
        if (creationPrompt != null)
        {
            creationPrompt.SetActive(true);
        }
    }

    void BuildExtractor()
    {
        if (creationPrompt != null)
        {
            // Add new extractor types here, just copy and past and follow format
            if (targetName.StartsWith("Ore Node"))
            {
                Debug.Log("Built Ore Extractor!");
                GameObject extractor = Instantiate(oreExtractor, target.transform.position, target.transform.rotation, target.transform.parent);
                Destroy(target);
            }
            else if (targetName.StartsWith("Wood Node"))
            {
                Debug.Log("Built Wood Extractor!");
                GameObject extractor = Instantiate(woodExtractor, target.transform.position, target.transform.rotation, target.transform.parent);
                Destroy(target);
            }
            creationPrompt.SetActive(false);
        }
    }

    void CancelCreation()
    {
        // Cancels prompt if no is clicked
        if (creationPrompt != null)
        {
            Debug.Log("Cancelled!");
            creationPrompt.SetActive(false);
        }
    }

    void Update()
    {

    }
}
