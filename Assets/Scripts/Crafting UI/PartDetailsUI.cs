
/***************************************************************
* file: PartDetailsUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/21/2026
*
* purpose: Controls the tower part details panel
* and displays the appropriate stat labels
* for bases, cores, and heads.
*
****************************************************************/

using TMPro;
using UnityEngine;

public class PartDetailsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject detailsPanel;

    [SerializeField]
    private TextMeshProUGUI detailsTitle;

    [SerializeField]
    private TextMeshProUGUI detailsStats;

    // function: Start
    // purpose: Hides the details panel when the game starts.
    private void Start()
    {
        closeDetails();
    }

    // function: showBaseDetails
    // purpose: Displays placeholder stats for a tower base.
    public void showBaseDetails(string partName)
    {
        showDetails(
            partName,
            "Health: TBD\n" +
            "Attack Range: TBD\n" +
            "Vision Angle: TBD"
        );
    }

    // function: showCoreDetails
    // purpose: Displays placeholder stats for a tower core.
    public void showCoreDetails(string partName)
    {
        showDetails(
            partName,
            "Damage: TBD\n" +
            "Fire Interval: TBD\n" +
            "Shots Per Burst: TBD"
        );
    }

    // function: showHeadDetails
    // purpose: Displays placeholder stats for a tower head.
    public void showHeadDetails(string partName)
    {
        showDetails(
            partName,
            "Projectile Count: TBD\n" +
            "Spread Angle: TBD\n" +
            "Turn Speed: TBD"
        );
    }

    // function: showDetails
    // purpose: Updates the details text and opens the panel.
    private void showDetails(string partName, string stats)
    {
        if (detailsTitle != null)
        {
            detailsTitle.text = partName;
        }

        if (detailsStats != null)
        {
            detailsStats.text = stats;
        }

        if (detailsPanel != null)
        {
            detailsPanel.SetActive(true);
        }
    }

    // function: closeDetails
    // purpose: Hides the tower part details panel.
    public void closeDetails()
    {
        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }
    }
}