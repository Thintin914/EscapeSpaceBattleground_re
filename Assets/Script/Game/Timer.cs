using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider sd;
    public int time;
    public SpawnPlayers sp;

    private TMPro.TextMeshProUGUI t;

    private void Start()
    {
        time = 80;
        sd =  GetComponent<Slider>();
        sd.value = time;
        sd.maxValue = time;
        t = transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        sp = transform.parent.GetComponent<SpawnPlayers>();
        StartCoroutine("Expand");
        InvokeRepeating("Counting", 0, 1f);
    }

    void Counting()
    {
        time--;
        sd.value = time;
        t.text = time.ToString();
        if (time == sd.minValue)
        {
            int highestScore = -1;
            int highestScorePlayer = 0;
            for(int i =0; i < 4; i++)
            {
                if(sp.playerScores[i] > highestScore)
                {
                    highestScore = sp.playerScores[i];
                    highestScorePlayer = i;
                }
            }
            highestScorePlayer ++;
            PlayerPrefs.SetInt("winner", highestScorePlayer);
            SceneManager.LoadScene("Win");
        }
    }

    IEnumerator Expand()
    {
        RectTransform sliderWidth = GetComponent<RectTransform>();

        sliderWidth.sizeDelta = new Vector2(0, 50);

        for(int i = 0; i < 16; i++)
        {
            sliderWidth.sizeDelta += new Vector2(50, 0);
            yield return new WaitForSeconds(0);
        }
    }
}
