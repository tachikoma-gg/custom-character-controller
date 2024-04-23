using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float rotationSpeed;   // Mouse sensitivity. How fast player can look around.
    [SerializeField] private Vector3 offset;        // Set to height of character's eyes.

    private GameObject playerCamera;
    private CharacterController characterController;

    private readonly float gravity = -9.8f;
    private Vector3 velocity;
    private Vector3 input;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        playerCamera = FindObjectOfType<Camera>().gameObject;
    }

    void Update()
    {
        // Rotate player & camera

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -89, 89);

        playerCamera.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);

        // Jump

        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity.y = jumpVelocity;
        }

        // Gravity

        if(!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Move player

        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        Quaternion direction = Quaternion.AngleAxis(playerCamera.transform.eulerAngles.y, Vector3.up);
        Vector3 playerMove = direction * input * speed + velocity;

        characterController.Move(Time.deltaTime * playerMove);
    }

    void LateUpdate()
    {
        playerCamera.transform.position = transform.position + offset;  // Move camera
    }
}
