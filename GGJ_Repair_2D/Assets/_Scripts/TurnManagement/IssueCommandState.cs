using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueCommandState : ITurnState {

	private int selectedUnit = -1;

	private Vector3 mousePosition;
	private Vector3 selectedTile;

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return false;
	}

	public void Start() {
		UnitManager.instance.ResetRemainingActions();
	}

	public void Update() {
		//if (selectedUnit <= 0 || selectedUnit > 4) {
		//if (selectedUnit > 0 && selectedUnit <= 4) {
		//	HighlightOptions();
		//}

		if (Input.GetMouseButtonDown(0)) {
			OnClick();
		}

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
		if (selectedUnit > 0) {
			TileManager.Instance.ToggleAdjacentHighlight(selectedTile, false);
		}

		int newSelect = TileManager.Instance.GetUnitOccupyingCell(mousePosition);
		if (newSelect > 0) {
			selectedUnit = newSelect;

			selectedTile = mousePosition;

			HighlightOptions();
		}
	}

	private void AttemptAction() {
		if (selectedUnit < 0) return;
		int remainingActions = UnitManager.instance.GetRemainingActions(selectedUnit);

		if (remainingActions > 0) {
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

	public void End() {
		
	}
}
