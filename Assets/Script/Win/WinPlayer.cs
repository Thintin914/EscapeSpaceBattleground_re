using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlayer : MonoBehaviour
{
    private int winner;
    public GameObject suit, audioPlayer;
    public TMPro.TextMeshProUGUI playerIndicator;
    private WinPlayer self;
    public GameObject[] players;

    private void Start()
    {
        GameObject audioCloner = Instantiate(audioPlayer);
        audioCloner.GetComponent<AudioPlayer>().index = 9;
        audioCloner.GetComponent<AudioPlayer>().vol = 0.5f;

        self = GetComponent<WinPlayer>();
        winner = PlayerPrefs.GetInt("winner");
        players = new GameObject[4];

        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("P"+i) == 1 && winner != i)
            {
                GameObject cloner = Instantiate(suit, new Vector3(250 + Random.Range(-100, 100), 800, 400 + Random.Range(-100, 100)), Quaternion.Euler(0, 0, 0));
                cloner.GetComponent<HeadAttachment>().id = i;
                cloner.GetComponent<HeadAttachment>().headId = PlayerPrefs.GetInt("P" + i + "Head");
                cloner.GetComponent<HeadAttachment>().isMovable = true;

                players[i - 1] = cloner;

                TMPro.TextMeshProUGUI textcloner = Instantiate(playerIndicator, Vector3.zero, Quaternion.Euler(0,0,0));
                textcloner.name = "P" + i + " Indicator";
                textcloner.text = "P" + i;
                textcloner.transform.parent = transform;
                textcloner.GetComponent<PlayerIndicator>().id = i;
                textcloner.GetComponent<PlayerIndicator>().wp = self;
            }
        }


        GameObject cloneWinner = Instantiate(suit, new Vector3(418,814,728), Quaternion.Euler(0, 0, 0));
        cloneWinner.GetComponent<HeadAttachment>().id = winner;
        cloneWinner.GetComponent<HeadAttachment>().headId = PlayerPrefs.GetInt("P" + winner + "Head");
        cloneWinner.GetComponent<HeadAttachment>().isMovable = false;

        players[winner - 1] = cloneWinner;

        TMPro.TextMeshProUGUI textWinner = Instantiate(playerIndicator, Vector3.zero, Quaternion.Euler(0, 0, 0));
        textWinner.name = "P" + winner + " Indicator";
        textWinner.text = "P" + winner;
        textWinner.transform.parent = transform;
        textWinner.GetComponent<PlayerIndicator>().id = winner;
        textWinner.GetComponent<PlayerIndicator>().wp = self;
    }
}
