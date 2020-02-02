using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnState {

	void Start();
	void Update();
	void End();
	bool IsDone();
	bool DidWin();

}
