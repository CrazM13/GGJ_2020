﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile
{
	public enum TileDirections
	{
		Above,
		Below,
		Left,
		Right
	}
	
	public DisasterTypes type = DisasterTypes.Count;	// Treat Count as none (i.e. empty tile)
	public Vector3Int cellLocation;
	public int occupiedByUnit = -1;
	public bool justSpread = false;

	public Dictionary<TileDirections, WorldTile> adjacentTiles = new Dictionary<TileDirections, WorldTile>();

	public WorldTile(DisasterTypes type, Vector3Int loc)
	{
		this.type = type;
		cellLocation = loc;
	}

	public void SetAdjacentTile(TileDirections dir, WorldTile tile)
	{
		if (adjacentTiles.ContainsKey(dir))
		{
			adjacentTiles[dir] = tile;
		}
		else
		{
			adjacentTiles.Add(dir, tile);
		}
	}

	public bool IsFree()
	{
		if (type == DisasterTypes.Count && occupiedByUnit == -1)
		{
			return true;
		}

		return false;
	}
}
