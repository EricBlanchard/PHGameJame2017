using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput> {

    public EGAMESTATE gameState;
    public PlayerDino selectedDino = null;

	void Start () {
		
	}

	void Update () {
        switch (gameState)
        {
            case EGAMESTATE.PLAYING_LEVEL:
                UpdatePlayingLevel();
                break;
            case EGAMESTATE.DINO_SELECTED:
                UpdateDinoSelected();
                break;
            default:
                break;
        }
    }

    void UpdatePlayingLevel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (isHit)
            {
                if (hit.transform.gameObject.tag == "PlayerDinosaur")
                {                  
                    if (hit.transform.gameObject.GetComponent<PlayerDino>())
                    {
                        gameState = EGAMESTATE.DINO_SELECTED;
                        selectedDino = hit.transform.gameObject.GetComponent<PlayerDino>();
                    }
                }
            }
        }
    }

    void UpdateDinoSelected()
    {
    }
}