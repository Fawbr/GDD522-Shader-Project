using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WinCondition : MonoBehaviour
{
    [SerializeField] InteractionScript interactionScript;
    [SerializeField] Image winScreen;
    [SerializeField] TextMeshProUGUI winText;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionScript.bodiesCollected == 5)
        {
            time += Time.deltaTime;
            winScreen.color = new Color(winScreen.color.r, winScreen.color.g, winScreen.color.b, time);
            winText.alpha = time;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
