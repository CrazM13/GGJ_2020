using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FixPercentagePanel : MonoBehaviour {

	public static FixPercentagePanel instance = null;

	private CanvasGroup group;
	public Text chance;

	private void Awake() {
		if (!instance) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	void Start() {
		group = GetComponent<CanvasGroup>();
		Hide();
	}

	public void Show(int chance) {
		this.chance.text = chance.ToString();
		group.alpha = 1;
	}

	public void Hide() {
		group.alpha = 0;
	}

	private void OnDestroy() {
		if (instance == this) instance = null;
	}
}
