using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFlyUp : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("FlyUp");
    }

    IEnumerator FlyUp()
    {
        for(int i = 0; i < 200; i++)
        {
            transform.position += new Vector3(Mathf.Sin(Time.time * 15) * 5, 4, 0);
            yield return new WaitForSeconds(0);
        }
        Destroy(gameObject);
    }
}
