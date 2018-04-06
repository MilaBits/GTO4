using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class CivAI : MonoBehaviour {
    public int MaxCars;
    public List<GridTile> DangerZones;

    [Range(0, 1)] public float SpawnChance;

    public UnitFactory unitFactory;
    public TileGrid grid;

    public TurnManager _turnManager;
    public EventLog _eventLog;
    private Player _player;
    private Selection _selection;

    private void Start() {
        _player = GetComponent<Player>();
        _selection = GetComponent<Selection>();
        _turnManager.StartTurn.AddListener(Think);

        PickNextLocation();
    }

    private void Think() {
        SpawnCars();

        if (DangerZones.Count < MaxCars) {
            PickNextLocation();
        }

        gameObject.SetActive(false);
    }

    public void PickNextLocation() {
        GridTile tile = grid.GetRandomTile(true);
        DangerZones.Add(tile);
        tile.MarkSelected(true, _selection.selectedMaterial);
    }

    public void SpawnCars() {
        int carsEntering = 0;

        for (var i = 0; i < DangerZones.Count; i++) {
            GridTile dangerZone = DangerZones[i];
            float chance = Random.Range(0.0f, 1.0f);
            if (chance <= SpawnChance) {
                _selection.selected = dangerZone;
                unitFactory.SpawnUnit(dangerZone);
                dangerZone.MarkSelected(false, _selection.selectedMaterial);
                DangerZones.RemoveAt(i);
                carsEntering++;
            }
        }

        if (carsEntering > 0) {
            _eventLog.Log(String.Format("{0} civilian(s) enter the field", carsEntering), _player);
            PopupController.CreateSlidingPopup("Incoming Civilians", _player.logColor);
        }
    }
}