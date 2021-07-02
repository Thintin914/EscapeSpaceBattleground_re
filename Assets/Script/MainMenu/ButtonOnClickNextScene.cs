using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOnClickNextScene : MonoBehaviour
{

    void NextScene()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    void Cancel()
    {
        Application.Quit();
    }
}
