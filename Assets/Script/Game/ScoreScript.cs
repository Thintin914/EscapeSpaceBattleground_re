using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int id;
    public int convertToPlayer;

    private TMPro.TextMeshProUGUI myText;
    private SpawnPlayers sp;

    private void Start()
    {
        myText = GetComponent<TMPro.TextMeshProUGUI>();
        sp = transform.parent.GetComponent<SpawnPlayers>();
    }

    public void SetScore()
    {
        myText.text = "Player " + convertToPlayer + " - " + sp.playerScores[id - 1];
    }
}
