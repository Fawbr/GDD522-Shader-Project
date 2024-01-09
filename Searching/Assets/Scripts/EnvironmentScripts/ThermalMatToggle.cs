using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ThermalMatToggle : MonoBehaviour
{
    [SerializeField] CameraViews thermalView;
    SkinnedMeshRenderer visibilityToggle;
    private void Start()
    {
        thermalView = FindObjectOfType<CameraViews>();
        visibilityToggle = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thermalView.thermalEnabled)
        {
            visibilityToggle.enabled = true;
        }
        if (!thermalView.thermalEnabled)
        {
            visibilityToggle.enabled = false;
        }
    }
}
