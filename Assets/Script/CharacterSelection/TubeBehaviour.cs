using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TubeBehaviour : MonoBehaviour
{
    public int id, headID;
    public Image img, imgCloner;
    public GameObject suit, suitCloner, audioPlayer;

    private bool isReady;

    private string changer;

    private void Start()
    {
        setControlInput(id);
        StartCoroutine("GoUp");
    }

    private void FixedUpdate()
    {
        if (isReady == true)
        {
            if (Input.GetKey(changer))
            {
                isReady = false;
                StartCoroutine("GoDown");
            }

        }
    }

    IEnumerator GoUp()
    {
        for(int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(0, 6, 0);
            yield return new WaitForSeconds(0);
        }
        GameObject cloner = Instantiate(audioPlayer);
        cloner.GetComponent<AudioPlayer>().index = 4;
        cloner.GetComponent<AudioPlayer>().vol = 1;

        imgCloner = Instantiate(img);
        imgCloner.transform.SetParent(GameObject.Find("Canvas").transform);
        Vector2 viewport = Camera.main.WorldToViewportPoint(transform.position);
        viewport += new Vector2(0.5f, 1.5f);
        imgCloner.transform.position = viewport;
        imgCloner.rectTransform.anchorMin = viewport;
        imgCloner.rectTransform.anchorMax = viewport;
        imgCloner.GetComponent<ControlImages>().id = id;
        imgCloner.GetComponent<ControlImages>().parent = gameObject;

        suitCloner = Instantiate(suit, new Vector3(transform.position.x, transform.position.y + 50, transform.position.z), Quaternion.Euler(0, 0, 0));
        suitCloner.GetComponent<HeadAttachment>().id = id;
        suitCloner.transform.SetParent(gameObject.transform);
        suitCloner.GetComponent<HeadAttachment>().isMovable = true;
        headID = (int)Random.Range(1, 4.9f);
        suitCloner.GetComponent<HeadAttachment>().headId = headID;

        PlayerPrefs.SetInt("P" + id, 1);
        PlayerPrefs.SetInt("P" + id + "Head", headID);

        isReady = true;
    }

    IEnumerator GoDown()
    {
        if (imgCloner != null)
        {
            Destroy(imgCloner.gameObject);
            Destroy(suitCloner);
        }

        GameObject cloner = Instantiate(audioPlayer);
        cloner.GetComponent<AudioPlayer>().index = 2;
        cloner.GetComponent<AudioPlayer>().vol = 1;
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(0, -6, 0);
            yield return new WaitForSeconds(0);
        }
        StartCoroutine("GoUp");
    }

    void setControlInput(int id)
    {
        switch (id)
        {
            case 1:
                changer = "w";
                break;
            case 2:
                changer = "up";
                break;
            case 3:
                changer = "i";
                break;
            case 4:
                changer = "[5]";
                break;
            default:
                break;
        }
    }
}
