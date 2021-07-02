using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGhost : MonoBehaviour
{
    public int id, speed, speedLimit;
    public Rigidbody rb;

    private string front, back, left, right;
    private Vector3 movement;
    private int moveFrontBack, moveLeftRight;

    private void Start()
    {
        speed = 30;
        speedLimit = 120;
        setInput(id);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
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

        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(250, 1000, 400);
        }
    }

    void setInput(int id)
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
}
