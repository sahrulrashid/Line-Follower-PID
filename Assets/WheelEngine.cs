using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEngine : MonoBehaviour {

    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public float maxPower;

    void Update () {
		if(Input.GetKey(KeyCode.RightArrow))
        {
            rightWheel.brakeTorque = 0;
            rightWheel.motorTorque = maxPower;
        }
        else
        {
            rightWheel.brakeTorque = Mathf.Infinity;
            rightWheel.motorTorque = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftWheel.motorTorque = maxPower;
            leftWheel.brakeTorque = 0;
        }
        else
        {
            leftWheel.brakeTorque = Mathf.Infinity;
            leftWheel.motorTorque = 0;
        }
    }
}
