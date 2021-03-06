﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Turret : MonoBehaviour {
    public GameObject turret;
    public Projectile projectile;
    public float projectileSpeed;
    public float MinDamage;
    public float MaxDamage;
    public bool Fired;

    private Unit unit;
    private Quaternion defaultRotation;

    private TurnManager turnManager;

    // Use this for initialization
    void Awake() {
        unit = GetComponent<Unit>();
        defaultRotation = transform.rotation;
        turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
    }

    public void Fire(Unit target) {
        Fired = true;
        if (target.owner != unit.owner) {
            turret.transform.LookAt(target.transform);
            turret.transform.Rotate(-90, 0, 0);

            StartCoroutine(SendProjectile(turret.transform.position, target.transform, projectileSpeed));
        }
    }

    IEnumerator SendProjectile(Vector3 startPosition, Transform target, float time) {
        Projectile shot = Instantiate(projectile);
        shot.owner = unit.owner;
        shot.Damage = Random.Range(MinDamage, MaxDamage);
        shot.transform.position = turret.transform.position;

        float startTime = Time.time;
        while (Time.time < startTime + time) {
            shot.transform.position = Vector3.Lerp(startPosition, target.position, (Time.time - startTime) / time);
            yield return null;
        }
        shot.transform.position = target.position;
    }
}
