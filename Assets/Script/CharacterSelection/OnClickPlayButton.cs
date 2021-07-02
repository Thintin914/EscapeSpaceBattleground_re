using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickPlayButton : MonoBehaviour
{
    void Play()
    {
        if (transform.parent.GetComponent<CreateTube>().count == 4)
        {
            int temp = 0;
            for (int i = 1; i < 5; i++)
            {
                if (PlayerPrefs.GetInt("P" + i) == 1)
                {
                    temp++;
                }
            }

            if (temp >= 1)
            {
                SceneManager.LoadScene("Map1");
            }
        }
    }
}
