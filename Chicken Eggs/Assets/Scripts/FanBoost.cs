using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBoost : MonoBehaviour
{
    [SerializeField]
    private float upwardsForce = 10.0f;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0, upwardsForce, 0);
        }
    }
}
