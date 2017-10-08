using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public int numberOfEggsInBasket;
    private GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerInput playerInput = other.gameObject.GetComponent<PlayerInput>();

        if (playerInput.hasAnEgg)
        {
            Debug.Log("Egg goes into basket");
            numberOfEggsInBasket++;
            playerInput.hasAnEgg = false;
            playerInput.eggOnPlayer.SetActive(false);
            gameManager.eggsCollected++;
        }
    }
}
