using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorChip : MonoBehaviour {

    [SerializeField]
    private float sensorDistance;

    private Sensor[] sensors;
    
    void Awake()
    {
        sensors = GetComponentsInChildren<Sensor>();
    }

    void Start()
    {
        SetSensorDistance(sensorDistance);
    }

    public Sensor[] GetSensors()
    {
        return sensors;
    }

    public void SetSensorDistance(float dist)
    {
        int halfSensorsCount = sensors.Length / 2;

        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].transform.localPosition = new Vector3(sensorDistance * (i - halfSensorsCount), sensors[i].transform.localPosition.y, sensors[i].transform.localPosition.z);
        }
    }

}
