using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private float runSpeed = 40f;
    
    float horizontalMove = 0f;
    bool jump = false;
    public bool isMoving = false;

    private void Update()
    {
        //To prevent player from moving while chatting
        if(DialogueManager.instance.sentences.Count == 0 && !Player.instance.isDead)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if(horizontalMove < -0.01f || horizontalMove > 0.01f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
            else
            {
                jump = false;
            }

            controller.Move(horizontalMove * Time.deltaTime, false, jump);
        }
    }

    public void JumpBack()
    {
        if(controller.m_FacingRight)
        {
            //Player is facing right so jump to left
            controller.Move(-3.5f * Time.deltaTime, false, true);
        }
        else
        {
            //Jump to right
            controller.Move(3.5f * Time.deltaTime, false, true);
        }
    }
}
