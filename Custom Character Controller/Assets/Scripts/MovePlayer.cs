using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private CharacterController characterController;
    private GameObject playerCamera;
    private MoveCamera cameraController;


    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private float smoothness;
    [SerializeField] private float slideMultiplier;

    private readonly float gravity = -20f;

    private float currentAngle;

    private Vector3 velocity;
    private Vector3 input;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraController = FindObjectOfType<MoveCamera>();
        playerCamera = cameraController.gameObject;
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        // calculate vertical velocity from gravity & jump
        velocity.y = (characterController.isGrounded && velocity.y <= 1) ? -0.5f : (velocity.y + gravity * Time.deltaTime);
        velocity.y = (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded) ? jump : velocity.y;

        if(cameraController.lockState)
        {
            LockMove();
        }
        else
        {
            FreeMove();
        }
    }

    void FreeMove()
    {
        if(input.x != 0 || input.z != 0)
        {
            float direction = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;        // what angle is input towards.
            float targetAngle = direction + playerCamera.transform.eulerAngles.y;   // account for direction camera is facing
            currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, smoothness * Time.deltaTime);

            // rotate player to face direction of movement.
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);              
        }

        // Combine input and velocity vectors, correct for camera direction
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 move = rotation * Vector3.forward * input.magnitude * speed + velocity;    

        // move character 
        characterController.Move(Time.deltaTime * move);
    }

    void LockMove()
    {
        // Combine input and velocity vectors
        Vector3 move = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * input.normalized * speed + velocity;

        // move character
        characterController.Move(Time.deltaTime * move);
    }
}
