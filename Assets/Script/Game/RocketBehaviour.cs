using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public SpawnPlayers sp;

    private void Start()
    {
        sp.MakeSound(3);
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        sp.MakeSound((int)Random.Range(0,1.9f));
        Instantiate(sp.shockwave, transform.position, Quaternion.identity);

        int temp = (int)(Random.Range(0, sp.gears.Length - 0.1f));
        GameObject cloner = Instantiate(sp.gears[temp], transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        cloner.name = "Gear" + temp;

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
    }
}
