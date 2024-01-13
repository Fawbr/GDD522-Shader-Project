using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JumpscareScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera jumpscareCam;
    [SerializeField] Canvas UICanvas;
    SceneManager sceneManager;
    bool playerLost = false;
    [SerializeField] float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (playerLost == true)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            jumpscareCam.enabled = true;
            player.SetActive(false);
            UICanvas.enabled = false;
            playerLost = true;
        }
    }
}
