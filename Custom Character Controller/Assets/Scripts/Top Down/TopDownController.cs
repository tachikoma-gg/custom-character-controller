using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float gravity;

    [SerializeField] private CharacterController characterController;

    [SerializeField] private int groundLayer;

    private bool grounded;
    private float velocityY;

    void Update()
    {
        MovePlayer();
        Gravity();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        RotatePlayer(x, z);

        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2   ));

        Vector3 inputDirection = new Vector3(x, 0, z).normalized * input;
        Vector3 verticalSpeed = new(0, velocityY, 0);
        Vector3 playerMove = inputDirection + verticalSpeed;

        characterController.Move(speed * Time.deltaTime * playerMove);
    }

    void RotatePlayer(float x, float z)
    {
        if(x != 0 || z != 0)
        {
            float targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotateSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == groundLayer)
        {
            grounded = false;
        }
    }

    void OnTriggerStay(Collider collider)
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
}
