using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CivAI : MonoBehaviour {
    public int MaxCars;
    public List<GridTile> DangerZones;

    [Range(0, 1)] public float SpawnChance;

    public UnitFactory unitFactory;
    public TileGrid grid;

    public void Think() {
        if (DangerZones.Count < MaxCars) {
            PickNextLocation();
        }

        foreach (GridTile tile in DangerZones) {
            unitFactory.SpawnUnit(tile);
        }

        SpawnCars();
    }

    public void PickNextLocation() {
        GridTile newTile = null;
        while (newTile == null || newTile.transform.childCount > 0) {
            newTile = grid.GetTile(Random.Range(0, grid.GridWidth), Random.Range(0, grid.GridHeight));
        }

        DangerZones.Add(newTile);
        newTile.MarkSelected(true, GetComponent<Player>().playerSelection);
    }

    public void SpawnCars() {
        foreach (GridTile dangerZone in DangerZones) {
            if (Random.Range(0, 1) >= SpawnChance) {
                unitFactory.SpawnUnit(dangerZone);
            }
        }
    }
}