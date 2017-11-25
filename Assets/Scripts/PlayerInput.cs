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
            case EGAMESTATE.PLAYER_DINO_SELECTED:
                UpdatePlayerDinoSelected();
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
                        gameState = EGAMESTATE.PLAYER_DINO_SELECTED;
                        selectedDino = hit.transform.gameObject.GetComponent<PlayerDino>();
                        selectedDino.Selected();
                    }
                }
            }
        }
    }

    void UpdatePlayerDinoSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (isHit)
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    if (selectedDino.GetComponent<PlayerDino>())
                    {
                        selectedDino.GetComponent<PlayerDino>().Move(hit.point);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedDino)
            {
                gameState = EGAMESTATE.PLAYING_LEVEL;
                selectedDino.UnSelected();
                selectedDino = null;
            }
        }
    }
}