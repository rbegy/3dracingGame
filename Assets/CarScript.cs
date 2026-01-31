using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    public float acc = 5000f;
    public float speed;
    public float slowDown = 5000f;
    public float brakeForce = 10000f;
    public float turnSpeed = 50.0f;


    public WheelCollider frontleftWheel;
    public WheelCollider frontrightWheel;
    public WheelCollider rearleftWheel;
    public WheelCollider rearrightWheel;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.1f;
        rb.angularDrag = 0.25f;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        speed = rb.velocity.magnitude * 2.237f;
    }

    void FixedUpdate()
    {
        float currentBrake = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            currentBrake = brakeForce;
        }
        else if (Mathf.Abs(verticalInput) < 0.1f)
        {
            currentBrake = slowDown;
        }


        float motor = verticalInput * acc;

        frontleftWheel.motorTorque = motor;
        frontrightWheel.motorTorque = motor;
        rearleftWheel.motorTorque = motor;
        rearrightWheel.motorTorque = motor;

        frontleftWheel.brakeTorque = currentBrake;
        frontrightWheel.brakeTorque = currentBrake;
        rearleftWheel.brakeTorque = currentBrake;
        rearrightWheel.brakeTorque = currentBrake;

        frontleftWheel.steerAngle = turnSpeed * horizontalInput;
        frontrightWheel.steerAngle = turnSpeed * horizontalInput;


    }
}
