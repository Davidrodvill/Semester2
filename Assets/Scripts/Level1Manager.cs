using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level1Manager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile targetTile; // Set this directly in the Inspector now

    private HashSet<Vector3Int> tilesToChange; // Set of tile positions that need to be changed

    void Start()
    {
        InitializeTilesToChange();
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
        if (tilesToChange.Contains(tilePosition))
        {
            tilemap.SetTile(tilePosition, targetTile);
            tilesToChange.Remove(tilePosition);
            tilemap.RefreshTile(tilePosition);

            if (tilesToChange.Count == 0)
            {
                Debug.Log("All tiles changed! Level complete!");
                // Handle level completion here (e.g., show a message or load a new level)
            }
        }
    }
}