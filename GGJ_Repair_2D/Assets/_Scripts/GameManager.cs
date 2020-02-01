using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DisasterTypes
{
	Fire,
	Flood,
	Disease,
	Count
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

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
}
