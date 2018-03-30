using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatList : MonoBehaviour {
    public List<Material> Materials;

    public Material RandomMaterial() {
        return Materials[Random.Range(0, Materials.Count)];
    }
}