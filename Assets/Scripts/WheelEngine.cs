using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEngine : MonoBehaviour {

    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public float maxTorque;
    private float leftPower;
    private float rightPower;
    private Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void SetEngineValues(float leftWheelPower, float rightWheelPower)
    {
        leftWheelPower = Mathf.Min(leftWheelPower, maxTorque);
        rightWheelPower = Mathf.Min(rightWheelPower, maxTorque);

        leftWheelPower = Mathf.Max(leftWheelPower, -maxTorque);
        rightWheelPower = Mathf.Max(rightWheelPower, -maxTorque);

        leftPower = leftWheelPower;
        rightPower = rightWheelPower;

        if (leftWheelPower == 0)
        {
            leftWheel.brakeTorque = Mathf.Infinity;
            leftWheel.motorTorque = 0;
        }
        else
        {
            leftWheel.motorTorque = leftWheelPower;
            leftWheel.brakeTorque = 0;
        }

        if(rightWheelPower == 0)
        {
            rightWheel.brakeTorque = Mathf.Infinity;
            rightWheel.motorTorque = 0;
        }
        else
        {
            rightWheel.motorTorque = rightWheelPower;
            rightWheel.brakeTorque = 0;
        }
    }

    public void StopWheels()
    {
        body.velocity = Vector3.zero;
    }

    public void BreakWheels(float mult)
    {
        body.velocity = body.velocity / mult;
    }
}
