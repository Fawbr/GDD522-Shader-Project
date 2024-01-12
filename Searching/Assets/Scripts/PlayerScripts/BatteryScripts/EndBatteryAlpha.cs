using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndBatteryAlpha : MonoBehaviour
{
    Image batteryNode;
    [SerializeField] BatteryScript batteryScript;
    float timeThermalChunk, timeEnemyChunk, timeflashlightChunk;
    public float thermalNodeDrain, enemyNodeDrain, flashlightNodeDrain;
    private void Start()
    {
        batteryNode = GetComponent<Image>();
        timeThermalChunk = batteryScript.maxThermalBattery / 3;
        timeEnemyChunk = batteryScript.maxEnemyBattery / 3;
        timeflashlightChunk = batteryScript.maxFlashlightBattery / 3;
        thermalNodeDrain = 1;
        enemyNodeDrain = 1;
        flashlightNodeDrain = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryScript.thermalBatteryDrain)
        {
            UpdateBattery(0, batteryScript.thermalBattery);
        }
        if (batteryScript.enemyBatteryDrain)
        {
            UpdateBattery(1, batteryScript.enemyBattery);
        }
        if (batteryScript.flashlightBatteryDrain)
        {
            UpdateBattery(2, batteryScript.flashlightBattery);
        }
    }

    void UpdateBattery(int batteryChunkType, float currentBatteryValue)
    {
        if (batteryChunkType == 0)
        {
            if (currentBatteryValue <= batteryScript.thermalBatteryChunk * 1)
            {
                thermalNodeDrain -= (Time.deltaTime / timeThermalChunk);
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, thermalNodeDrain);
            }
            else if (currentBatteryValue <= 1)
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 0);
            }
            else
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 1);
            }
        }
        else if (batteryChunkType == 1)
        {
            if (currentBatteryValue <= batteryScript.enemyBatteryChunk * 1)
            {
                enemyNodeDrain -= (Time.deltaTime / timeEnemyChunk);
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, enemyNodeDrain);
            }
            else if (currentBatteryValue <= 1)
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 0);
            }
            else
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 1);
            }
        }
        else if (batteryChunkType == 2)
        {
            if (currentBatteryValue <= batteryScript.flashlightBatteryChunk * 1)
            {
                flashlightNodeDrain -= (Time.deltaTime / timeflashlightChunk);
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, flashlightNodeDrain);
            }
            else if (currentBatteryValue <= 1)
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 0);
            }
            else
            {
                batteryNode.color = new Color(batteryNode.color.r, batteryNode.color.g, batteryNode.color.b, 1);
            }
        }
    }
}
