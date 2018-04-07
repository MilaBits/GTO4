using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitFactory : MonoBehaviour {
    public Unit Unit;
    public TileGrid Grid;
    public Player Owner;
    public Selection _selection;
    public EventLog _eventLog;
    public TurnManager _TurnManager;

    [Space] public int spawnX;
    public int spawnY;

    [Space] public List<ResourceCost> costs = new List<ResourceCost>();
    public bool Built;

    void Start() {
        _TurnManager.StartTurn.AddListener(TurnPrep);
    }
    
    public void SpawnUnit(GridTile tile) {
        spawnX = tile.x;
        spawnY = tile.y;
        SpawnUnit();
    }

    public void TurnPrep() {
        Built = false;
    }

    public void SpawnUnit() {
        if (Built) {
            _eventLog.Log("Already called reinforcements this turn");
            return;
        }

        // Get player's selection
        if (Owner.GetComponent<Selection>() && _selection.selected) {
            spawnX = _selection.selected.x;
            spawnY = _selection.selected.y;
        }
        else {
            _eventLog.Log("Select where to spawn your unit first");
            return;
        }

        // Check if the tile is empty
        if (Grid.GetTile(spawnX, spawnY).transform.childCount > 1 &&
            Owner != GameObject.Find("Civ").GetComponent<Player>()) {
            _eventLog.Log("Unable to spawn unit, tile is already occupied");
            return;
        }

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

        // Spawn unit
        Unit unit = Instantiate(Unit, Grid.GetTile(spawnX, spawnY).transform);
        unit.owner = Owner;
        if (!unit.name.Contains("Civ")) {
            _eventLog.Log(String.Format("{0} called in reinforcements!", Owner.name), Owner);
            PopupController.CreateSlidingPopup(String.Format("Incoming {0} {1}", Owner.name, Unit.name),
                Owner.logColor);
            Built = true;
        }

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