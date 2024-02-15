using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private bool _grounded = true;
    public bool grounded
    {
        get
        {
            return _grounded;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            _grounded = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 6)
        {
            _grounded = true;
        }
    }
}
