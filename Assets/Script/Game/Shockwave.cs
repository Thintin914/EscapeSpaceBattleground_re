using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Expand");
    }

    IEnumerator Expand()
    {
        for(int i = 0; i < 10; i++)
        {
            transform.localScale += new Vector3(1, 1, 1);
            yield return new WaitForSeconds(0);
        }
        Destroy(gameObject);
    }

}
