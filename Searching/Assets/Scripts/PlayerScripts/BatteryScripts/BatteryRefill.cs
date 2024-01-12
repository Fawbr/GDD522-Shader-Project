using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryRefill : MonoBehaviour
{
    [SerializeField] BatteryPickup batteryPickup;
    [SerializeField] BatteryScript batteryScript;
    [SerializeField] TopBatteryAlpha topBattery;
    [SerializeField] MiddleBatteryAlpha middleBattery;
    [SerializeField] EndBatteryAlpha endBattery;
    // Update is called once per frame
    void Update()
    {
        if (batteryPickup.batteriesOwned >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                batteryScript.flashlightBattery = batteryScript.maxFlashlightBattery;
                batteryPickup.batteriesOwned -= 1;
                topBattery.flashlightNodeDrain = 1;
                middleBattery.flashlightNodeDrain = 1;
                endBattery.flashlightNodeDrain = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                batteryScript.thermalBattery = batteryScript.maxThermalBattery;
                batteryPickup.batteriesOwned -= 1;
                topBattery.thermalNodeDrain = 1;
                middleBattery.thermalNodeDrain = 1;
                endBattery.thermalNodeDrain = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                batteryScript.enemyBattery = batteryScript.maxEnemyBattery;
                batteryPickup.batteriesOwned -= 1;
                topBattery.enemyNodeDrain = 1;
                middleBattery.enemyNodeDrain = 1;
                endBattery.enemyNodeDrain = 1;
            }
        }
    }
}
