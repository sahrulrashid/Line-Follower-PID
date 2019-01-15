using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID : MonoBehaviour {

    public enum Overshoot
    {
        Left, Right
    };

    [Header("Hardware")]
    public WheelEngine engine;
    public SensorChip sensorChip;

    [Header("PID")]
    public float setVelocity;
    public float Kp;
    public float Ki;
    public float Kd;
    public int errorWhenOverShoot = 60;
    public int sensorWeight = 15;
    public float breakOvershoot = 4.0f;

    private int error;
    private int prevError;
    private int prevCalcError;
    private int errorSum;
    private Overshoot overshoot;
    private Sensor[] sensors;

    void FixedUpdate () {
        sensors = sensorChip.GetSensors();
        StepPID();
	}


    private int CalcError()
    {
        int err = 0;
        int activeSensors = 0;
        int halfSensorsCount = sensors.Length / 2;

        for (int i = 0; i < sensors.Length; i++)
        {
            int state = sensors[i].GetState();
            err += state * (i - halfSensorsCount) * sensorWeight;
            activeSensors += state;
        }

        if (activeSensors != 0)
        {
            err /= activeSensors;
            prevCalcError = err;
        }
        else
        {
            if(overshoot == 0)
                engine.BreakWheels(breakOvershoot);

            if (prevCalcError < 0)
            {
                err = -errorWhenOverShoot;
                overshoot = Overshoot.Left;                // ustawienie flagi - przestrzelony, linia po lewej 
            }
            else
            {
                err = errorWhenOverShoot;
                overshoot = Overshoot.Right;                // ustawienie flagi - przestrzelony, linia po prawej 
            }
        }

        if (overshoot == Overshoot.Left && err >= 0)        // zerowanie flagi przestrzelenia zakrętu po powrocie na środek linii 
            overshoot = 0;
        else if (overshoot == Overshoot.Right && err <= 0)
            overshoot = 0;

        return err;
    }

    private void StepPID()
    {
        error = CalcError();
        float errorDerivative = error - prevError;
        errorSum += Mathf.Abs(error);
        float pid = Kp * error + Kd * errorDerivative;
        engine.SetEngineValues(setVelocity - pid, setVelocity + pid);
        prevError = error;
    }
}
