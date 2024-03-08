using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    private Rigidbody rb;
    
    private Vector3 input;

    [SerializeField] private float moveForce;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float slidePower;

    private float speedMultiplier = 0;
    private readonly float forceMultiplier = 500f;

    [SerializeField] private LayerMask slideLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        
        Debug.Log(rb.velocity.magnitude);
    }

    void MovePlayer()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        rb.AddRelativeForce(moveForce * forceMultiplier * Time.deltaTime * input.normalized, ForceMode.Force);
    }

    void RotatePlayer()
    {
        float inputHorizontal = Input.GetAxis("Horizontal_2");
        transform.Rotate(inputHorizontal * rotateSpeed * Time.deltaTime * Vector3.up);
    }

    void OnCollisionStay(Collision collision)
    {
        // Slide Fast
        if(collision.gameObject.layer == slideLayer && Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier += slidePower;
            rb.AddForce(speedMultiplier * Time.deltaTime * rb.velocity);
        }
    }
}
