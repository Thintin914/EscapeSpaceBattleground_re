using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("Despawn");
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
