using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UItoPID : MonoBehaviour {

    [SerializeField]
    private PID pid;

    [SerializeField]
    private TMP_InputField Kp;

    [SerializeField]
    private TMP_InputField Ki;

    [SerializeField]
    private TMP_InputField Kd;

    [SerializeField]
    private TMP_InputField setValue;

    [SerializeField]
    private TMP_InputField overError;

    [SerializeField]
    private TMP_InputField overBreak;

    [SerializeField]
    private TMP_InputField maxEnginePower;

    private float startKp;
    private float startKi;
    private float startKd;
    private float startSetValue;
    private int startOverError;
    private float startOverBreak;
    private float startMaxEnginePower;

    void Start()
    {
        startKp = pid.Kp;
        startKi = pid.Ki;
        startKd = pid.Kd;
        startSetValue = pid.setVelocity;
        startOverError = pid.errorWhenOverShoot;
        startOverBreak = pid.breakOvershoot;
        startMaxEnginePower = pid.engine.maxPower;
        UpdateUI();
    }

    public void ResetPID()
    {
        pid.Kp = startKp;
        pid.Ki = startKi;
        pid.Kd = startKd;
        pid.setVelocity = startSetValue;
        pid.errorWhenOverShoot = startOverError;
        pid.breakOvershoot = startOverBreak;
        pid.engine.maxPower = startMaxEnginePower;
        UpdateUI();
    }

    public void UpdateUI()
    {
        Kp.text = pid.Kp.ToString();
        Ki.text = pid.Ki.ToString();
        Kd.text = pid.Kd.ToString();
        setValue.text = pid.setVelocity.ToString();
        overError.text = pid.errorWhenOverShoot.ToString();
        overBreak.text = pid.breakOvershoot.ToString();
        maxEnginePower.text = pid.engine.maxPower.ToString();
    }

    public void UpdatePID()
    {
        pid.Kp = float.Parse(Kp.text);
        pid.Ki = float.Parse(Ki.text);
        pid.Kd = float.Parse(Kd.text);
        pid.setVelocity = float.Parse(setValue.text);
        pid.errorWhenOverShoot = int.Parse(overError.text);
        pid.breakOvershoot = float.Parse(overBreak.text);
        pid.engine.maxPower = float.Parse(maxEnginePower.text);
    }

}
