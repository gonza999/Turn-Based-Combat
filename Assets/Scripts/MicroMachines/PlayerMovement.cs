using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float turbo;
    float originalTurbo;
    public Rigidbody2D rigidbody2D;
    public Transform forward;
    public GameObject humo;

    private void Start()
    {
        originalTurbo = turbo;
        turbo = 0;
        humo.SetActive(false);
    }
    private void FixedUpdate()
    {
        Movement();
        Turbo();
    }

    private void Turbo()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            turbo = originalTurbo;
        }
        else
        {
            turbo = 0;
        }
    }
    private void Movement()
    {
        rigidbody2D.velocity = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = forward.position;
            humo.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            humo.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 5);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,-5);
        }
    }
}
