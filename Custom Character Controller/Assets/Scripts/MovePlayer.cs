using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private CharacterController controller;
    private GameObject cam;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private float gravity = -20f;
    private float velocity;

    float x;
    float z;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = FindObjectOfType<MoveCamera>().gameObject;
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(x, 0, z) * speed;

        velocity = (controller.isGrounded && velocity <= 1) ? -0.5f : velocity + gravity * Time.deltaTime;

        if(input.magnitude != 0)
        {
            float rotationY = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            float targetAngle = rotationY + cam.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
        
        velocity = (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded) ? jump : velocity;

        Vector3 move = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * input + new Vector3(0, velocity, 0);

        controller.Move(Time.deltaTime * move);
    }
}
