using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayEggStation : MonoBehaviour
{
    [SerializeField]
    private bool canLayEggs;
    [SerializeField]
    private bool playerIsInNest;

    public Transform eggSpawnPoint;
    public GameObject eggPrefab;
    public Slider slider;
    
	void Start ()
    {
        canLayEggs = false;
        playerIsInNest = false;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            playerIsInNest = true;
            if (playerIsInNest)
            {
                Debug.Log("in egg laying");
                if (Input.GetButton("Laying" + playerInput.playerId))
                {
                    slider.value += Time.deltaTime * playerInput.layingSpeed;
                    if (slider.value >= 1)
                    {
                        canLayEggs = true;
                        slider.value = 0;
                    }
                }
            }

            if (canLayEggs)
            {
                Instantiate(eggPrefab, eggSpawnPoint.transform.position, eggSpawnPoint.transform.rotation);
                canLayEggs = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            playerIsInNest = false;
            slider.value = 0;
        }
    }
}
