﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

	public int GameNumber { get; private set; } = 0;

	public Texture2D fixCursor;

	private bool[] aliveUnits = new bool[]
	{
		true, true, true, true
	};

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this);
		}
	}

	public void StartGame()
	{
        SceneManager.LoadSceneAsync(4).completed += (e) => {
			GameNumber++;
			Debug.Log(GameNumber);
			TileManager.Instance.Generate(GameNumber + 3);
			Vector3[] startPositions = TileManager.Instance.GetUnitStartPositions().ToArray();

			for (int i = 0; i < 4; i++) {
				UnitManager.instance.SetUnitPosition(i + 1, startPositions[i]);
				TileManager.Instance.OnUnitMovedToTile(startPositions[i], i + 1);

				if (!IsUnitAvailable(i + 1)) {
					UnitManager.instance.Kill(i + 1);
					TileManager.Instance.OnUnitKilled(i + 1);
				}
			}

            SoundSystem.Instance.PlaySound(SoundEvents.LevelStart);
		};
	}

	public void ShowUpgradeScreen()
	{
		SceneManager.LoadScene("UpgradeScreen");
	}

	public void ShowCreditScreen()
	{
		SceneManager.LoadScene("Credits");
	}

	public void ShowStatScreen()
	{
		SceneManager.LoadScene("StatsScreen");
	}

	public void QuitToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void EndGame()
	{
		GameNumber = 0;

		//aliveUnits = new bool[]
		//{
		//	true, true, true, true
		//};

		ShowStatScreen();
	}

	public bool IsUnitAvailable(int number) {
		return aliveUnits[number - 1];
	}

	public void DisableUnit(int number) {
		aliveUnits[number - 1] = false;
	}

	public void ResetDeath() {
		aliveUnits = new bool[]
		{
			true, true, true, true
		};
	}

}
