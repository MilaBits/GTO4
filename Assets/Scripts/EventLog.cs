using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour {
    public GameObject log;
    public Text LogMessage;

    public List<Color> playerColors;
    private TurnManager turnManager;

    public Text Log(String message) {
        Text newMessage = Instantiate(LogMessage);
        newMessage.transform.SetParent(log.transform);

        message += String.Format(" [Turn {0}]", turnManager.Turn);
        newMessage.text = message;
        newMessage.name = message;

        return newMessage;
    }

    public void Log(String message, Player owner) {
        Log(message).color = owner.logColor;
    }

    // Use this for initialization
    void Start() {
        turnManager = GetComponent<TurnManager>();
    }
}