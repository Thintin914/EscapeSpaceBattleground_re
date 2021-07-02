using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketFly : MonoBehaviour
{
    public void Ready()
    {
        StartCoroutine("Fly");
    }

    IEnumerator Fly()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.position += Vector3.up * 5;
            yield return new WaitForSeconds(0);
        }
        Camera.main.transform.parent = null;
        for (int i = 0; i < 80; i++)
        {
            transform.position += Vector3.up * 5;
            yield return new WaitForSeconds(0);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
