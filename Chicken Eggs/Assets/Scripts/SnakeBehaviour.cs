using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;
    [SerializeField]
    private float timeToBreakBoard = 3.5f;
    public GameObject eggPrefab;

    public bool isMovingRight;
    public bool isMovingLeft;
    public bool gotToCenter;
    public bool snakeIsBlocked;

	void Start ()
    {
        isMovingLeft = false;
        gotToCenter = false;
        snakeIsBlocked = false;
	}

	void Update ()
    {
        if (!snakeIsBlocked)
        {   // snake only moves if not blocked by board
            if (transform.position.z > 0 && !gotToCenter)
            {
                transform.Translate(0, 0, Time.deltaTime * -moveSpeed);
                if (transform.position.z <= 0)
                {
                    gotToCenter = true;
                    isMovingRight = true;
                }
            }

            if (gotToCenter && isMovingRight)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.Translate(0, 0, Time.deltaTime * -moveSpeed);
            }

            if (gotToCenter && isMovingLeft)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.Translate(0, 0, Time.deltaTime * -moveSpeed);
            }
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
        if(other.gameObject.tag == "Player")
        {
            PlayerInput playerInput = other.gameObject.GetComponent<PlayerInput>();
            // cause them to drop their egg if carrying one
            //Debug.Log("contacted player");
            if(playerInput.hasAnEgg)
            {
                playerInput.hasAnEgg = false;
                playerInput.eggOnPlayer.SetActive(false);
                Instantiate(eggPrefab, playerInput.transform.position, playerInput.transform.rotation);
            }
            else if(!playerInput.hasAnEgg)
            {
                if (Input.GetButtonDown("Crow" + playerInput.playerId))
                {
                    snakeIsBlocked = true;
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.AddForce(0, 200, -200);
                    rb.useGravity = true;
                    Destroy(gameObject, 2.0f);
                }
            }
        }
        if(other.gameObject.tag == "Eggs")
        {
            // destroy egg i.e. eat it
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Board")
        {
            snakeIsBlocked = true;
            StartCoroutine("DestroyBoard");
            //Debug.Log("contacted board");
            Destroy(other.gameObject, timeToBreakBoard);
        }
    }

    private IEnumerator DestroyBoard()
    {
        yield return new WaitForSeconds(timeToBreakBoard);
        snakeIsBlocked = false;
        StopCoroutine("DestroyBoard");
    }
}
