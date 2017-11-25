using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    
    //Enemy Stats
    public float Speed = 1;

    //Moving Stuff
    private enum EnemyStates { NewTile, Moving, Finished }
    private enum Directions { up, down, left, right }
    private EnemyStates State = EnemyStates.NewTile;
    private Directions DesiredDirection = Directions.down;

    private int CurrentStep = 0;
    private int CurrentTile;
    private int TargetTile;
    private Vector3 TargetTilePosition;

    [SerializeField] PathManager PM;
    

	// Use this for initialization
	void Start ()
    {
        CurrentTile = PM.StartingTile;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(State)
        {
            case EnemyStates.NewTile:
                if(CurrentStep == PM.LevelPath.Count)
                {
                    State = EnemyStates.Finished;
                    break;
                }
                TargetTile = int.Parse(PM.LevelPath[CurrentStep].name);
                TargetTilePosition = PM.LevelPath[CurrentStep].position + new Vector3(0, transform.localScale.y, 0);
                DesiredDirection = CalculateTurnDirection();
                CurrentStep++;
                State = EnemyStates.Moving;
                break;

            case EnemyStates.Moving:
                gameObject.transform.position = Vector3.MoveTowards(transform.position, TargetTilePosition, Speed/10);
                switch(DesiredDirection)
                {
                    case Directions.up:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 10);
                        break;
                    case Directions.down:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), 10);
                        break;
                    case Directions.right:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), 10);
                        break;
                    case Directions.left:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 270, 0), 10);
                        break;
                }
                if(transform.position == TargetTilePosition)
                {
                    CurrentTile = TargetTile;
                    State = EnemyStates.NewTile;
                }
                break;

            case EnemyStates.Finished:
                break;
        }
	}

    Directions CalculateTurnDirection()
    {
        if(CurrentTile < TargetTile)
        {
            if(CurrentTile%11 == TargetTile%11)
            {
                return Directions.down;
            }
            else
            {
                return Directions.right;
            }
        }
        else
        {
            if(CurrentTile%11 == TargetTile%11)
            {
                return Directions.up;
            }
            else
            {
                return Directions.left;
            }
        }
    }
}
