using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Turret : MonoBehaviour
{
    public GameObject turret;
    public float Damage;

    private Unit unit;
    private Quaternion defaultRotation;

    // Use this for initialization
    void Awake ()
    {
        unit = GetComponent<Unit>();
        defaultRotation = transform.rotation;
    }

    public void Fire(Unit target)
    {

        //TODO: Turret rotation is incorrect
        if (target.owner != unit.owner)
        {
            turret.transform.LookAt(target.transform);
            Debug.Log("Bang bang bang!");
        }
        else
        {
            Debug.Log("Can't fire at your own team!");
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
