using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpVelocity;

    private bool grounded;
    private float velocityY;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            grounded = false;
            velocityY = jumpVelocity;
        }

        if(!grounded)
        {
            velocityY += gravity * Time.deltaTime;
        }
        
        Vector3 vertical = new Vector3(0, velocityY, 0);

        Vector3 inputDirection = new Vector3(inputX, 0, inputZ); 
        Vector3 moveDirection = inputDirection + vertical;
        
        controller.Move(speed * Time.deltaTime * moveDirection);

        if(inputX != 0 || inputZ != 0)
        {
            float yRotation = Mathf.Atan2(inputX, inputZ) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            grounded = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            grounded = true;
            velocityY = 0;
        }
    }
}
