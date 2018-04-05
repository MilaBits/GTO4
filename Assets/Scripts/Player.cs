using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Text nameLabel;
    public Color logColor;
    public Material playerMaterial;
    public Material playerSelection;

    void Awake() {
        if (nameLabel != null)
            nameLabel.text = name;
    }

    public void OnStartTurn() { }

    public void OnEndTurn() { }
}