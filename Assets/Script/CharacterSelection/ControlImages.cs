using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlImages : MonoBehaviour
{
    public int id;
    public GameObject parent;
    public Sprite[] imgs;

    public Button deleteButton;

    private void Start()
    {
        transform.SetParent(GameObject.Find("TubeHandler").transform);
        GetComponent<Image>().sprite = imgs[id - 1];
        Button buttonCloner = Instantiate(deleteButton, transform.position, Quaternion.Euler(0, 0, 0));
        buttonCloner.transform.SetParent(gameObject.transform);
        buttonCloner.transform.position += new Vector3(-100, 0, 0);
        buttonCloner.GetComponent<OnClickDeleteButton>().tube = parent;
    }

}
