using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ThermalView : MonoBehaviour
{
    [Header("Post Processing Effects")]
    [SerializeField] Volume postProcessingVolume;
    [SerializeField] VolumeProfile postProfile;
    [SerializeField] Camera thermalCam;
    [SerializeField] Image staticImage;

    ColorAdjustments thermalHueShift;
    public bool thermalEnabled = false;

    private void Start()
    {
        postProcessingVolume.profile.TryGet(out thermalHueShift);
        thermalHueShift.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (thermalEnabled)
            {
                thermalEnabled = false;
                staticImage.enabled = false;
                thermalHueShift.active = false;
            }
            else
            {
                thermalEnabled = true;
                staticImage.enabled = true;
                thermalHueShift.active = true;
            }
        }
 
    }
}
