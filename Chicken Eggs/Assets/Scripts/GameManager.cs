using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text collectedEggstext;

    public int eggsCollected;

	// Update is called once per frame
	void Update ()
    {
        collectedEggstext.text = eggsCollected.ToString();
    }
}
