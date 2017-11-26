using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //Pathfinding
    [Tooltip("Starting tile name as an int/string")]
    public int StartingTile = 0;
    [Tooltip("Size = number of stops in the path")]
    public List<Transform> LevelPath;

    //Enemy Lists
    [Tooltip("Put the enemy prefabs here")]
    public List<GameObject> ListOfEnemyTypes;
    public List<int> NumOfEachEnemy;
    [SerializeField] private List<GameObject> ListOfSpawnedEnemies;

    //Spawning
    [Tooltip("Time between each enemy spawn")]
    [SerializeField] private float SpawnDelay;
    private float SpawnTimer;
    [Tooltip("Up = 0, Right = 90, Down = 180, Left = 270")]
    [SerializeField] private float StartingRotation;
    [SerializeField] Transform SpawnPoint;

    //Game State
    private enum LevelStates { Spawning, WaitingForNextSpawn, FinishedSpawning, LevelComplete, LevelLost }
    LevelStates State;

    [SerializeField] private float PlayerHealth = 3;


    void Start()
    {
        //Setup Check
        if(LevelPath.Count <= 0) //Check path
        {
            Debug.Log("Level Path not set correctly");
        }
        if(ListOfEnemyTypes.Count != NumOfEachEnemy.Count) //Check enemies
        {
            Debug.Log("Enemy List not set correctly");
        }

        if(NumOfEachEnemy.Count > 0) //If okay, gogogogogogo 1a2a3a
        {
            State = LevelStates.Spawning;
        }

        ListOfSpawnedEnemies = new List<GameObject>(50);
    }

    // Update is called once per frame
    void Update()
    {
        switch(State)
        {
            case LevelStates.Spawning:
                int RandNum = Mathf.FloorToInt(Random.Range(0, ListOfEnemyTypes.Count - 0.001f)); //Pick a random number
                ListOfSpawnedEnemies.Add(Instantiate(ListOfEnemyTypes[RandNum], SpawnPoint.position, Quaternion.Euler(0, StartingRotation, 0))); //Spawn based on random number
                NumOfEachEnemy[RandNum] -= 1; //Decrease number of that enemy type
                if(NumOfEachEnemy[RandNum] <= 0) //Check if enemy type is finished spawning
                {
                    NumOfEachEnemy.RemoveAt(RandNum);
                    ListOfEnemyTypes.RemoveAt(RandNum);
                }
                SpawnTimer = 0; //Reset spawn timer
                if (ListOfEnemyTypes.Count <= 0) //Check if wave is done && change state
                {
                    State = LevelStates.FinishedSpawning;
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

            case LevelStates.FinishedSpawning:
                if(ListOfSpawnedEnemies.Count <= 0)
                {
                    State = LevelStates.LevelComplete;
                }
                break;

            case LevelStates.LevelComplete:
                //TODO Level win stuff
                break;

            case LevelStates.LevelLost:
                foreach(GameObject Enemy in ListOfSpawnedEnemies)
                {
                    Enemy.GetComponent<EnemyPathfinding>().GameOver();
                }
                break;
        }
    }

    public void TakeDamage()
    {
        PlayerHealth--;
        if(PlayerHealth <0)
        {
            //TODO GAMEOVER
            Debug.Log("GAME OVER");
            State = LevelStates.LevelLost;
        }
    }
}