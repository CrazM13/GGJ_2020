using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

	public int GameNumber { get; private set; } = 0;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(Instance);
			Instance = this;
		}
	}

	private void Start()
	{
		StartGame();
	}

	public void StartGame()
	{
		// SWITCH SCENES
		GameNumber++;
		TileManager.Instance.Generate(GameNumber + 3);
	}

}
