using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    public int x;
    public int y;


    public Material selectedMaterial;
    private MeshRenderer renderer;
    private Material originalMaterial;

    // Use this for initialization  
    void Awake() {
        renderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = renderer.material;
    }

    // Update is called once per frame
    void Update() {

    }

    public void MarkSelected(bool selected) {
        if (selected) {
            renderer.material = selectedMaterial;
        }
        else {
            renderer.material = originalMaterial;
        }
    }
}
