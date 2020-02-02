using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisasterCountHUD : MonoBehaviour {

	public Text text;

	void Start() {

	}

	void Update() {
		text.text = TileManager.Instance.GetDisasterCount().ToString();
	}
}
