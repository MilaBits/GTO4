using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float Damage;
    public Player Owner;
    public ParticleSystem particleSystem;

    void Awake() {
        particleSystem = GameObject.Find("GameManager").GetComponent<ParticleSystem>();
    }

    void OnCollisionEnter(Collision col) {

        Debug.Log(col.gameObject.name);
        if (col.gameObject.transform.parent.GetComponent<Unit>() != null) {
            Unit hit = col.gameObject.transform.parent.GetComponent<Unit>();
            if (hit.owner == Owner) {
                return;
            }

            Debug.Log("Hit enemy!");
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.position = col.contacts.Last().point;
            particleSystem.Emit(emitParams, 50);
            hit.TakeDamage(Damage);

            Destroy(gameObject, 1f);
        }

    }
}