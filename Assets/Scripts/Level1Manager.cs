using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level1Manager : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Tile> possibleTiles; // List of possible target tiles
    private Tile targetTile; // The randomly selected target tile

    private HashSet<Vector3Int> tilesToChange; // Set of tile positions that need to be changed

    void Start()
    {
        SelectRandomTargetTile();
        InitializeTilesToChange();
    }

    void SelectRandomTargetTile()
    {
        int randomIndex = Random.Range(0, possibleTiles.Count);
        targetTile = possibleTiles[randomIndex];
    }

    void InitializeTilesToChange()
    {
        tilesToChange = new HashSet<Vector3Int>();

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);
            if (tilemap.HasTile(localPlace))
            {
                tilesToChange.Add(localPlace);
            }
        }
    }

    public void PlayerChangedTile(Vector3Int tilePosition)
    {
        Debug.Log($"Trying to change tile at {tilePosition}");

        if (tilesToChange.Contains(tilePosition))
        {
            Debug.Log($"Tile at {tilePosition} is in the set and will be changed to the target tile.");
            tilemap.SetTile(tilePosition, targetTile);
            tilesToChange.Remove(tilePosition);
            tilemap.RefreshTile(tilePosition); // This updates the tile's visual appearance

            if (tilesToChange.Count == 0)
            {
                Debug.Log("All tiles changed! Level complete!");
                // Handle level completion here (e.g., show a message or load a new level)
            }
        }
        else
        {
            Debug.Log($"Tile at {tilePosition} is not in the set to be changed.");
        }
    }
}
