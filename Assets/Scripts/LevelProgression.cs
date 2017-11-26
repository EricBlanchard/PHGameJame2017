using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelProgression : MonoBehaviour {

	private int currentWave = 0;
	private float currentWaveDifficulty = 0;
	private const int MAX_WAVES = 10;
	private const float WAVE_SCALER = 2.7f;
	private void LastEnemyDied()
	{
		OnWaveComplete();
	}
	//grantsClass.LastEnemyDied += LastEnemyDied();

	private Action<int> m_OnWaveCompleted;

	public Action<int> OnWaveCompleted
	{
		set { m_OnWaveCompleted += value; }
	}

	private void OnWaveComplete()
	{
		if (m_OnWaveCompleted != null)
		{
			m_OnWaveCompleted(currentWave);
		}
		currentWave++;

		if (currentWave == 10)
		{
			EndTheGame();
		}
		DoScaling();
	}

	private Action m_EndGame;

	public Action EndGame
	{
		set { m_EndGame += value; }
	}

	private void EndTheGame()
	{
		//Do EndGamy things
		if (m_EndGame != null)
		{
			m_EndGame();
		}
	}

	private Action<float> m_ScaleNextWave;

	public Action<float> ScaleNextWave
	{
		set { m_ScaleNextWave += value; }
	}

	private void DoScaling()
	{
		currentWaveDifficulty = Mathf.Pow(currentWave / MAX_WAVES, WAVE_SCALER);
		if (m_ScaleNextWave != null)
		{
			m_ScaleNextWave(currentWaveDifficulty);
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
