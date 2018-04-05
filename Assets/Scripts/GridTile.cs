using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {
    public int x;
    public int y;

    private MeshRenderer renderer;
    private Material originalMaterial;

    // Use this for initialization  
    void Awake() {
        renderer = GetComponentInChildren<MeshRenderer>();
        originalMaterial = renderer.material;
    }

    public bool isSelected(Material selectedMaterial) {
        if (renderer.material != selectedMaterial) {
            return true;
        }

        return false;
    }

    public void MarkSelected(bool selected, Material selectedMaterial) {
        if (selected) {
            renderer.material = selectedMaterial;
        }
        else {
            renderer.material = originalMaterial;
        }
    }

    public override string ToString() {
        return String.Format("Zone ({0},{1})", x, y);
    }
}