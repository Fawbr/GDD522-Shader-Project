using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class JumpscareScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    NewPlayerScript newPlayerScript;
    CameraViews cameraViews;
    [SerializeField] Camera jumpscareCam;
    [SerializeField] RawImage jumpscareBackground;
    [SerializeField] Canvas UICanvas;
    [SerializeField] RawImage jumpscareImage;
    [SerializeField] Text jumpscareText;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera playerCam;
    [SerializeField] AudioSource staticSound;
    [SerializeField] AudioSource playerSound;
    SceneManager sceneManager;
    EnemyPathfinding enemyPathfinding;
    bool playerLost = false;
    bool huntingJumpscareInitiated = false;
    [SerializeField] float resetTimer = 0f;
    [SerializeField] float jumpscareTimer = 0f;

    private void Start()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        newPlayerScript = player.GetComponent<NewPlayerScript>();
        cameraViews = player.GetComponentInChildren<CameraViews>();
    }
    // Update is called once per frame
    void Update()
    {
        jumpscareTimer += Time.deltaTime;
        if (playerLost == true)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer > 3f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 100f)
            {
                if (Physics.SphereCast(player.transform.position, 1, player.transform.TransformDirection(Vector3.back), out RaycastHit hit) && hit.transform.name == "Enemy" && jumpscareTimer > Random.Range(15, 30))
                {
                    jumpscareTimer = 0f;
                    jumpscareText.text = "BEHIND YOU";
                    StartCoroutine("Jumpscare");
                }
            }

            if (enemyPathfinding.isWandering == false)
            {
                if (huntingJumpscareInitiated == false)
                {
                    huntingJumpscareInitiated = true;
                    jumpscareText.text = "RUN";
                    StartCoroutine("Jumpscare");
                }
            }
            else
            {
                huntingJumpscareInitiated = false;
            }
        }
    }

    IEnumerator Jumpscare()
    {
        jumpscareText.enabled = true;
        jumpscareImage.enabled = true;
        staticSound.Play();
        yield return new WaitForSeconds(0.5f);
        staticSound.Stop();
        jumpscareImage.enabled = false;
        jumpscareText.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            jumpscareBackground.enabled = true;
            jumpscareText.enabled = true;
            jumpscareImage.enabled = true;
            staticSound.Play();
            playerSound.enabled = false;
            jumpscareText.text = "GOT YOU";
            jumpscareText.enabled = true;
            cameraViews.enabled = false;
            newPlayerScript.enabled = false;
            playerLost = true;
        }
    }
}
