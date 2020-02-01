﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
	public enum TileTypes
	{
		Empty,		// Any tile on the grid free of disaster
		Disaster,	// A tile with any type of disaster
		None		// To represent off the edge of the grid
	}

	public static TileManager Instance = null;

	const int GRID_WIDTH = 9;
	const int GRID_HEIGHT = 9;

	GridLayout grid;
	Tilemap tilemap;

	TileBase emptyTile;
	Dictionary<DisasterTypes, TileBase> disasterTiles = new Dictionary<DisasterTypes, TileBase>();
	//GameObject emptyTilePrefab;
	//Dictionary<DisasterTypes, GameObject> disasterTilePrefabs = new Dictionary<DisasterTypes, GameObject>();

	Vector3 anchor = new Vector3(.5f, .5f, .5f);
	int numDisasters = 0;

	WorldTile[,] tiles = new WorldTile[GRID_WIDTH, GRID_HEIGHT];

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(Instance);
			Instance = this;
		}
	}

	private void Start()
	{
		grid = transform.parent.GetComponent<GridLayout>();
		tilemap = GetComponent<Tilemap>();

		emptyTile = Resources.Load<TileBase>("Tiles/EmptyTile");
		disasterTiles.Add(DisasterTypes.Fire, Resources.Load<TileBase>("Tiles/FireTile"));
		disasterTiles.Add(DisasterTypes.Flood, Resources.Load<TileBase>("Tiles/FloodTile"));
		disasterTiles.Add(DisasterTypes.Disease, Resources.Load<TileBase>("Tiles/DiseaseTile"));
	}

	private void Update()
	{
		//if (Input.GetMouseButtonDown(0))
		//{
		//	Vector3 worldLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	Vector3Int clickedTilePos = tilemap.WorldToCell(worldLoc);
		//	TileBase clickedTile = tilemap.GetTile(clickedTilePos);
		//	Debug.Log("Clicked " + clickedTile);
		//	tilemap.SetTile(clickedTilePos, disasterTiles[DisasterTypes.Disease]);
		//}
	}

	[ContextMenu("TestGenerateMap")]
	public void TestGenerate()
	{
		Generate(5);
	}

	public void Generate(int numDisasters)
	{
		this.numDisasters = numDisasters;	// Track the number of disasters
		Clear();	// Make sure the grid is empty before generating a new map
		List<Vector3> disasterLocations = RandomizeDisasterLocations(numDisasters);	// Determine where the disasters will be

		// Create the tiles
		for (int x = 0; x < GRID_WIDTH; x++)
		{
			for (int y = 0; y < GRID_HEIGHT; y++)
			{
				bool isDisasterLoc = false;
				foreach (Vector3 loc in disasterLocations)
				{
					if (loc.x == x && loc.y == y)
					{
						isDisasterLoc = true;
					}
				}

				//GameObject tileToSpawn = emptyTilePrefab;

				//if (isDisasterLoc)
				//{
				//	DisasterTypes type = (DisasterTypes)Random.Range((int)DisasterTypes.Fire, (int)DisasterTypes.Count);
				//	disasterTilePrefabs.TryGetValue(type, out tileToSpawn);
				//}

				//GameObject instance = Instantiate<GameObject>(tileToSpawn);
				//instance.transform.SetParent(gameObject.transform);
				//instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3(x - 4, y - 5, 0f) + anchor));
				//tiles[x, y] = new WorldTile(instance, isDisasterLoc);

				TileBase tileToSpawn = emptyTile;

				DisasterTypes type = DisasterTypes.Count;

				if (isDisasterLoc)
				{
					type = (DisasterTypes)Random.Range((int)DisasterTypes.Fire, (int)DisasterTypes.Count);
					disasterTiles.TryGetValue(type, out tileToSpawn);
				}

				Vector3Int cellLoc = new Vector3Int(x, y, 0);
				tilemap.SetTile(cellLoc, tileToSpawn);
				tiles[x, y] = new WorldTile(type);
			}
		}

		// Give each tile information about its neighbors
		for (int x = 0; x < GRID_WIDTH; x++)
		{
			for (int y = 0; y < GRID_HEIGHT; y++)
			{
				// Set the tile's left (no left if this is the first column)
				if (x > 0)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Left, tiles[x - 1, y]);
				}

				// Set the tile's right (no right if this is the last column)
				if (x < GRID_WIDTH - 1)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Right, tiles[x + 1, y]);
				}

				// Set the tile's above (no above if this is the first row)
				if (y > 0)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Above, tiles[x, y - 1]);
				}

				// Set the tile's below (no below if this is the last row)
				if (y < GRID_HEIGHT - 1)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Below, tiles[x, y + 1]);
				}
			}
		}
	}

	public void SpawnDisaster(DisasterTypes type)
	{

	}

	public void SpawnDisasterAdjacent(DisasterTypes type, Vector3 worldLocation)
	{

	}

	public int GetDisasterCount()
	{
		return numDisasters;
	}

	public void ClearDisaster(Vector3 worldLocation)
	{
		Vector3Int clickedTilePos = tilemap.WorldToCell(worldLocation);
		TileBase clickedTile = tilemap.GetTile(clickedTilePos);
		Debug.Log("Clearing " + clickedTile);
		tilemap.SetTile(clickedTilePos, emptyTile);
	}

	private List<Vector3> RandomizeDisasterLocations(int numDisasters)
	{
		List<Vector3> locs = new List<Vector3>();
		int centerGridWidthRange = GRID_WIDTH / 2 - 1;
		int centerGridHeightRange = GRID_HEIGHT / 2 + 1;
		
		for (int i = 0; i < numDisasters; i++)
		{
			int x = 0;
			int y = 0;

			bool success = false;
			while (!success)
			{
				x = Random.Range(0, GRID_WIDTH);
				y = Random.Range(0, GRID_HEIGHT);

				if (x < centerGridWidthRange || x > centerGridWidthRange &&
					y < centerGridHeightRange || y > centerGridHeightRange)
				{
					success = true;
				}
			}

			locs.Add(new Vector3(x, y, 0f));
		}

		return locs;
	}

	private void Clear()
	{
		for (int x = 0; x < GRID_WIDTH; x++)
		{
			for (int y = 0; y < GRID_WIDTH; y++)
			{
				if (tiles[x, y] != null)
				{
					tilemap.SetTile(new Vector3Int(x, y, 0), null);
					tiles[x, y] = null;
				}
			}
		}
	}
}
