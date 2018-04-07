using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {
    public GridTile previousSelected;
    public GridTile selected;
    public GridTile ActionSelected;
    public Player player;
    public LayerMask layerMask;

    public EventLog eventLog;

    public Material selectedMaterial;
    public Material ActionMaterial;

    void Awake() {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
        
        if (Input.GetButtonDown("Fire1") && GetClickedObject()) { //LMB
            if (selected != null)
                selected.MarkSelected(false, selectedMaterial);
            selected = GetClickedObject();
            selected.MarkSelected(true, selectedMaterial);
        }

        if (Input.GetButtonDown("Fire2")) { //RMB
            if (ActionSelected != null)
                ActionSelected.MarkSelected(false, ActionMaterial);
            ActionSelected = GetClickedObject();
            ActionSelected.MarkSelected(true, ActionMaterial);

            if (selected.gameObject.GetComponent<GridTile>() && selected.GetComponentInChildren<Unit>()) {
                Unit unit = selected.GetComponentInChildren<Unit>();

                // Check if it's the owner's turn
                if (unit.owner.gameObject.activeSelf) {
                    if (ActionSelected.gameObject.GetComponent<GridTile>() && ActionSelected.GetComponentInChildren<Unit>()) {
                        Unit target = ActionSelected.GetComponentInChildren<Unit>();

                        // Clicked an enemy, attack
                        if (unit.turret.Fired) {
                            eventLog.Log(String.Format("{0} already fired or just entered", unit.name));
                            return;
                        }

                        if (unit.owner == target.owner) {
                            eventLog.Log("No friendly fire!");
                            return;
                        }

                        eventLog.Log(String.Format("{0} fired at {1}", unit.name, target.name), player);
                        unit.turret.Fire(target);
                    }
                    else {
                        // Clicked an empty tile, move
                        if (unit.Moved) {
                            eventLog.Log(String.Format("{0} already moved or just entered", unit.name));
                            return;
                        }

                        unit.Move(ActionSelected.transform);
                        eventLog.Log(String.Format("{0} moved to {1}", unit.name, ActionSelected), player);
                        
                        // Correct selection (select car at it's new position)
                        selected.MarkSelected(false, selectedMaterial);
                        ActionSelected.MarkSelected(false, selectedMaterial);
                        selected = ActionSelected;
                        selected.MarkSelected(true, selectedMaterial);
                    }
                }
            }
        }
    }

    private GridTile GetClickedObject() {
        //get mouse position in world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        int maxDistance = 25;
        Physics.Raycast(ray, out hit, maxDistance, layerMask);

        if (hit.transform == null) {
            return null;
        }

        if (selected != null) {
            previousSelected = selected;
        }

        return hit.transform.gameObject.GetComponent<GridTile>();

//        selected = hit.transform.gameObject.GetComponent<GridTile>();
//
//        if (previousSelected != null)
//            previousSelected.MarkSelected(false, player.playerSelection);
//        selected.MarkSelected(true, player.playerSelection);
    }

    private void OnEnable() {
        if (selected != null)
            selected.MarkSelected(true, selectedMaterial);
    }

    private void OnDisable() {
        if (selected != null)
            selected.MarkSelected(false, selectedMaterial);
    }
}