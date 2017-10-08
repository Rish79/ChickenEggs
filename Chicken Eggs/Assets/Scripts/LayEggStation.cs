using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayEggStation : MonoBehaviour
{
    public Transform eggSpawnPoint;
    public GameObject eggPrefab;
    private bool canLayEggs;
    private bool playerIsInNest;
    

	// Use this for initialization
	void Start ()
    {
        canLayEggs = false;
        playerIsInNest = false;
	}

    private void OnTriggerStay(Collider other)
    {
        PlayerInput playerInput = other.gameObject.GetComponent<PlayerInput>();

        if (playerInput.gameObject.tag == "Player")
        {
            playerIsInNest = true;
            StartCoroutine("StartEggLaying");

            if (canLayEggs)
            {
                Instantiate(eggPrefab, eggSpawnPoint.transform.position, eggSpawnPoint.transform.rotation);
                StopCoroutine("StartEggLaying");
                canLayEggs = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInput playerInput = other.gameObject.GetComponent<PlayerInput>();
        
        if(playerInput.gameObject.tag == "Player")
        {
            playerIsInNest = false;
            StopCoroutine("StartEggLaying");
        }
    }

    private IEnumerator StartEggLaying()
    {
        if(playerIsInNest)
        {
            Debug.Log("in egg laying");
            yield return new WaitForSeconds(3.5f);
            canLayEggs = true;
        }
    }
}
