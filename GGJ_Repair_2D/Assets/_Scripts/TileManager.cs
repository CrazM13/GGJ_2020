using System.Collections;
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
	const float SPREAD_DELAY = .65f;

	GridLayout grid;
	Tilemap tilemap;
	public Tilemap overlayTilemap;

	public bool AreDisastersSpreading { get; private set; }

	TileBase emptyTile;
	TileBase highlightTile;
	TileBase overlayFixTile;
	Dictionary<DisasterTypes, TileBase> disasterTiles = new Dictionary<DisasterTypes, TileBase>();
	
	Vector3 anchor = new Vector3(.5f, .5f, .5f);
	int numDisasters = 0;

	WorldTile[,] tiles = new WorldTile[GRID_WIDTH, GRID_HEIGHT];

	bool initialized = false;

	//Color highlightColor = new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f);
	//Color normalColor = new Color(Color.green.r, Color.green.g, Color.green.b, 0f);

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

	private void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{
			Vector3 worldLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int clickedTilePos = tilemap.WorldToCell(worldLoc);
			Debug.Log("Clicked " + clickedTilePos);
			tilemap.SetTile(clickedTilePos, emptyTile);
			tilemap.SetTileFlags(clickedTilePos, TileFlags.None);
			tilemap.SetColor(clickedTilePos, Color.green);
		}*/
	}

	private void Init()
	{
		grid = transform.parent.GetComponent<GridLayout>();
		tilemap = GetComponent<Tilemap>();

		emptyTile = Resources.Load<TileBase>("Tiles/EmptyTile");
		highlightTile = Resources.Load<TileBase>("Tiles/HighlightTile");
		overlayFixTile = Resources.Load<TileBase>("Tiles/FixOverlayTile");
		disasterTiles.Add(DisasterTypes.Fire, Resources.Load<TileBase>("Tiles/FireTile"));
		disasterTiles.Add(DisasterTypes.Flood, Resources.Load<TileBase>("Tiles/FloodTile"));
		disasterTiles.Add(DisasterTypes.Disease, Resources.Load<TileBase>("Tiles/DiseaseTile"));

		initialized = true;
	}

	[ContextMenu("TestGenerateMap")]
	public void TestGenerate()
	{
		Generate(5);
	}

	public void Generate(int numDisasters)
	{
		if (!initialized)
		{
			Init();
		}

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
				tiles[x, y] = new WorldTile(type, cellLoc);
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
				if (y < GRID_HEIGHT - 1)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Above, tiles[x, y + 1]);
				}

				// Set the tile's below (no below if this is the last row)
				if (y > 0)
				{
					tiles[x, y].SetAdjacentTile(WorldTile.TileDirections.Below, tiles[x, y - 1]);
				}
			}
		}
	}

	public void ToggleAdjacentHighlight(Vector3 location, bool isHighlighted = true)
	{
		Vector3Int cellPos = tilemap.WorldToCell(location);
		WorldTile tile = SafeGetWorldTile(cellPos);
		if (tile != null)
		{
			foreach (WorldTile adjTile in tile.adjacentTiles.Values)
			{
				if (adjTile.IsFree())
				{
					//tilemap.SetTileFlags(adjTile.cellLocation, TileFlags.None);
					//tilemap.SetColor(adjTile.cellLocation, isHighlighted ? highlightColor : normalColor);
					tilemap.SetTile(adjTile.cellLocation, isHighlighted ? highlightTile : emptyTile);
					//if (!isPendingTileRefresh)
					//{
					//	StartCoroutine(DelayRefreshTiles());
					//}
					//tilemap.RefreshTile(adjTile.cellLocation);
					//tilemap.RefreshAllTiles();
				}
				else if (adjTile.type != DisasterTypes.Count)
				{
					overlayTilemap.SetTile(adjTile.cellLocation, isHighlighted ? overlayFixTile : null);
				}
			}
		}
	}

	bool isPendingTileRefresh = false;
	IEnumerator DelayRefreshTiles()
	{
		isPendingTileRefresh = true;
		yield return new WaitForSeconds(.1f);

		tilemap.RefreshAllTiles();
		isPendingTileRefresh = false;
	}

	public Vector3 GetWorldCoordsFromCellPosition(Vector3Int cellPosition)
	{
		return tilemap.CellToWorld(cellPosition);
	}

	public WorldTile GetWorldTileAtPosition(Vector3 worldPosition)
	{
		Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
		return SafeGetWorldTile(cellPos);
	}

	public int GetUnitOccupyingCell(Vector3 worldLocation)
	{
		Vector3Int cellPos = tilemap.WorldToCell(worldLocation);
		WorldTile tile = SafeGetWorldTile(cellPos);

		if (tile != null)
		{
			return tile.occupiedByUnit;
		}

		return -1;
	}

	private WorldTile SafeGetWorldTile(Vector3Int cellPos)
	{
		if (cellPos.x >= 0 && cellPos.x < GRID_WIDTH)
		{
			if (cellPos.y >= 0 && cellPos.y < GRID_HEIGHT)
			{
				return tiles[cellPos.x, cellPos.y];
			}
		}

		return null;
	}

	public Vector3 GetWorldTileCenterPos(Vector3 pos)
	{
		return tilemap.GetCellCenterWorld(tilemap.WorldToCell(pos));
	}

	public void OnUnitMovedToTile(Vector3 newWorldLocation, int unitNumber)
	{
		Vector3Int cellPos = tilemap.WorldToCell(newWorldLocation);
		WorldTile tile = SafeGetWorldTile(cellPos);
		if (tile != null)
		{
			tile.occupiedByUnit = unitNumber;
			Debug.Log("Unit " + unitNumber + " moving to " + cellPos);

			foreach (WorldTile worldTile in tiles)
			{
				if (tile != worldTile && worldTile.occupiedByUnit == unitNumber)
				{
					worldTile.occupiedByUnit = -1;
				}
			}
		}
	}

	[ContextMenu("TestSpread")]
	public void StartDisasterSpread()
	{
		StartCoroutine(SpreadDisasters());
	}

	public IEnumerator SpreadDisasters()
	{
		AreDisastersSpreading = true;

		List<WorldTile> possibleSpreadTiles = new List<WorldTile>(4);

		// Find all the disaster tiles and spread to a free adjacent tile
		foreach (WorldTile tile in tiles)
		{
			// Not a disaster? Keep going. Also keep going if justSpread is set, as this means the disaster
			// tile is a result of spreading this turn.
			if (tile.type == DisasterTypes.Count || tile.justSpread)
			{
				continue;
			}
			
			foreach (WorldTile adjTile in tile.adjacentTiles.Values)
			{
				if (adjTile.type == DisasterTypes.Count)
				{
					possibleSpreadTiles.Add(adjTile);
				}
			}

			if (possibleSpreadTiles.Count > 0)
			{
				int spreadTileIndex = Random.Range(0, possibleSpreadTiles.Count);
				WorldTile spreadTile = possibleSpreadTiles[spreadTileIndex];
				if (spreadTile != null)
				{
					spreadTile.type = tile.type;
					spreadTile.justSpread = true;
					tilemap.SetTile(spreadTile.cellLocation, disasterTiles[tile.type]);
					numDisasters++;

                    // Sound
                    switch(tile.type)
                    {
                        case DisasterTypes.Disease:
                            SoundSystem.Instance.PlaySound(SoundEvents.DiseaseCreated);
                            break;
                        case DisasterTypes.Fire:
                            SoundSystem.Instance.PlaySound(SoundEvents.FireCreated);
                            break;
                        case DisasterTypes.Flood:
                            SoundSystem.Instance.PlaySound(SoundEvents.FloodCreated);
                            break;
                    }

					if (spreadTile.occupiedByUnit > -1)
					{
						UnitManager.instance.Kill(spreadTile.occupiedByUnit);
						spreadTile.occupiedByUnit = -1;
					}

					yield return new WaitForSecondsRealtime(SPREAD_DELAY);
				}
			}

			possibleSpreadTiles.Clear();
		}

		// Clear the justSpread flag
		foreach (WorldTile tile in tiles)
		{
			tile.justSpread = false;
		}

		AreDisastersSpreading = false;
	}

	public int GetDisasterCount()
	{
		return numDisasters;
	}

	public void ClearDisaster(Vector3 worldLocation)
	{
		Vector3Int clickedTilePos = tilemap.WorldToCell(worldLocation);
		tiles[clickedTilePos.x, clickedTilePos.y].type = DisasterTypes.Count;
		Debug.Log("Clearing " + clickedTilePos);
		tilemap.SetTile(clickedTilePos, emptyTile);
		numDisasters--;
	}

	public DisasterTypes GetTileDisasterType(Vector3 worldLocation)
	{
		Vector3Int tileCell = tilemap.WorldToCell(worldLocation);
		WorldTile tile = SafeGetWorldTile(tileCell);

		if (tile != null)
		{
			return tile.type;
		}

		return DisasterTypes.Count;
	}

	public List<Vector3> GetUnitStartPositions()
	{
		List<Vector3> positions = new List<Vector3>(4);

		int minX = Mathf.CeilToInt(GRID_WIDTH / 2f) - 1;
		int maxX = Mathf.CeilToInt(GRID_WIDTH / 2f) + 1;
		int minY = Mathf.CeilToInt(GRID_HEIGHT / 2f) - 1;
		int maxY = Mathf.CeilToInt(GRID_HEIGHT / 2f) + 1;

		positions.Add(tilemap.CellToWorld(new Vector3Int(minX, minY, 0)));
		positions.Add(tilemap.CellToWorld(new Vector3Int(minX, maxY, 0)));
		positions.Add(tilemap.CellToWorld(new Vector3Int(maxX, minY, 0)));
		positions.Add(tilemap.CellToWorld(new Vector3Int(maxX, maxY, 0)));

		return positions;
	}

	private List<Vector3> RandomizeDisasterLocations(int numDisasters)
	{
		List<Vector3> locs = new List<Vector3>();
		int minX = Mathf.CeilToInt(GRID_WIDTH / 2f) - 1;
		int maxX = Mathf.CeilToInt(GRID_WIDTH / 2f) + 1;
		int minY = Mathf.CeilToInt(GRID_HEIGHT / 2f) - 1;
		int maxY = Mathf.CeilToInt(GRID_HEIGHT / 2f) + 1;
		
		for (int i = 0; i < numDisasters; i++)
		{
			int x = 0;
			int y = 0;

			bool success = true;
			do
			{
				x = Random.Range(0, GRID_WIDTH);
				y = Random.Range(0, GRID_HEIGHT);
				Vector3 newLoc = new Vector3(x, y, 0f);

				if (((x >= minX && x <= maxX) &&
					(y >= minY && y <= maxY)) ||
					locs.Contains(newLoc))
				{
					success = false;
				}
				else
				{
					success = true;
				}

			} while (!success);

			locs.Add(new Vector3(x, y, 0f));
		}

		return locs;
	}

	private void Clear()
	{
		for (int x = 0; x < GRID_WIDTH; x++)
		{
			for (int y = 0; y < GRID_HEIGHT; y++)
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
