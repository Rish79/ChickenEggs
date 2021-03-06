﻿using UnityEngine;
using UnityEngine.UI;

public class BarricadeStation : MonoBehaviour
{
    [SerializeField]
    private bool canBuildBarricade;
    [SerializeField]
    private bool playerIsInBuildArea;

    public Transform barricadeSpawnPoint;
    public GameObject barricadePrefab;
    public Slider slider;
    public GameObject barricadeProgressBar;

    void Start()
    {
        canBuildBarricade = false;
        playerIsInBuildArea = false;
        barricadeProgressBar.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            playerIsInBuildArea = true;
            if (playerIsInBuildArea)
            {
                barricadeProgressBar.SetActive(true);
                Debug.Log("in egg laying");
                if (Input.GetButton("Building" + playerInput.playerId))
                {
                    slider.value += Time.deltaTime * playerInput.barricadeBuildTime;
                    if (slider.value >= 1)
                    {
                        canBuildBarricade = true;
                        slider.value = 0;
                    }
                }
            }

            if (canBuildBarricade)
            {
                Instantiate(barricadePrefab, barricadeSpawnPoint.transform.position, barricadePrefab.transform.rotation);
                canBuildBarricade = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            barricadeProgressBar.SetActive(false);
            playerIsInBuildArea = false;
            slider.value = 0;
        }
    }
}
