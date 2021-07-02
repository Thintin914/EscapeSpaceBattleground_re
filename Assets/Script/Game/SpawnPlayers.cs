using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject suit, rocket, ghost, shockwave, audioplayer, beam;
    public GameObject[] players, ghosts, gears;
    public int[] playerScores;
    public ScoreScript[] scoreObjects;
    private SpawnPlayers self;

    public int totalPlayer;
    public TMPro.TextMeshProUGUI scoreDisplay, playerIndicator;

    private void Awake()
    {
        self = GetComponent<SpawnPlayers>();
        totalPlayer = 0;
        players = new GameObject[4];
        scoreObjects = new ScoreScript[4];
        ghosts = new GameObject[4];

        playerScores = new int[4];

        for(int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("P" + i) == 1)
            {
                GameObject cloner = Instantiate(suit, new Vector3(250 + Random.Range(-300,300), 1000 + i * 50, 400 + Random.Range(-300,300)), Quaternion.Euler(0, 0, 0));
                cloner.GetComponent<HeadAttachment>().id = i;
                cloner.GetComponent<HeadAttachment>().headId = PlayerPrefs.GetInt("P" + i + "Head");
                cloner.GetComponent<HeadAttachment>().sp = self;
                cloner.GetComponent<HeadAttachment>().isMovable = true;
                players[i-1] = cloner;

                TMPro.TextMeshProUGUI textCloner = Instantiate(scoreDisplay, Vector3.zero, Quaternion.Euler(0, 0, 0));
                textCloner.name = "P"+ (totalPlayer + 1) + " Score";
                textCloner.transform.parent = transform;
                textCloner.transform.position = new Vector2(Screen.width * 0.1f, Screen.height * 0.8f - (totalPlayer * 50));
                textCloner.text = "Player " + (totalPlayer + 1);
                textCloner.gameObject.GetComponent<ScoreScript>().id = i;
                textCloner.gameObject.GetComponent<ScoreScript>().convertToPlayer = totalPlayer + 1;

                scoreObjects[i-1] = textCloner.gameObject.GetComponent<ScoreScript>();

                textCloner = Instantiate(playerIndicator, Vector3.zero, Quaternion.Euler(0,0,0));
                textCloner.name = "P" + (totalPlayer + 1) + " Indicator";
                textCloner.text = "P" + (totalPlayer + 1);
                textCloner.transform.parent = transform;
                textCloner.gameObject.GetComponent<PlayerIndicator>().id = i;
                textCloner.gameObject.GetComponent<PlayerIndicator>().sp = self;

                totalPlayer++;
            }
        }
    }

    private void Start()
    {
        MakeSound(6, 0.5f);
        InvokeRepeating("SpawnRocket", 0, Random.Range(1f, 2.5f));
    }

    void SpawnRocket()
    {
        GameObject cloner = Instantiate(rocket, new Vector3(250 + Random.Range(-300, 300), 2000, 400 + Random.Range(-300, 300)), Quaternion.Euler(-180,0,0));
        cloner.GetComponent<RocketBehaviour>().sp = self;
    }

    public void SpawnGhost(int id, int index, GameObject parent)
    {
        if (ghosts[id - 1] == null)
        {
            GameObject cloner = Instantiate(ghost, new Vector3(250, 1000, 400), Quaternion.Euler(0, 0, 0));
            cloner.GetComponent<ControlGhost>().id = id;
            Camera.main.GetComponent<CameraFollow>().targets[index] = cloner.transform;
            ghosts[id - 1] = cloner;
            players[id - 1] = cloner;
            Destroy(parent);
        }
    }

    public void SpawnGhost(int id, int index)
    {
        if (ghosts[id-1] == null)
        {
            GameObject cloner = Instantiate(ghost, new Vector3(250, 1000, 400), Quaternion.Euler(0, 0, 0));
            cloner.GetComponent<ControlGhost>().id = id;
            Camera.main.GetComponent<CameraFollow>().targets[index] = cloner.transform;
            ghosts[id - 1] = cloner;
            players[id - 1] = cloner;
        }
    }

    public void MakeSound(int soundIndex)
    {
        GameObject cloner = Instantiate(audioplayer);
        cloner.GetComponent<AudioPlayer>().index = soundIndex;
        cloner.GetComponent<AudioPlayer>().vol = 1;
    }

    public void MakeSound(int soundIndex, float vol)
    {
        GameObject cloner = Instantiate(audioplayer);
        cloner.GetComponent<AudioPlayer>().index = soundIndex;
        cloner.GetComponent<AudioPlayer>().vol = vol;
    }
}
