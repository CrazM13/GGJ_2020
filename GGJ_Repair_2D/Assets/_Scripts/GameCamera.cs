using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public static GameCamera instance = null;

	private float magnitude = 0;

	private Vector3 startPosition;

	private void Awake() {
		if (!instance) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	void Start() {
		startPosition = transform.position;
	}

	void Update() {
		if (magnitude <= 0) return;
		Vector2 randomVector = Random.insideUnitCircle.normalized * magnitude;
		transform.position = new Vector3(randomVector.x, randomVector.y, startPosition.z);
		magnitude -= Time.deltaTime;
	}

	public void Shake(float violence) {
		magnitude += violence;
	}

	private void OnDestroy() {
		if (instance == this) instance = null;
	}

}
