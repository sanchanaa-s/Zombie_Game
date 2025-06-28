using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float playerSpeed = 5f;
    public float gravity = -9.81f;

    [Header("References")]
    public CharacterController cC;
    private Vector3 velocity;

    void Start()
    {
        // Assign the CharacterController component if not already assigned
        if (cC == null)
        {
            cC = GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Rotate player toward movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Move the player
            Vector3 move = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(move.normalized * playerSpeed * Time.deltaTime);
        }

        // Apply gravity
        if (cC.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative to keep grounded
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);
    }
}
