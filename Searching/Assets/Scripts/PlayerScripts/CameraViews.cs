using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CameraViews : MonoBehaviour
{
    [Header("Post Processing Effects")]
    [SerializeField] Volume postProcessingVolumeThermal;
    [SerializeField] Volume postProcessingVolumeThermalBloom;
    [SerializeField] Volume postProcessingVolumeEnemy;
    [SerializeField] VolumeProfile postProfile;
    [SerializeField] Camera thermalCam;
    [SerializeField] Camera enemyCam;
    [SerializeField] Image staticImage;
    [SerializeField] BatteryScript batteryScript;
    ColorAdjustments thermalHueShift;
    ColorAdjustments enemyHueShift;
    Bloom thermalBloom;
    bool thermalSwitch = false;
    bool enemySwitch = false;
    public bool thermalEnabled = false;
    public bool enemyViewEnabled = false;
    
    private void Start()
    {
        postProcessingVolumeThermal.profile.TryGet(out thermalHueShift);
        postProcessingVolumeThermalBloom.profile.TryGet(out thermalBloom);
        postProcessingVolumeEnemy.profile.TryGet(out enemyHueShift);
        thermalHueShift.active = false;
        enemyHueShift.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && batteryScript.thermalBattery >= 0f)
        {
            if (enemyHueShift)
            {
                enemyViewEnabled = false;
                enemyHueShift.active = false;
                enemyCam.enabled = false;
            }
            if (thermalEnabled)
            {
                thermalCam.enabled = false;
                thermalEnabled = false;
                staticImage.enabled = false;
                thermalHueShift.active = false;
                thermalBloom.active = false;
            }
            else
            {
                thermalCam.enabled = true;
                thermalEnabled = true;
                staticImage.enabled = true;
                thermalHueShift.active = true;
                thermalBloom.active = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && batteryScript.enemyBattery >= 0f)
        {
            if (thermalEnabled)
            {
                thermalEnabled = false;
                thermalHueShift.active = false;
                thermalBloom.active = false;
                thermalCam.enabled = false;
            }

            if (enemyViewEnabled)
            {
                enemyCam.enabled = false;
                enemyViewEnabled = false;
                staticImage.enabled = false;
                enemyHueShift.active = false;
            }
            else
            {
                enemyCam.enabled = true;
                enemyViewEnabled = true;
                staticImage.enabled = true;
                enemyHueShift.active = true;
            }
        }

        if (batteryScript.thermalBattery <= 0f)
        {
            if (thermalSwitch == false)
            {
                thermalCam.enabled = false;
                thermalEnabled = false;
                staticImage.enabled = false;
                thermalHueShift.active = false;
                thermalBloom.active = false;
                thermalSwitch = true;
            }
        }

        if (batteryScript.enemyBattery <= 0f)
        {
            if (enemySwitch == false)
            {
                enemyCam.enabled = false;
                enemyViewEnabled = false;
                staticImage.enabled = false;
                enemyHueShift.active = false;
                enemySwitch = true;
            }
        }

    }
}
