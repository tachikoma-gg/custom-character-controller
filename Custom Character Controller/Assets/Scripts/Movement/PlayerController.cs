using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;

    
    [SerializeField] private CharacterController characterController;

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

        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            y = jumpVelocity;
        }

        Vector3 playerMove = new Vector3(x, y, z);

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
