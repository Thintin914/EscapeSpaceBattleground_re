using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDeleteButton : MonoBehaviour
{
    public GameObject tube;

    public void DeleteCharacter()
    {
        foreach(Transform child in tube.transform)
        {
            PlayerPrefs.SetInt("P" + tube.GetComponent<TubeBehaviour>().id, 0);
            Destroy(child.gameObject);
        }
        Destroy(transform.parent.gameObject);
    }


}
