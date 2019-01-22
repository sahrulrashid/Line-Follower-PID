using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

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

    private Vector3 startLineFollowerPosition;
    private Quaternion startLineFollowerRotation;

    bool wasSaved = false;

    void Start()
    {
        startLineFollowerPosition = pid.transform.position;
        startLineFollowerRotation = pid.transform.rotation;
        UpdateUI();
    }

    void FixedUpdate()
    {
        if(!wasSaved && pid.totalTime >= 60.0)
        {
            SaveToFile();
            wasSaved = true;
        }
    }

    public void StartPID()
    {
        pid.enabled = true;
    }

    public void ResetPID()
    {
        pid.transform.position = startLineFollowerPosition;
        pid.transform.rotation = startLineFollowerRotation;
        pid.Reset();
        pid.enabled = false;
        wasSaved = false;
        UpdateUI();
    }

    public void UpdateUI()
    {
        Kp.text = pid.Kp.ToString();
        Ki.text = pid.Ki.ToString();
        Kd.text = pid.Kd.ToString();
        setValue.text = pid.setTorque.ToString();
        overError.text = pid.errorWhenOverShoot.ToString();
        overBreak.text = pid.breakOvershoot.ToString();
        maxEnginePower.text = pid.engine.maxTorque.ToString();
    }

    public void UpdatePID()
    {
        pid.Kp = float.Parse(Kp.text);
        pid.Ki = float.Parse(Ki.text);
        pid.Kd = float.Parse(Kd.text);
        pid.setTorque = float.Parse(setValue.text);
        pid.errorWhenOverShoot = int.Parse(overError.text);
        pid.breakOvershoot = float.Parse(overBreak.text);
        pid.engine.maxTorque = float.Parse(maxEnginePower.text);
    }

    public void SaveToFile()
    {
        List<string> errors = new List<string>();
        foreach (float e in pid.errorTable)
            errors.Add(e.ToString());

        File.WriteAllLines(@"error.txt", errors.ToArray());

        List<string> times = new List<string>();
        foreach (float t in pid.timeTable)
            times.Add(t.ToString());

        File.WriteAllLines(@"time.txt", times.ToArray());

        string[] parameters = { pid.Kp.ToString(), pid.Ki.ToString(), pid.Kd.ToString() };

        File.WriteAllLines(@"parameters.txt", parameters.ToArray());
    }

}
