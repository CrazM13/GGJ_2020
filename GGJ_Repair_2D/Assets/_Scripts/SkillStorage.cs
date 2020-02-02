using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStorage : MonoBehaviour {

	private static UnitStats[] stats = new UnitStats[] {
		new UnitStats(),
		new UnitStats(),
		new UnitStats(),
		new UnitStats()
	};

	private static UnitStats[] tmpStats = new UnitStats[] {
		new UnitStats(),
		new UnitStats(),
		new UnitStats(),
		new UnitStats()
	};

	private static int remainingPoints = 0;

	public static void AddTmpPoint(int unitNumber, Stats stat) {
		if (remainingPoints > 0) {
			tmpStats[unitNumber - 1].AddPoint(stat);
			remainingPoints--;
		}
	}

	public static void RemoveTmpPoint(int unitNumber, Stats stat) {
		unitNumber = unitNumber - 1;
		if (tmpStats[unitNumber].GetPoints(stat) > stats[unitNumber].GetPoints(stat)) {
			tmpStats[unitNumber - 1].RemovePoint(stat);
			remainingPoints++;
		}	
	}

	public static void Confirm() {
		for (int i = 0; i < 4; i++) {
			foreach (Stats s in System.Enum.GetValues(typeof(Stats))) {
				stats[i].SetPoints(s, tmpStats[i].GetPoints(s));
			}
		}
	}

	public static void Cancel() {
		for (int i = 0; i < 4; i++) {
			foreach (Stats s in System.Enum.GetValues(typeof(Stats))) {
				int refund = tmpStats[i].GetPoints(s) - stats[i].GetPoints(s);
				remainingPoints += refund;
				tmpStats[i].SetPoints(s, stats[i].GetPoints(s));
			}
		}
	}

	public static int GetLevel(int unitNumber, Stats stat) {
		return stats[unitNumber - 1].GetLevel(stat);
	}

	public static Stats StatFromDisaster(DisasterTypes type) {
		switch (type) {
			case DisasterTypes.Disease:
				return Stats.MEDIC;
			case DisasterTypes.Fire:
				return Stats.FIREFIGHTER;
			case DisasterTypes.Flood:
				return Stats.PLUMBER;
			default:
				return Stats.FIREFIGHTER;
		}
	}

	public static void AddUpgradePoint() {
		remainingPoints++;
	}

	public static void RemoveUpgradePoint() {
		remainingPoints--;
	}

	public static int GetRemainingPoints() {
		return remainingPoints;
	}

}
