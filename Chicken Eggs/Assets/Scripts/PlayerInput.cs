using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float cappedVelocity = 2.5f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float sustainedJumpForce = 1000f;

    public int playerId;
    public bool hasAnEgg;
    public GameObject eggOnPlayer;


    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        hasAnEgg = false;
        eggOnPlayer.SetActive(false);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        CheckForMovement();
        CheckForJumping();
	}

    void CheckForMovement()
    {
        if(Input.GetAxisRaw("MoveHorizontal" + playerId) > 0)
        {
            if (playerId == 1 || playerId == 3)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Input.GetAxisRaw("MoveHorizontal" + playerId) * Time.deltaTime * -moveSpeed, /*Input.GetAxisRaw("MoveVertical" + playerId) * Time.deltaTime * moveSpeed*/ 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Input.GetAxisRaw("MoveHorizontal" + playerId) * Time.deltaTime * moveSpeed, 0, 0);
            }
        }

        if (Input.GetAxisRaw("MoveHorizontal" + playerId) < 0)
        {
            if(playerId == 1 || playerId == 3)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Input.GetAxisRaw("MoveHorizontal" + playerId) * Time.deltaTime * moveSpeed, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Input.GetAxisRaw("MoveHorizontal" + playerId) * Time.deltaTime * -moveSpeed, 0, 0);
            }           
        }
    }

    void CheckForJumping()
    {
        if (Input.GetButton("Jump" + playerId))
        {
            rb.velocity = new Vector3(0, Mathf.Clamp(rb.velocity.y, 0.0f, cappedVelocity), 0);
            rb.AddForce(new Vector3(0, sustainedJumpForce * Time.deltaTime, 0), ForceMode.Force);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("in egg trigger");
        if (other.gameObject.tag == "Eggs" && !hasAnEgg)
        {
            if (Input.GetButtonDown("PickUp" + playerId))
            {
                Debug.Log("In pick up egg");
                Destroy(other.gameObject);
                eggOnPlayer.SetActive(true);
                hasAnEgg = true;
            }
        }
    }
}
