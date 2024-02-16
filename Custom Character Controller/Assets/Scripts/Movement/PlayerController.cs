using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float rotateSpeed;

    private CharacterController characterController;
    private GameObject playerCamera;

    private bool grounded;
    private float velocityY;
    private readonly float gravity = -9.8f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = FindObjectOfType<CameraFollow>().gameObject;
    }

    void Update()
    {
        MovePlayer();
        Gravity();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = playerCamera.transform.eulerAngles.y;
        float z = Input.GetAxis("Vertical");

        RotatePlayer(x, y, z);
        Jump();

        // Magnitude of input (Pythagorean Theorem).
        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2));

        // Find vector of movement relative to camera.
        Vector3 inputDirection = Quaternion.Euler(0, y, 0) * new Vector3(x, 0, z).normalized * input;
        Vector3 verticalSpeed = new Vector3(0, velocityY, 0);
        Vector3 playerMove = inputDirection + verticalSpeed;

        // Move character.
        characterController.Move(playerMove * speed * Time.deltaTime);
    }

    void RotatePlayer(float x, float y, float z)
    {
        if(x != 0 || z != 0)
        {
            // Find angle of player input.
            float rotationY = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            
            // Sum input with camera rotation to make player input relative to camera.
            float targetAngle = rotationY += y;
            // Smoothly turn character in input direction.
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotateSpeed * Time.deltaTime);

            // Rotates character.
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            velocityY = jumpVelocity;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            velocityY = 0;
            grounded = true;
        }
    }

    void Gravity()
    {
        if(!grounded)
        {
            velocityY += gravity * Time.deltaTime;
        }
    }
}
