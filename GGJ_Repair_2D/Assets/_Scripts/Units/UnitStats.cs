using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats {

	#region Const
	private const float GAIN = 2f;
	#endregion

	private Dictionary<Stats, int> levels;

	public UnitStats() {
		levels = new Dictionary<Stats, int>();

		foreach (Stats s in System.Enum.GetValues(typeof(Stats))) {
			levels.Add(s, 0);
		}

	}

	public void AddPoint(Stats stat) {
		levels[stat]++;
	}

	public void RemovePoint(Stats stat) {
		levels[stat]--;
	}

	public void SetPoints(Stats stat, int points) {
		levels[stat] = points;
	}

	public int GetLevel(Stats stat) {
		return Mathf.FloorToInt(Mathf.Pow(levels[stat], GAIN));
	}

	public int GetPoints(Stats stat) {
		return levels[stat];
	}

}

public enum Stats {
	FIREFIGHTER,
	MEDIC,
	PLUMBER
}

