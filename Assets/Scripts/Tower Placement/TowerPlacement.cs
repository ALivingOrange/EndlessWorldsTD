
/***************************************************************
* file: TowerPlacement.cs
* author: Dylan Diaz
* class: CS 4700 - Game Development
* assignment: Endless Worlds TD
* date last modified: 11/27/2026
*
* purpose: Handles selecting map tiles for tower placement.
*          Will later connect selected tiles to tower crafting,
*          inventory, and construction systems.
****************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private WorldGeneration worldGeneration;
    [SerializeField] private Camera mainCamera;
    private HashSet<WorldTiles> occupiedTiles = new HashSet<WorldTiles>();
    // function: Update
    // purpose: Detects mouse clicks and selects a world tile.
    private void Update()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        // Prevent clicking through UI elements.
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (worldGeneration == null || mainCamera == null)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Converts the mouse position to a world position.
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y,
            -mainCamera.transform.position.z));

        WorldTiles selectedTile =
            worldGeneration.GetTileAt(new Vector2(worldPosition.x, worldPosition.y));

        if (selectedTile == null)
        {
            Debug.Log("Clicked outside the map.");
            return;
        }


        if (!canPlaceTower(selectedTile))
        {
        Debug.Log("Cannot place tower here. Terrain: " +
                  selectedTile.terrainType);
        return;
        }

        Debug.Log("Valid tower placement tile: " +
            selectedTile.position +
            " | Terrain: " + selectedTile.terrainType);

    }

    // function: canPlaceTower
    // purpose: Checks whether a selected tile allows tower placement.
    private bool canPlaceTower(WorldTiles tile)
    {
        if (tile == null)
        {
            return false;
        }

        if (tile.terrainType == TerrainType.Water ||
            tile.terrainType == TerrainType.Cliff)
        {
            return false;
        }

        if (occupiedTiles.Contains(tile))
        {
            return false;
        }

        return true;
    }
}