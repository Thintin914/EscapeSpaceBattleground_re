using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTube : MonoBehaviour
{
    public int count;
    public GameObject cloner, audioPlayer;
    public GameObject[] allTubes;

    private void Start()
    {
        GameObject cloner = Instantiate(audioPlayer);
        cloner.GetComponent<AudioPlayer>().index = 8;
        cloner.GetComponent<AudioPlayer>().vol = 1;

        allTubes = new GameObject[4];
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        count = 0;
        for (int i = 1; i < 5; i++)
        {
            GameObject tube = Instantiate(cloner, new Vector3(-300 + (i - 1) * 140, -150, -340), Quaternion.Euler(-90, 0, 150 + (i - 1) * 14));
            tube.GetComponent<TubeBehaviour>().id = i;
            tube.name = "Tube " + i;
            allTubes[i - 1] = tube;
            yield return new WaitForSeconds(0.3f);
            count++;
        }
    }

    public void DeleteAll()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("P" + i+1, 0);
            Destroy(allTubes[i]);
        }
    }
}
