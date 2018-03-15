using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public List<Player> players;

    [SerializeField]
    private int currentPlayer = 0;

    // Use this for initialization
    void Start() {
        foreach (Player player in players.Skip(1)) {
            player.gameObject.SetActive(false);
        }
    }

    public void NextTurn() {

        players[currentPlayer].gameObject.SetActive(false);

        currentPlayer++;

        if (currentPlayer >= players.Count) {
            currentPlayer = 0;
        }

        players[currentPlayer].gameObject.SetActive(true);
    }

}
