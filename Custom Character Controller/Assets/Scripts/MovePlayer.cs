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
    private Vector3 move;

    private float inertia = 15;
    // private float airModifier = 0.05f;


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

        // check for normal angle to walk down inclines.

        Ray rayDown = new Ray(transform.position, Vector3.down);
        Physics.Raycast(rayDown, out RaycastHit hitData, 0.25f);
        float normalY = hitData.normal.normalized.y;

        // calculate vertical velocity from gravity & jump
        velocity.y = (characterController.isGrounded && velocity.y <= 1) ? normalY : (velocity.y + gravity * Time.deltaTime);
        velocity.y = (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded) ? jump : velocity.y;

        if(cameraController.lockState)
        {
            // Combine input and velocity vectors
            move = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * input.normalized * speed;
        }
        else
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
            move = rotation * Vector3.forward * input.magnitude * speed;
        }

        float inertiaCurrent = characterController.isGrounded ? inertia : 0;

        velocity.x = Mathf.Lerp(velocity.x, move.x, inertiaCurrent * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, move.z, inertiaCurrent * Time.deltaTime);

        // move character
        characterController.Move(Time.deltaTime * velocity);
    }
}
