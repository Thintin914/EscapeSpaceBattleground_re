using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickResetButton : MonoBehaviour
{
   void ResetTube()
    {
        if (transform.parent.GetComponent<CreateTube>().count == 4)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).name == "ControlImage(Clone)")
                {
                    Destroy(transform.parent.GetChild(i).gameObject);
                }
            }

            transform.parent.GetComponent<CreateTube>().DeleteAll();
            transform.parent.GetComponent<CreateTube>().StartCoroutine("Create");
        }
    }
}
