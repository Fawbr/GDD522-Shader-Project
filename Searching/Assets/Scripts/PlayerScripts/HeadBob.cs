using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] bool enable = true;
    [SerializeField, Range(0, 0.1f)] public float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] public float frequency = 10f;

    [SerializeField] Transform cam = null;
    [SerializeField] Transform camHolder = null;

    NewPlayerScript playerMovement;

    float toggleSpeed = 1f;
    private Vector3 startPos;
    private CharacterController controller;
    public Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 15.0f;
        return pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<NewPlayerScript>();
        controller = GetComponent<CharacterController>();
        startPos = cam.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable)
        {
            return;
        }

        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
;    }

    void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (playerMovement.RawMoveInput == new Vector2(0,0))
        {
            return;
        }
        if (!controller.isGrounded)
        {
            return;
        }
        PlayMotion(FootStepMotion());
    }

    void ResetPosition()
    {
        if (cam.localPosition == startPos)
        {
            return;
        }
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, 1 * Time.deltaTime);
    }
    void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }
}
