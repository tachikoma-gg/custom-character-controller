using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private CharacterController characterController;
    private GameObject cam;

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
        cam = FindObjectOfType<MoveCamera>().gameObject;
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        if(input.x != 0 || input.z != 0)                                      // only rotate if there's an input.
        {
            float direction = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;  // what angle is input towards.
            float targetAngle = direction + cam.transform.eulerAngles.y;      // account for direction camera is facing
            currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, smoothness * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, currentAngle, 0);         // rotate player to face direction of movement.
        }

        // calculate vertical velocity from gravity & jump
        velocity.y = (characterController.isGrounded && velocity.y <= 1) ? -0.5f : (velocity.y + gravity * Time.deltaTime);
        velocity.y = (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded) ? jump : velocity.y;

        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 move = rotation * Vector3.forward * input.magnitude * speed + velocity;    

        // move character 
        characterController.Move(Time.deltaTime * move);
    }
}
