using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 6f;
    public float sprintSpeedMultiplier = 100f; // Facteur d'accélération lors de l'appui sur Shift
    public float jumpspeed = 8f;
    private Animator animator;
    public float gravity = 20f;
    public Camera mainCamera;

    private Vector3 moveD = Vector3.zero;
    CharacterController Cac;

    void Start()
    {
        Cac = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("run", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("walk", false);
        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!mainCamera.gameObject.activeSelf)
        {  
            float currentSpeed = speed; // Vitesse actuelle de l'agent

            if(animator.GetBool("run") == true)
            {
                animator.SetBool("isRunning", true);
            } else
            {
                animator.SetBool("isRunning", false);
            }

            if (animator.GetBool("walk") == true)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetKey(KeyCode.LeftShift)) // Si la touche Shift est enfoncée
            {
                animator.SetBool("run", true);
                currentSpeed *= sprintSpeedMultiplier; // Multiplier la vitesse par le facteur d'accélération
            }
            else
            {
                animator.SetBool("run", false);
            }

            if (Cac.isGrounded)
            {
                // Utiliser les touches "zqsd" pour le mouvement horizontal
                float horizontalInput = 0f;
                if (Input.GetKey(KeyCode.D))
                    horizontalInput += 1f;
                if (Input.GetKey(KeyCode.Q))
                    horizontalInput -= 1f;

                if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("walk", true);
                }
                else
                {
                    animator.SetBool("walk", false);
                }

                moveD = new Vector3(horizontalInput, 0, Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.Z) ? 1 : 0);
                moveD = transform.TransformDirection(moveD);
                moveD *= currentSpeed; // Utilisation de la vitesse actuelle

                if (Input.GetButton("Jump"))
                {
                    moveD.y = jumpspeed;
                }
            }

            moveD.y -= gravity * Time.deltaTime;
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * speed * 100);

            Cac.Move(moveD * Time.deltaTime);
        }
    }
}