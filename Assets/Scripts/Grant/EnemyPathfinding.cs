using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    
    //Enemy Stats
    public float MoveSpeed = 1;
    public float TurnSpeed = 720;
    public float BaseHealth = 100;
    public float CurrentHealth = 100;
    public float TotalHealth = 100;

    //Moving Stuff
    private enum EnemyStates { NewTile, Moving, Finished, Dead, GameOver }
    private enum Directions { up, down, left, right }
    private EnemyStates State = EnemyStates.NewTile;
    private Directions DesiredDirection = Directions.down;

    private int CurrentStep = 0;
    private int CurrentTile;
    private int TargetTile;
    private Vector3 TargetTilePosition;
    
    [SerializeField] LevelManager LM;

    //Scaling

    [SerializeField] LevelProgression LP;

	// Use this for initialization
	void Start ()
    {
        LM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        LP = GameObject.Find("LevelManager").GetComponent<LevelProgression>();
        //LP.ScaleNextWave += GetWaveScaling;
        CurrentTile = LM.StartingTile;
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch(State)
        {
            case EnemyStates.NewTile:
                if(CurrentStep == LM.LevelPath.Count)
                {
                    State = EnemyStates.Finished;
                    break;
                }
                TargetTile = int.Parse(LM.LevelPath[CurrentStep].name);
                TargetTilePosition = LM.LevelPath[CurrentStep].position + new Vector3(0, transform.localScale.y, 0);
                DesiredDirection = CalculateTurnDirection();
                CurrentStep++;
                State = EnemyStates.Moving;
                break;

            case EnemyStates.Moving:
                gameObject.transform.position = Vector3.MoveTowards(transform.position, TargetTilePosition, MoveSpeed*Time.deltaTime);
                switch(DesiredDirection)
                {
                    case Directions.up:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), TurnSpeed*Time.deltaTime);
                        break;
                    case Directions.down:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), TurnSpeed*Time.deltaTime);
                        break;
                    case Directions.right:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), TurnSpeed*Time.deltaTime);
                        break;
                    case Directions.left:
                        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 270, 0), TurnSpeed*Time.deltaTime);
                        break;
                }
                if(transform.position == TargetTilePosition)
                {
                    CurrentTile = TargetTile;
                    State = EnemyStates.NewTile;
                }
                break;

            case EnemyStates.Finished:
                LM.TakeDamage();
                State = EnemyStates.Dead;
                break;

            case EnemyStates.Dead:
                //TODO dead stuff
                Destroy(this.gameObject);
                break;

            case EnemyStates.GameOver:
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

    void GetWaveScaling(float Difficulty)
    {

    }

    public void TakeDamage(float DamageTaken)
    {
        CurrentHealth -= DamageTaken;
        if(CurrentHealth <= 0)
        {
            State = EnemyStates.Dead;
        }
    }

    public void GameOver()
    {
        State = EnemyStates.GameOver;
    }
}
