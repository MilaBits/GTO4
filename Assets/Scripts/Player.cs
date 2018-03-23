using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Text nameLabel;
    public Material playerMaterial;
    public Material playerSelection;

    void Awake()
    {
        nameLabel.text = name;
    }
    
    public void OnStartTurn()
    {
    }

    public void OnEndTurn() {
    }
}
