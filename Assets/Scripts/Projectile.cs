using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : Ownable {
    public float Damage;
    public ParticleSystem particleSystem;
    public bool damaged;

    void Awake() {
        particleSystem = GameObject.Find("GameManager").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision col) {
        if (damaged) return;
        Debug.Log(col.gameObject.name);
        if (col.gameObject.transform.parent.GetComponent<Unit>() != null) {
            Unit hit = col.gameObject.transform.parent.GetComponent<Unit>();
            if (owner == null || hit.owner == owner) {
                return;
            }

            Debug.Log("Hit enemy!");
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.position = col.contacts.Last().point;
            particleSystem.Emit(emitParams, 50);
            hit.TakeDamage(Damage);

            damaged = true;
            Destroy(gameObject);
        }
    }
}