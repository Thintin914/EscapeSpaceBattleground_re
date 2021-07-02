using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private SpawnPlayers sp;
    private Vector3 velocity;

    public List<Transform> targets;
    public float smoothTime = 0.5f;

    public Vector3 offset;

    private void Start()
    {
        sp = GameObject.Find("GameHandler").GetComponent<SpawnPlayers>();

        for(int i = 0; i < 4; i++)
        {
            if (sp.players[i] != null)
            {
                targets.Add(sp.players[i].transform);
                sp.players[i].GetComponent<HeadAttachment>().camIndex = i;
            }
        }
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        int firstIndex = GetFirstTarget();
        if (firstIndex != -1)
        {
            if (targets.Count == 1)
            {
                return targets[0].position;
            }

            var bounds = new Bounds(targets[firstIndex].position, Vector3.zero);

            for (int i = firstIndex; i < targets.Count; i++)
            {
                if (targets[i] != null)
                {
                    bounds.Encapsulate(targets[i].position);
                }
            }
            return bounds.center;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private int GetFirstTarget()
    {
        int temp = -1;
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null && temp == -1)
            {
                temp = i;
                return temp;
            }
        }
        return -1;
    }
}
