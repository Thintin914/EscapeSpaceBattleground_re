using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popin : MonoBehaviour
{
    private Vector3 originalScale, temp;

    private void Awake()
    {
        originalScale = transform.localScale;
        temp = new Vector3(0, 0, 0);
        transform.localScale = temp;
    }

    private void Start()
    {
        StartCoroutine("ZoomIn");
    }

    IEnumerator ZoomIn()
    {
        for(int i = 0; i< 15; i++)
        {
            temp += new Vector3(originalScale.x / 15, originalScale.y / 15, originalScale.z / 15);
            transform.localScale = temp;
            yield return new WaitForSeconds(0);
        }
    }
}
