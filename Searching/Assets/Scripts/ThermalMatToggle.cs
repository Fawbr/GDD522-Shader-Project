using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ThermalMatToggle : MonoBehaviour
{
    [SerializeField] ThermalView thermalView;
    [SerializeField] Material thermalMat;
    [SerializeField] Material regularMat;

    // Update is called once per frame
    void Update()
    {
        if (thermalView.thermalEnabled)
        {
            gameObject.GetComponent<Renderer>().material = thermalMat;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = regularMat;
        }
    }
}
