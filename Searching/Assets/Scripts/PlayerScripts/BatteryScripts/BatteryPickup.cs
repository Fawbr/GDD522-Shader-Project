using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public int batteriesOwned = 0;
    [SerializeField] Camera playerCam;
    float time = 0f;
    bool disappear = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] CameraViews cameraView;
    [SerializeField] ObjectSpawning objectSpawning;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "Battery")
                {
                    batteriesOwned++;
                    hit.transform.gameObject.SetActive(false);
                    text.enabled = true;
                    objectSpawning.batteriesInArea--;
                }
            }
        }

        if (text.enabled)
        {
            text.text = "Batteries on hand: " + batteriesOwned.ToString();
            if (!disappear)
            {
                time += Time.deltaTime;
                text.alpha = (time / 2);
            }
            if (disappear)
            {
                time -= Time.deltaTime;
                text.alpha = (time / 2);
                if (text.alpha <= 0f)
                {
                    time = 0f;
                    disappear = false;
                    text.enabled = false;
                }
            }

            if (time >= 3f)
            {
                disappear = true;
            }
        }
    }
}
