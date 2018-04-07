using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public Text nameLabel;
    public Color logColor;
    public Material playerMaterial;

    public Animator cover;
    public Text Text;

    public int UnitsLostLoseCondition = 2;
    public int UnitsLost = 0;

    void Start() {
        if (nameLabel != null)
            nameLabel.text = name;
    }

    public void CheckLoss() {
        if (UnitsLost >= UnitsLostLoseCondition) {
            cover.gameObject.SetActive(true);

            Text.text = String.Format("{0} Failed!");
        }
    }
}