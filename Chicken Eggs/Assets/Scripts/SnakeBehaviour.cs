using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;
    [SerializeField]
    private float turnSpeed = 5.0f;

    public bool isMovingRight;
    public bool isMovingLeft;
    public bool gotToCenter;

	void Start ()
    {
        isMovingLeft = false;
        gotToCenter = false;
	}

	void Update ()
    {
		if(transform.position.z > 0 && !gotToCenter)
        {
            transform.Translate(0, 0, Time.deltaTime * -moveSpeed);
            if (transform.position.z <= 0)
            {
                gotToCenter = true;
                isMovingRight = true;
            }
        }

        if(gotToCenter && isMovingRight)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.Translate(0, 0, Time.deltaTime * -moveSpeed);                
        }

        if(gotToCenter && isMovingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Translate(0, 0, Time.deltaTime * -moveSpeed);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightWorldTrigger")
        {
            isMovingRight = false;
            isMovingLeft = true;   ;
        }
        if (other.gameObject.tag == "LeftWorldTrigger")
        {
            isMovingRight = true;
            isMovingLeft = false;        
        }
    }
}
