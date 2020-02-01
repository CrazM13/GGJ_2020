using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public void StartGame()
	{
		SceneManager.LoadSceneAsync(4).completed += (e) => {
			GameNumber++;
			TileManager.Instance.Generate(GameNumber + 3);
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

}
