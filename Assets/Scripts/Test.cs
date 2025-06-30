using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public Tilemap tilemap;
    private Dictionary<Vector3Int, FarmTile> tileData = new Dictionary<Vector3Int, FarmTile>();
    public Tile TileToChange;
    public EventSystem EventSystem;
    void Start()
    {
        // Инициализация логики по всем тайлам, где есть земля
        BoundsInt bounds = tilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tileData[pos] = new FarmTile { position = pos };
            }
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(new Vector3(worldPos.x, worldPos.y, 0));

            if (tileData.ContainsKey(cellPos))
            {
                FarmTile tile = tileData[cellPos];
                if (!tile.isOccupied)
                {
                    var tileSus = tilemap.GetTile(cellPos); 
                    /*tilemap.SetTile(cellPos,TileToChange);*/
                    /*// Логика посадки растения
                    tile.isOccupied = true;
                    tile.growthProgress = 0;*/
                    Debug.Log($"Посажено растение в тайле {cellPos}");
                }
            }
        }
    }
}
