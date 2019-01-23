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
    public float setTorque;
    public float Kp;
    public float Ki;
    public float Kd;
    public int errorWhenOverShoot = 60;
    public int sensorWeight = 15;
    public float breakOvershoot = 4.0f;

    [Header("Statistics")]
    public float totalTime;
    public List<int> errorTable;
    public List<float> timeTable;

    private int error;
    private int prevError;
    private int prevCalcError;
    private float errorSum;
    private Overshoot overshoot;
    private Sensor[] sensors;

    void Start()
    {
        errorTable = new List<int>();
        timeTable = new List<float>();
    }

    void FixedUpdate () {
        sensors = sensorChip.GetSensors();
        StepPID();
        totalTime += Time.fixedDeltaTime;
        errorTable.Add(error);
        timeTable.Add(totalTime);
	}

    public void Reset()
    {
        engine.SetEngineValues(0.0f, 0.0f);
        engine.BreakWheels(Mathf.Infinity);
        prevError = 0;
        error = 0;
        prevCalcError = 0;
        errorSum = 0.0f;
        totalTime = 0.0f;
        errorTable = new List<int>();
        timeTable = new List<float>();
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
        float errorDerivative = (error - prevError)/Time.fixedDeltaTime;
        errorSum += error * Time.fixedDeltaTime;
        float pid = Kp * error + Ki * errorSum + Kd * errorDerivative;
        engine.SetEngineValues(setTorque - pid, setTorque + pid);
        prevError = error;
    }

}
