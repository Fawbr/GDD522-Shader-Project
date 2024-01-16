using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class JumpscareScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera jumpscareCam;
    [SerializeField] RawImage jumpscareBackground;
    [SerializeField] Canvas UICanvas;
    [SerializeField] RawImage jumpscareImage;
    [SerializeField] Text jumpscareText;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera playerCam;
    SceneManager sceneManager;
    EnemyPathfinding enemyPathfinding;
    bool playerLost = false;
    bool huntingJumpscareInitiated = false;
    [SerializeField] float resetTimer = 0f;
    [SerializeField] float jumpscareTimer = 0f;

    private void Start()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
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

    IEnumerator Jumpscare()
    {
        jumpscareText.enabled = true;
        jumpscareImage.enabled = true;
        yield return new WaitForSeconds(0.5f);
        jumpscareImage.enabled = false;
        jumpscareText.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            jumpscareBackground.enabled = true;
            jumpscareImage.enabled = true;
            jumpscareText.text = "GOT YOU";
            jumpscareText.enabled = true;
            player.SetActive(false);
            playerCam.gameObject.SetActive(true);
            UICanvas.enabled = false;
            playerLost = true;
        }
    }
}
