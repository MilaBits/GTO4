using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Unit : Ownable {

    public float maxHealth;
    public float Health;
    public float movementSpeed = 1f;

    private bool move;
    private Transform target;

    [HideInInspector]
    public Turret turret;

    public void Start() {
        if (GetComponent<Turret>() != null)
            turret = GetComponent<Turret>();

        FixTeamColors();
    }

    public void Move(Transform target) {
        transform.parent = target.transform;
        StartCoroutine(MoveToPosition(transform.position, target.transform.position, movementSpeed));
    }

    IEnumerator MoveToPosition(Vector3 startPosition, Vector3 targetPosition, float time) {
        float startTime = Time.time;
        while (Time.time < startTime + time) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / time);
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void FixTeamColors() {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>()) {

            List<Material> mats = renderer.materials.ToList();
            mats[mats.FindIndex(m => m.name == "Car Color (Instance)")] = owner.playerMaterial;

            renderer.materials = mats.ToArray();

            Debug.Log("This error means it can't find the turret's team color material");
            //TODO: Fix later i guess.


            //Material mat = mats.First(m => m.name == "Car Color (Instance)");
            //Debug.Log("material found: " + mat.name);
            //mat = owner.playerMaterial;
            //Debug.Log("material changed: " + mat.name);
            //Debug.Log(renderer.materials[1].name);
        }
    }
}
