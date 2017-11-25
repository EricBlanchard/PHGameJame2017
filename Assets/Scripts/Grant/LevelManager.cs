using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [Tooltip("Starting tile name as an int/string")]
    public int StartingTile = 0;
    [Tooltip("Size = number of stops in the path")]
    public List<Transform> LevelPath;
    [Tooltip("Put the enemy prefabs here")]
    public List<GameObject> ListOfEnemies;
    public List<int> NumOfEachEnemy;

    [Tooltip("Time between each enemy spawn")]
    [SerializeField] private float SpawnDelay;
    private float SpawnTimer;
    [Tooltip("Up = 0, Right = 90, Down = 180, Left = 270")]
    [SerializeField] private float StartingRotation;
    [SerializeField] Transform SpawnPoint;

    private enum LevelStates { Spawning, WaitingForNextSpawn, Finished }
    LevelStates State;

    void Start()
    {
        //Setup Check
        if(LevelPath.Count <= 0) //Check path
        {
            Debug.Log("Level Path not set correctly");
        }
        if(ListOfEnemies.Count != NumOfEachEnemy.Count) //Check enemies
        {
            Debug.Log("Enemy List not set correctly");
        }

        if(NumOfEachEnemy.Count > 0) //If okay, gogogogogogo 1a2a3a
        {
            State = LevelStates.Spawning;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(State)
        {
            case LevelStates.Spawning:
                int RandNum = Mathf.FloorToInt(Random.Range(0, ListOfEnemies.Count - 0.001f)); //Pick a random number
                Instantiate(ListOfEnemies[RandNum], SpawnPoint.position, Quaternion.Euler(0, StartingRotation, 0)); //Spawn based on random number
                NumOfEachEnemy[RandNum] -= 1; //Decrease number of that enemy type
                if(NumOfEachEnemy[RandNum] <= 0) //Check if enemy type is finished spawning
                {
                    NumOfEachEnemy.RemoveAt(RandNum);
                    ListOfEnemies.RemoveAt(RandNum);
                }
                SpawnTimer = 0; //Reset spawn timer
                if (ListOfEnemies.Count <= 0) //Check if wave is done && change state
                {
                    State = LevelStates.Finished;
                }
                else
                {
                    State = LevelStates.WaitingForNextSpawn; //Change state
                }
                break;

            case LevelStates.WaitingForNextSpawn:
                SpawnTimer += Time.deltaTime; 
                if(SpawnTimer >= SpawnDelay) 
                {
                    State = LevelStates.Spawning;
                }
                break;

            case LevelStates.Finished:
                break;
        }
    }
}