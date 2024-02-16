using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject playerCamera;

    private bool grounded;
    private float x, y, z;
    
    public float speed
    {
        get
        {
            return _speed;
        }
    }

    void Update()
    {
        MovePlayer();
        Gravity();
    }

    void MovePlayer()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        float cameraRotationY = playerCamera.transform.eulerAngles.y;

        if(x != 0 || z != 0)
        {
            float rotationY = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            
            float targetAngle = rotationY += cameraRotationY;
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotateSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            y = jumpVelocity;
        }

        float input = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2));
        Vector3 direction = new Vector3(x, 0, z);
        Vector3 jump = new Vector3(0, y, 0);
        Vector3 playerMove = Quaternion.Euler(0, cameraRotationY, 0) * direction.normalized * input + jump;

        characterController.Move(playerMove * _speed * Time.deltaTime);
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
            y = 0;
            grounded = true;
        }
    }

    void Gravity()
    {
        if(!grounded)
        {
            y -= gravity * Time.deltaTime;
        }
    }
}
