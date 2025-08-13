using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;


    private string idleTrigger = "idle";
    private string walkTrigger = "walk";

    private CharacterController controller;
   private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (move.magnitude > 0.1f)
        {
            animator.ResetTrigger(idleTrigger);
            animator.SetTrigger(walkTrigger);
        }
        else
        {
            animator.ResetTrigger(walkTrigger);
            animator.SetTrigger(idleTrigger);
        }
    }
}