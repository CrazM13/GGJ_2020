using System.Collections;
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
	public int isOccupiedByUnit = -1;

	Dictionary<TileDirections, WorldTile> adjacentTiles = new Dictionary<TileDirections, WorldTile>();

	//public WorldTile(GameObject go, bool isDisasterTile)
	//{
	//	gameObject = go;

	//	if (isDisasterTile)
	//	{
	//		type = TileManager.TileTypes.Disaster;
	//	}
	//}

	public WorldTile(DisasterTypes type)
	{
		this.type = type;
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
