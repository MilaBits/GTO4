using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour {

    public Unit Unit;
    public TileGrid Grid;
    public Player Owner;

    public int spawnX;
    public int spawnY;

    public List<ResourceCost> costs = new List<ResourceCost>();

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

        // Spawn unit
        Unit unit = Instantiate(Unit, Grid.GetTile(spawnX, spawnY).transform);
        unit.owner = Owner;
    }

    //Attempt at placing a unit where the player clicks
    //IEnumerator WaitForKeyDown(KeyCode keyCode) {
    //    while (!Input.GetKeyDown("Fire2"))
    //        Debug.Log("No click");
    //    yield return null;
    //}

    //public void SpawnUnitOnSelectedTile() {
    //    WaitForInput();

    //    //get mouse position in world
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    // Casts the ray and get the first game object hit
    //    Physics.Raycast(ray, out hit);

    //    if (hit.transform.GetComponent<GridTile>() != null) {
    //        GridTile tile = hit.transform.GetComponent<GridTile>();
    //        spawnX = tile.x;
    //        spawnY = tile.y;
    //        SpawnUnit();
    //    }
    //}

    //public IEnumerable WaitForInput() {
    //    yield return StartCoroutine(WaitForKeyDown(KeyCode.Mouse0));
    //}

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
