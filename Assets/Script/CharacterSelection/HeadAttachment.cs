using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeadAttachment : MonoBehaviour
{
    public GameObject[] avaliableHeads;
    public GameObject headCloner;
    public Rigidbody rb;
    public int id, headId, camIndex;
    public SpawnPlayers sp;
    public bool isMovable;
    public Image img;

    private string front, back, left, right;
    private Vector3 movement;
    private int moveFrontBack, moveLeftRight;
    private int speed, speedLimit;
    private bool hasParent;
    private Renderer myRenderer;
    private HeadAttachment self;

    private void Start()
    {
        speed = 50;
        speedLimit = 150;
        setControlInput(id);
        rb = GetComponent<Rigidbody>();
        self = GetComponent<HeadAttachment>();
        headCloner = Instantiate(avaliableHeads[headId - 1], transform.position, Quaternion.identity);
        headCloner.transform.SetParent(transform);
        if (SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            hasParent = true;
            myRenderer = GetComponent<Renderer>();
        }
        if (SceneManager.GetActiveScene().name == "Win" && PlayerPrefs.GetInt("winner") == id)
        {
            StartCoroutine("Win");
        }
    }

    IEnumerator Win()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        for (int i = 0; i < 47; i++)
        {
            yield return new WaitForSeconds(0);
            transform.Rotate(0, 2, 0);
        }
        for (int i = 0; i < 220; i++)
        {
            yield return new WaitForSeconds(0);
            transform.position += transform.forward * 2.5f;
        }
        for (int i = 0; i < 47; i++)
        {
            yield return new WaitForSeconds(0);
            transform.Rotate(0, -2, 0);
        }
        Camera.main.transform.SetParent(transform);
        rb.isKinematic = true;
        transform.position = new Vector3(-165, 956.5f, 792);
        transform.SetParent(GameObject.Find("spinMap-v.2020").transform.GetChild(1));
        transform.parent.GetComponent<RocketFly>().Ready();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MakeSound(4);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 750, ForceMode.VelocityChange);
        }
        if (collision.gameObject.tag == "Gear")
        {
            switch (collision.gameObject.name)
            {
                case "Gear0":
                    sp.playerScores[id - 1] += 1;
                    break;
                case "Gear1":
                    sp.playerScores[id - 1] += 2;
                    break;
                case "Gear2":
                    sp.playerScores[id - 1] += 3;
                    break;
                case "Gear3":
                    GameObject cloner = Instantiate(sp.beam, transform.position, Quaternion.identity);
                    cloner.transform.SetParent(transform);
                    break;
                default:
                    break;

            }
            MakeSound(5);
            sp.scoreObjects[id - 1].SetScore();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ghost")
        {
            Instantiate(sp.shockwave, transform.position, Quaternion.identity);
            ControlGhost temp = collision.gameObject.GetComponent<ControlGhost>();

            if (sp.ghosts[id - 1] == null)
            {
                DisplayGhostImage();
                sp.SpawnGhost(id, camIndex);
                MakeSound(7, 0.2f);
            }
            sp.ghosts[temp.id - 1] = null;
            sp.players[temp.id - 1] = gameObject;
            id = temp.id;
            camIndex = temp.id - 1;
            setControlInput(id);
            Camera.main.GetComponent<CameraFollow>().targets[temp.id - 1] = transform;
            isMovable = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject temp = other.gameObject;

        switch (temp.tag) {
            case "Rocket":
                if (sp.ghosts[id - 1] == null)
                {
                    DisplayGhostImage();
                    rb.AddForce(Vector3.up * 500, ForceMode.VelocityChange);
                    sp.SpawnGhost(id, camIndex);
                    isMovable = false;
                }
                break;
            case "Beam":
                if (temp.transform.parent.gameObject != gameObject)
                {
                    rb.velocity += Vector3.up * 350;
                    MakeSound(2);
                    if (sp.playerScores[id - 1] - 1 > -1)
                    {
                        sp.playerScores[id - 1] = sp.playerScores[id - 1] - 1;
                        sp.scoreObjects[id - 1].SetScore();
                        Vector3 midpoint = new Vector3(GetMidPoint(transform.position.x, temp.transform.position.x), 100 + GetMidPoint(transform.position.y, temp.transform.position.y), GetMidPoint(transform.position.z, temp.transform.position.z));
                        Instantiate(sp.gears[0], midpoint, Quaternion.identity);
                    }
                }
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        if (isMovable)
        {
            moveFrontBack = 0;
            moveLeftRight = 0;
            if (Input.GetKey(front))
            {
                moveFrontBack = 1;
                rb.velocity += new Vector3(0, 0, 1) * speed;
                movement = new Vector3(moveLeftRight, 0, moveFrontBack);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.5F);
            }
            if (Input.GetKey(back))
            {
                moveFrontBack = -1;
                rb.velocity += new Vector3(0, 0, -1) * speed;
                movement = new Vector3(moveLeftRight, 0, moveFrontBack);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.5F);
            }
            if (Input.GetKey(left))
            {
                moveLeftRight = -1;
                rb.velocity += new Vector3(-1, 0, 0) * speed;
                movement = new Vector3(moveLeftRight, 0, moveFrontBack);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.5F);
            }
            if (Input.GetKey(right))
            {
                moveLeftRight = 1;
                rb.velocity += new Vector3(1, 0, 0) * speed;
                movement = new Vector3(moveLeftRight, 0, moveFrontBack);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.5F);
            }
        }

        if (rb.velocity.x >= speedLimit)
        {
            rb.velocity = new Vector3(speedLimit, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z >= speedLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedLimit);
        }
        if (rb.velocity.x <= speedLimit * -1)
        {
            rb.velocity = new Vector3(speedLimit * -1, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z <= speedLimit * -1)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedLimit * -1);
        }

        if (hasParent == true)
        {
            if (myRenderer.isVisible == false)
            {
                transform.parent.GetComponent<TubeBehaviour>().StartCoroutine("GoDown");
            }
        }

        if (transform.position.y <= 0)
        {
            if (sp.ghosts[id-1] == null)
            {
                DisplayGhostImage();
                sp.SpawnGhost(id, camIndex, gameObject);
                self.enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    void setControlInput(int id)
    {
        switch (id)
        {
            case 1:
                front = "w";
                back = "s";
                left = "a";
                right = "d";
                break;
            case 2:
                front = "up";
                back = "down";
                left = "left";
                right = "right";
                break;
            case 3:
                front = "i";
                back = "k";
                left = "j";
                right = "l";
                break;
            case 4:
                front = "[5]";
                back = "[2]";
                left = "[1]";
                right = "[3]";
                break;
            default:
                break;
        }
    }

    void DisplayGhostImage()
    {
        MakeSound(2);
        Image cloner = Instantiate(img, Camera.main.WorldToScreenPoint(transform.position), Quaternion.Euler(0, 0, 0));
        cloner.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    void MakeSound(int index)
    {
        if (sp != null)
        {
            sp.MakeSound(index);
        }
    }

    void MakeSound(int index, float vol)
    {
        if (sp != null)
        {
            sp.MakeSound(index, vol);
        }
    }

    float GetMidPoint(float a, float b)
    {
        return a + (b - a) / 2;
    }
}
