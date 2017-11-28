using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractionHandler : MonoBehaviour {
    private Button pauseButton;
	// Use this for initialization
	void Start () {
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        print(pauseButton);
        pauseButton.onClick.AddListener(pauseGame);
	}

    void pauseGame() {
        print("g");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
