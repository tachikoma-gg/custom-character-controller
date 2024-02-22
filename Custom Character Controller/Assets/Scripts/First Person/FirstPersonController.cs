using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float gravity;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject playerCamera;

    [SerializeField] private int groundLayer;

    private bool grounded;
    private float velocityY;

    private float mouseX;
    private float mouseY;

    [SerializeField] private float verticalMin;
    [SerializeField] private float verticalMax;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpVelocity;

    [SerializeField] private Vector3 offset;

    void Update()
    {
        RotatePlayer();
        MovePlayer();
        Gravity();
    }

    void LateUpdate()
    {
        CameraFollow();
    }

    void RotatePlayer()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, verticalMin, verticalMax);

        playerCamera.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = playerCamera.transform.eulerAngles.y;
        float z = Input.GetAxis("Vertical");

        Jump();

        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2));

        Vector3 inputDirection = Quaternion.AngleAxis(y, Vector3.up) * new Vector3(x, 0, z).normalized * input;
        Vector3 verticalSpeed = new Vector3(0, velocityY, 0);
        Vector3 playerMove = inputDirection + verticalSpeed;

        characterController.Move(speed * Time.deltaTime * playerMove);
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
        if(collider.gameObject.layer == groundLayer)
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == groundLayer)
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

    void CameraFollow()
    {
        playerCamera.transform.position = transform.position + offset;
    }
}
