using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    public int id;
    public SpawnPlayers sp;
    public WinPlayer wp;

    private void LateUpdate()
    {
        if (sp != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(sp.players[id - 1].transform.position);
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(wp.players[id - 1].transform.position);
        }
    }

}
