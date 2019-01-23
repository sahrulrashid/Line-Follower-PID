using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    [SerializeField]
    private LayerMask lineMask;

    [SerializeField]
    private Material onMaterial;

    [SerializeField]
    private Material offMaterial;

    private int state;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10, lineMask))
        {
            state = 1;
            rend.material = onMaterial;
        }
        else
        {
            state = 0;
            rend.material = offMaterial;
        }
    }

    public int GetState()
    {
        return state;
    }
}
