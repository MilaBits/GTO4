using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public GridTile previousSelected;
    public GridTile selected;
    public Player player;
    public LayerMask layerMask;

    void Awake() {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update() {

        //LMB
        if (Input.GetButtonDown("Fire1")) {
            GetClickedObject();
        }

        //RMB
        if (Input.GetButtonDown("Fire2")) {
            GetClickedObject();

            if (previousSelected.gameObject.GetComponent<GridTile>() && previousSelected.GetComponentInChildren<Unit>()) {
                Unit unit = previousSelected.GetComponentInChildren<Unit>();

                // Check if it's the owner's turn
                if (unit.owner.gameObject.activeSelf) {

                    if (selected.gameObject.GetComponent<GridTile>() && selected.GetComponentInChildren<Unit>()) {

                        // Clicked an enemy, attack
                        unit.turret.Fire(selected.GetComponentInChildren<Unit>());
                    }
                    else {

                        // Clicked an empty tile, move
                        unit.Move(selected.transform);
                    }
                }
            }
        }
    }

    private void GetClickedObject() {
        //get mouse position in world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        int maxDistance = 25;
        Physics.Raycast(ray, out hit, maxDistance, layerMask);

        if (hit.transform == null) {
            return;
        }

        if (selected != null) {
            previousSelected = selected;
        }

        selected = hit.transform.gameObject.GetComponent<GridTile>();

        if (previousSelected != null)
            previousSelected.MarkSelected(false, player.playerSelection);
        selected.MarkSelected(true, player.playerSelection);
    }

    private void OnEnable() {
        if (selected != null && player.playerSelection != null)
            selected.MarkSelected(true, player.playerSelection);
    }

    private void OnDisable() {
        if (selected != null && player.playerSelection != null)
            selected.MarkSelected(false, player.playerSelection);
    }
}
