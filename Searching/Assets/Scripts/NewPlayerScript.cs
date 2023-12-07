using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    [Header("Input Readings")]
    [SerializeField] InputReader input;
    Vector2 RawMoveInput;

    [Header("Movment")]
    private CharacterController PlayerCC;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] private float JumpHeight = 10;

    //gravity
    private Vector3 velocity;
    public float gravity = -9.81f;


    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] public MouseLook mouseLook = new MouseLook();
 

    [Header("Ground Check")]
    [SerializeField] Transform GroundPos;
    [SerializeField] float GroundCheckRadius = 0.3f;
    [SerializeField] private LayerMask WhatisGround;

    [Header("Player Stats")]
    [SerializeField]
    private int Health = 100;


    #region Unity Callbacks

    private void Start()
    {
        input.MoveEvent += HandelDirctionalInput;
        input.JumpEvent += HandelJumpInput;
        mouseLook.Init(transform, cam.transform,input);
        PlayerCC = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerRotation();
        PlayerMovement();
    }

    #endregion

    #region PlayerRotation

    void PlayerRotation()
    {
        // PlayerRotation is just a call of mouseLook.LookRotation
        mouseLook.LookRotation(transform, cam.transform);
    }

    #endregion

    #region Movement

    private void PlayerMovement()
    {

        if (IsGrounded() && velocity.y < 0)
        {
            
          velocity.y = -2f;
        }

        Vector3 moveDir = transform.forward * RawMoveInput.y + transform.right * RawMoveInput.x;

        PlayerCC.Move(moveDir * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        PlayerCC.Move(velocity * Time.deltaTime);
    }

    #endregion

    #region Jump

    private void Jump()
    {
        if (IsGrounded())
        {
            velocity.y += Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    #endregion

    #region Checks

    public bool IsGrounded()
    {

        if (Physics.CheckSphere(GroundPos.position, GroundCheckRadius, WhatisGround))
        {
            return true;
        }
        return false;
    }


    #endregion

    #region HandelEvents

    private void HandelDirctionalInput(Vector2 dir)
    {
        
        RawMoveInput = dir;
    }

    private void HandelJumpInput()
    {
        Jump();
    }

    private void HandelPause()
    {
        input.ToggleActionMaps(input._customInputs.UI);

    }

    #endregion

    #region Interfaces
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        Debug.Log("health: " + Health);
    }

    #endregion

}
