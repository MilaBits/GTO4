using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class CivCar : Unit {
    public int LifeTime = 2;

    private void HandleTurn() {
        LifeTime--;
        if (LifeTime < 1) {
            
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.GetComponent<Unit>()) {
            Unit unit = other.transform.GetComponent<Unit>();
            unit.TakeDamage(20);
            Leave();
        }
    }
}