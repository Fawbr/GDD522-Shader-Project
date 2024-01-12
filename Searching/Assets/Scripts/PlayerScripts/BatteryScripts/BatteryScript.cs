using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryScript : MonoBehaviour
{
    [SerializeField] public float flashlightBattery = 100f, maxFlashlightBattery = 100f;
    [SerializeField] public float thermalBattery = 30f, maxThermalBattery = 30f;
    [SerializeField] public float enemyBattery = 30f, maxEnemyBattery = 30f;
    [SerializeField] public float thermalBatteryChunk, enemyBatteryChunk, flashlightBatteryChunk;
    [SerializeField] public bool thermalBatteryDrain = false, enemyBatteryDrain = false, flashlightBatteryDrain = false;
    [SerializeField] CameraViews cameraViews;
    [SerializeField] Light flashlight;
    // Start is called before the first frame update
    void Start()
    {
        thermalBatteryChunk = maxThermalBattery / 3;
        enemyBatteryChunk = maxEnemyBattery / 3;
        flashlightBatteryChunk = maxFlashlightBattery / 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraViews.thermalEnabled && thermalBattery >= 0f)
        {
            thermalBattery -= Time.deltaTime;
            thermalBatteryDrain = true;

        }
        else
        {
            thermalBatteryDrain = false;
        }

        if (cameraViews.enemyViewEnabled && enemyBattery >= 0f)
        {
            enemyBattery -= Time.deltaTime;
            enemyBatteryDrain = true;
        }
        else
        {
            enemyBatteryDrain = false;
        }
        if (!cameraViews.enemyViewEnabled && !cameraViews.thermalEnabled && flashlightBattery >= 0f)
        {
            flashlightBattery -= Time.deltaTime;
            flashlightBatteryDrain = true;
        }
        else
        {
            flashlightBatteryDrain = false;
        }

        if (flashlightBattery <= 0f)
        {
            flashlight.enabled = false;
        }
    }
}
