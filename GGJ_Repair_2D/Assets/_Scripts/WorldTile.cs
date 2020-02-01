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

    //public GameObject gameObject;
	//public TileManager.TileTypes type = TileManager.TileTypes.Empty;
	public DisasterTypes type = DisasterTypes.Count;	// Treat Count as none (i.e. empty tile)
	public Vector3Int cellLocation;
	public bool isOccupiedByUnit = false;
	public bool willBeOccupiedByUnit = false;	// So we can flag a tile as not currently occupied, but a unit wants to move here during the planning phase.

	Dictionary<TileDirections, WorldTile> adjacentTiles = new Dictionary<TileDirections, WorldTile>();

	//public WorldTile(GameObject go, bool isDisasterTile)
	//{
	//	gameObject = go;

	//	if (isDisasterTile)
	//	{
	//		type = TileManager.TileTypes.Disaster;
	//	}
	//}

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
}