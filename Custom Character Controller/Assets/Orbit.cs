using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject otherBody;
    [SerializeField] private Vector3 velocity;

    private float volume;
    private float _mass;
    public float mass
    {
        get
        {
            return _mass;
        }
    }

    private readonly float gravitationalConstant = 0.01f;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        volume = (4 / 3) * Mathf.PI * Mathf.Pow(radius, 2);
        _mass = volume;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.Normalize(otherBody.transform.position - transform.position);
        float distance = Vector3.Distance(otherBody.transform.position, transform.position);
        float force = gravitationalConstant * mass * otherBody.GetComponent<Orbit>().mass / Mathf.Pow(distance, 2);
        float accelleration = force / mass;

        velocity = direction * accelleration + velocity;
        Debug.Log(velocity.magnitude);
        controller.Move(velocity * Time.deltaTime);
    }
}
