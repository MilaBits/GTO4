﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {
    public List<Player> Players;
    public Player Civ;

    public UnityEvent StartTurn;


    [SerializeField] private int currentPlayer = 0;

    public int Turn { get; private set; }

    // Use this for initialization
    void Start() {
        Turn = 1;
        foreach (Player player in Players.Skip(1)) {
            player.gameObject.SetActive(false);
        }
    }

    public void NextTurn() {
        Players[currentPlayer].gameObject.SetActive(false);

        currentPlayer++;

        if (currentPlayer >= Players.Count) {
            currentPlayer = 0;
        }

        Players[currentPlayer].gameObject.SetActive(true);
        Turn++;
        
        Civ.gameObject.SetActive(true);
        StartTurn.Invoke();
        PopupController.CreateSlidingPopup(String.Format("{0} Turn", Players[currentPlayer].name),Players[currentPlayer].logColor);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame() {
        Application.Quit();
    }
}