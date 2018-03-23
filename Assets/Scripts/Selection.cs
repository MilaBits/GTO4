using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour {

    public GridTile previousSelected;
    public GridTile selected;
    public Player player;


    void Awake() {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetButtonDown("Fire1")) {
            GetClickedObject();
        }

        if (Input.GetButtonDown("Fire2")) {
            GetClickedObject();

            if (previousSelected.gameObject.GetComponent<GridTile>() && previousSelected.GetComponentInChildren<Unit>()) {
                Unit unit = previousSelected.GetComponentInChildren<Unit>();

                // Check if it's the owner's turn
                if (unit.owner.gameObject.activeSelf) {
                    unit.Move(selected.transform);
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
        Physics.Raycast(ray, out hit, maxDistance);

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
        selected.MarkSelected(true, player.playerSelection);
    }

    private void OnDisable() {
        selected.MarkSelected(false, player.playerSelection);
    }
}
