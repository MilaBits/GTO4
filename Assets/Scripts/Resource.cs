using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{

    public int quantity;
    public int initialQuantity;
    public UnityEvent OnValueChanged = new UnityEvent();


    public void Awake()
    {
        quantity = initialQuantity;
    }

    public void AddAmount(int value)
    {
        quantity += value;

        OnValueChanged.Invoke();
    }

    public bool HasEnough(int value)
    {
        return quantity >= value;
    }

    public void RemoveAmount(int value) {
        quantity -= value;

        OnValueChanged.Invoke();
    }

    public void SetAmount(int value)
    {
        quantity = value;

        OnValueChanged.Invoke();
    }
}
