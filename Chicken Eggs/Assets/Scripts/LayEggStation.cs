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
    public GameObject pickUpButton;
    public GameObject layingProgressBar;
    
	void Start ()
    {
        canLayEggs = false;
        playerIsInNest = false;
        pickUpButton.SetActive(false);
        layingProgressBar.SetActive(false);
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            playerIsInNest = true;
            if (playerIsInNest)
            {
                layingProgressBar.SetActive(true);
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
                pickUpButton.SetActive(true);
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
            if(slider.value == 0)
            {
                pickUpButton.SetActive(false);
                layingProgressBar.SetActive(false);
            }
        }
    }
}
