
/***************************************************************
* file: PartDetailsUI.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/25/2026
*
* purpose: Displays actual tower part attributes
* from the tower data ScriptableObjects.
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
    // purpose: Displays the selected base's attributes.
    public void showBaseDetails(TowerBaseData data)
    {
        if (data == null)
        {
            return;
        }

        string stats =
            "Health: " + data.health + "\n" +
            "Attack Range: " + data.attackRange + "\n" +
            "Vision Angle: " + data.visionAngle;

        showDetails(data.getDisplayName(), stats);
    }

    // function: showCoreDetails
    // purpose: Displays the current tower core modifiers and damage type.
    public void showCoreDetails(TowerCoreData data)
    {
        detailsPanel.SetActive(true);
        detailsTitle.text = data.getDisplayName();

        detailsStats.text =
            "Damage Modifier: " + data.damageModifier + "\n" +
            "Fire Interval Modifier: " + data.fireIntervalModifier + "\n" +
            "Area of Effect Modifier: " + data.areaOfEffectModifier + "\n" +
            "Damage Type: " + data.damageType;
    }

    // function: showHeadDetails
    // purpose: Displays the selected head's attributes.
    public void showHeadDetails(TowerHeadData data)
    {
        if (data == null)
        {
            return;
        }

        string stats =
            "Projectile Count: " + data.numOfProjectiles + "\n" +
            "Spread Angle: " + data.spreadAngle + "\n" +
            "Turn Speed: " + data.turnSpeed;

        showDetails(data.getDisplayName(), stats);
    }

    // function: showDetails
    // purpose: Updates and opens the details panel.
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
    // purpose: Hides the details panel.
    public void closeDetails()
    {
        if (detailsPanel != null)
        {
            detailsPanel.SetActive(false);
        }
    }
}