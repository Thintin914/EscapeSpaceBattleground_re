using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("WaitDelete");
    }


    private void LateUpdate()
    {
        transform.Rotate(0, 200 * Time.deltaTime, 0);
        transform.localScale = new Vector3(4 + Mathf.Sin(Time.time * 5) * 4, 4 + Mathf.Sin(Time.time * 5) * 4, 3000);
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
