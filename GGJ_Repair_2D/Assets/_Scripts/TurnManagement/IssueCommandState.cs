﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueCommandState : ITurnState {

	private int selectedUnit = -1;

	private Vector3 mousePosition;
	private Vector3 selectedTile;

	private GameUI ui = null;

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return false;
	}

	public void Start() {
		UnitManager.instance.ResetRemainingActions();
		if (!ui) {
			ui = GameObject.FindObjectOfType<GameUI>();
		}
		ui.SetEndTurnInteractable(true);
        ui.stageDisplay.text = "Stage - Issue Commands";
        ui.turnDisplay.text = "Turn " + TurnManager.instance.TurnNumber;
    }

	public void Update() {
		//if (selectedUnit <= 0 || selectedUnit > 4) {
		//if (selectedUnit > 0 && selectedUnit <= 4) {
		//	HighlightOptions();
		//}

		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (Input.GetMouseButtonDown(0)) {
			OnClick();
		}

		AttemptHoverAdjacent();

	}

	private void HighlightOptions() {
		TileManager.Instance.ToggleAdjacentHighlight(selectedTile, true);
	}

	private void OnClick() {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		SelectUnit();
		AttemptAction();
	}

	private void SelectUnit() {
        int newSelect = TileManager.Instance.GetUnitOccupyingCell(mousePosition);

        // check currently selected (if different)
        if (selectedUnit > 0 && selectedUnit != newSelect)
        {
			TileManager.Instance.ToggleAdjacentHighlight(selectedTile, false);

            // only play the sound if we are selecting nothing now
            // (that way we dont play over the next dude being selected)
            if (newSelect == 0)
            {
                SoundSystem.Instance.PlayHeroUnselectedSound(selectedUnit);
            }
		}


		if (newSelect > 0 && UnitManager.instance.IsUnitAlive(newSelect)) {
			selectedUnit = newSelect;

			selectedTile = mousePosition;

			HighlightOptions();

            SoundSystem.Instance.PlayHeroSelectedSound(selectedUnit);
		}
	}

	private void AttemptAction() {
		if (selectedUnit < 0) return;
		int remainingActions = UnitManager.instance.GetRemainingActions(selectedUnit);

		if (remainingActions > 0 && !UnitManager.instance.IsLastActionFixing(selectedUnit)) {
			WorldTile tile = TileManager.Instance.GetWorldTileAtPosition(selectedTile);

			WorldTile selected = TileManager.Instance.GetWorldTileAtPosition(mousePosition);

			foreach (WorldTile.TileDirections d in tile.adjacentTiles.Keys) {
				WorldTile t = tile.adjacentTiles[d];
				if (selected == t) {

					if (selected.occupiedByUnit == -1) {
						SendAction(selected, d);

						remainingActions--;
						if (remainingActions <= 0) {
							selectedUnit = -1;
							TileManager.Instance.ToggleAdjacentHighlight(selectedTile, false);
						}
					}
				}
			}
		} else {
			selectedUnit = -1;
			TileManager.Instance.ToggleAdjacentHighlight(selectedTile, false);
		}

	}

	private void SendAction(WorldTile tile, WorldTile.TileDirections direction) {
		UnitAction action;
		
		if (tile.type == DisasterTypes.Count) {
			action = new UnitAction(UnitAction.ActionType.MOVE, TileManager.Instance.GetWorldCoordsFromCellPosition(tile.cellLocation), WorldTileDirectionToActionDirection(direction));
			TileManager.Instance.ToggleAdjacentHighlight(selectedTile, false);
			selectedTile = TileManager.Instance.GetWorldCoordsFromCellPosition(tile.cellLocation);
			TileManager.Instance.ToggleAdjacentHighlight(selectedTile);
			UnitManager.instance.AddAction(selectedUnit, action);
		} else {
			action = new UnitAction(UnitAction.ActionType.FIX, TileManager.Instance.GetWorldCoordsFromCellPosition(tile.cellLocation), UnitAction.ActionDirection.NONE);
			UnitManager.instance.AddAction(selectedUnit, action);
			selectedUnit = -1;
		}
	}

	private UnitAction.ActionDirection WorldTileDirectionToActionDirection(WorldTile.TileDirections directions) {
		switch (directions) {
			case WorldTile.TileDirections.Above:
				return UnitAction.ActionDirection.UP;
			case WorldTile.TileDirections.Below:
				return UnitAction.ActionDirection.DOWN;
			case WorldTile.TileDirections.Left:
				return UnitAction.ActionDirection.LEFT;
			case WorldTile.TileDirections.Right:
				return UnitAction.ActionDirection.RIGHT;
			default:
				return UnitAction.ActionDirection.NONE;
		}
	}

	private void AttemptHoverAdjacent() {
		if (selectedUnit < 0) {
			FixPercentagePanel.instance.Hide();
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			return;
		}

		bool isShowing = false;

		WorldTile tile = TileManager.Instance.GetWorldTileAtPosition(selectedTile);

		WorldTile selected = TileManager.Instance.GetWorldTileAtPosition(mousePosition);

		foreach (WorldTile.TileDirections d in tile.adjacentTiles.Keys) {
			WorldTile t = tile.adjacentTiles[d];
			if (selected == t) {

				if (selected.type != DisasterTypes.Count) {
					float chance = UnitManager.instance.GetFixChance(selectedUnit, mousePosition);
					FixPercentagePanel.instance.Show(Mathf.RoundToInt(chance * 100));
					Cursor.SetCursor(GameManager.Instance.fixCursor, Vector2.zero, CursorMode.Auto);
					isShowing = true;
				}
			}
		}

		if (!isShowing) {
			FixPercentagePanel.instance.Hide();
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

	}

	public void End() {
		if (!ui) {
			ui = GameObject.FindObjectOfType<GameUI>();
		}
	}
}
