using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    public float acc = 5000f;
    public float speed;
    public float motor;
    public float slowDown = 5000f;
    public float brakeForce = 10000f;
    public float turnSpeed = 50.0f;


    public WheelCollider frontleftWheel;
    public WheelCollider frontrightWheel;
    public WheelCollider rearleftWheel;
    public WheelCollider rearrightWheel;

    public Text speedText;


    public InputActionReference steering;
    public InputActionReference throttle;
    public InputActionReference brake;
    public InputActionReference gearChange;

    private float horizontalInput;
    private float verticalInput;
    private float brakeInput;
    private Rigidbody rb;
    public bool controller;
    public bool keyboard;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.1f;
        rb.angularDrag = 0.25f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        if(controller)
        {
            steering.action.Enable();
            throttle.action.Enable();
            brake.action.Enable();
            gearChange.action.Enable();
        }
        if(keyboard)
        {
            steering.action.Disable();
            throttle.action.Disable();
            brake.action.Disable();
            gearChange.action.Disable();
        }
    }


    void Update()
    {
        if (controller)
        {
            horizontalInput = steering.action.ReadValue<float>();
            float rawThrottle = throttle.action.ReadValue<float>();
            verticalInput = Mathf.Clamp01((rawThrottle + 1f) * 0.5f);
            float rawBrake = brake.action.ReadValue<float>();
            brakeInput = Mathf.Clamp01((1f - rawBrake) * 0.5f);
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }


        speedText.text = "Speed: " + verticalInput;
        Debug.Log(brakeInput);
        speed = rb.velocity.magnitude * 2.237f;
    }

    void FixedUpdate()
    {
        float currentBrake = 0f;
        motor = verticalInput * acc;

        if (controller)
        {
            if (brakeInput > 0.05f)
            {
                currentBrake = brakeInput * brakeForce;
                motor = 0f;
            }

        }

        if (keyboard)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z))
            {
                currentBrake = brakeForce;
                motor = 0f;
            }
        }

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
