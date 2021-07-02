using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFloating : MonoBehaviour
{
    private Transform parentPos;

    private void Start()
    {
        parentPos = transform.parent;
    }

    private void Update()
    {
        transform.position = new Vector3(parentPos.position.x, parentPos.position.y + Mathf.Sin(Time.time * 5) * 15, parentPos.position.z);
    }
}
