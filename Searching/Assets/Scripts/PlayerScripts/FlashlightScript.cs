using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] Light flashlight;
    [SerializeField] BatteryScript batteryScript;
    [SerializeField] float minTime, maxTime, timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryScript.flashlightBattery > 5f)
        {
            StartCoroutine(FlickerFlashlight());
        }
    }

    IEnumerator FlickerFlashlight()
    {
        if (timer > 0)
        {
            flashlight.intensity = 10f;
            timer -= Time.deltaTime;
            yield return null;
        }

        if (timer <= 0)
        {
            timer = Random.Range(minTime, maxTime);
            flashlight.intensity = Random.Range(1, 3);
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
}
