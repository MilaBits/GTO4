using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitFactory : MonoBehaviour {
    public Unit Unit;
    public TileGrid Grid;
    public Player Owner;

    [Space] public EventLog eventLog;

    [Space] public int spawnX;
    public int spawnY;

    [Space] public List<ResourceCost> costs = new List<ResourceCost>();


    public void SpawnUnit(GridTile tile) {
        spawnX = tile.x;
        spawnY = tile.y;
        SpawnUnit();
    }

    public void SpawnUnit() {
        // Check if there are enough resources
        foreach (ResourceCost resourceCost in costs) {
            if (!resourceCost.HasEnough()) {
                Debug.Log("Not enough " + resourceCost.ResourceType.name);
                return;
            }
        }

        // Pay resources
        foreach (ResourceCost resourceCost in costs) {
            resourceCost.Pay();
        }

        // Get player's selection
        if (Owner.GetComponent<Selection>() != null) {
            Selection selection = Owner.GetComponent<Selection>();
            spawnX = selection.selected.x;
            spawnY = selection.selected.y;
            Debug.Log("Spawning at: " + spawnX + "," + spawnY);
        }

        // Spawn unit
        Unit unit = Instantiate(Unit, Grid.GetTile(spawnX, spawnY).transform);
        unit.owner = Owner;
        if (!unit.name.Contains("Civ"))
            eventLog.Log(String.Format("{0} called in reinforcements!", Owner.name), Owner);
        unit.name = String.Format("{0} {1}", Owner.name, Unit.name);
    }

    [Serializable]
    public class ResourceCost {
        public Resource ResourceType;
        public int cost;

        public bool HasEnough() {
            return ResourceType.HasEnough(cost);
        }

        public void Pay() {
            ResourceType.RemoveAmount(cost);
        }
    }
}