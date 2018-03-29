using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class Unit : Ownable {

    public float maxHealth;
    public float Health;
    public float movementSpeed = 1f;

    private bool move;
    private Transform target;

    private ParticleSystem particleSystem;
    private Animator animator;

    [HideInInspector]
    public Turret turret;

    public void Start() {
        if (GetComponent<Turret>() != null)
            turret = GetComponent<Turret>();

        particleSystem = GameObject.Find("GameManager").GetComponent<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();

        FixTeamColors();
    }

    public void TakeDamage(float damage) {
        if (Health > damage) {
            Health -= damage;
        }
        else {
            Die();
        }
    }

    private void Die() {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = gameObject.transform.position;
        emitParams.startSize = 15;
        particleSystem.Emit(emitParams, 50);
        //TODO: Maybe a fade out effect
        animator.SetTrigger("Crash");
        Destroy(gameObject, 5f);
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

            if (mats.FindIndex(m => m.name == "Car Color (Instance)") != -1) {
                mats[mats.FindIndex(m => m.name == "Car Color (Instance)")] = owner.playerMaterial;
            }

            renderer.materials = mats.ToArray();
        }
    }
}
