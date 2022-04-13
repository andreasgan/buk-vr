using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buk.Motion;
using TMPro;

public class ScoreMonitoring : MonoBehaviour
{
	private Movement movement;
	// private TextMeshProUGUI textObj;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponentInParent(typeof(Movement)) as Movement;
        if (!movement)
        {
        	Debug.Log("Could not get Movement component");
        }
    }

    // Update is called once per frame
    void Update()
    {
    	string scoreMessage = "Remaining life: " +  movement.scoreValue.ToString();
        this.GetComponent<TextMeshProUGUI>().text = scoreMessage;
    }
}
