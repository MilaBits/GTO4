using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Resource))]
public class ResourceUI : MonoBehaviour {
    public Text label;
    public Text value;

    private Resource resource;
    
    void Start() {
        resource = GetComponent<Resource>();
        label.text = resource.name;
        
        resource.OnValueChanged.AddListener(UpdateUI);

        UpdateUI();
    }

    public void UpdateUI() {
        value.text = resource.quantity.ToString();
    }
}
