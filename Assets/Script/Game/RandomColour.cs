using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColour : MonoBehaviour
{
    public Material[] material;
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        int temp = Random.Range(0, material.Length);
        rend.sharedMaterial = material[temp];
    }


}
