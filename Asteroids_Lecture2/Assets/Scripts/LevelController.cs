using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public int asteroidCount;
    public float waveWait;
    public float startWait;
    public float spawnWait;
    public Text waveText;

    public GameObject[] objectsToSpawn = { null, null, null };
    public GameObject[] bossObjectsToSpawn = { null, null };

    private int countWaves = 1;
    private int maxAsteroidLevel = 0;
    public int bossCount = 1;
    private int bossLevel = 0;

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            Debug.Log("Wave is: " + countWaves);
            waveText.enabled = false;
            if (countWaves % 5 == 0)
            {
                for (int i = 0; i < bossCount; i++)
                {
                    Vector3 spawnPosition = GeneratePositionToSpawn();
                    GameObject enemy = Instantiate(bossObjectsToSpawn[bossLevel], spawnPosition, transform.rotation);
                    Debug.Log("Enemy spawn with health: " + enemy.GetComponent<HP>().currentHealth);
                    GameStateController.Instance.spawnObjects.Add(enemy);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            else
            {
                Debug.Log("Incoming: " + asteroidCount);
                for (int i = 0; i < asteroidCount; i++)
                {
                    Vector3 spawnPosition = GeneratePositionToSpawn();
                    GameObject newAsteroid = Instantiate(objectsToSpawn[whichAsteroidLevel()], spawnPosition, transform.rotation);
                    GameStateController.Instance.spawnObjects.Add(newAsteroid);
                    yield return new WaitForSeconds(spawnWait);
                }


            }
            progress();
            yield return new WaitWhile(() => GameStateController.Instance.isNotClear());
            printWave();
            yield return new WaitForSeconds(waveWait);
        }
    }

    private int whichAsteroidLevel()
    {
        int returnValue = Random.Range(0, maxAsteroidLevel + 1);
        //Debug.Log(returnValue);
        return returnValue;
    }

    private void progress()
    {
        if (countWaves % 5 == 0)
        {
            if(bossLevel == 0)
            {
                Debug.Log("Boss Wave overcome: next boss level -> 1 ");
                bossLevel = 1;
            }
            else
            {
                Debug.Log("Boss Wave overcome: next boss level -> 0; next boss count -> " + bossCount);
                bossLevel = 0;
                bossCount++;
            }
        }

        countWaves++;
        
        maxAsteroidLevel++;
        Debug.Log("Max asteroid level: " + maxAsteroidLevel);
        if(maxAsteroidLevel == 3)
        {
            maxAsteroidLevel = 0;
            asteroidCount++;
            Debug.Log("New asteroid count: " + asteroidCount);
        }

    }

    private Vector3 GeneratePositionToSpawn()
    {
        Vector3 resultingPositionViewport = new Vector3();
        int side = Random.Range(0, 3);

        switch (side)
        {
            case 0:
                resultingPositionViewport.x = Random.value;
                resultingPositionViewport.z = 0;
                break;
            case 1:
                resultingPositionViewport.x = 0;
                resultingPositionViewport.z = Random.value;
                break;
            case 2:
                resultingPositionViewport.x = Random.value;
                resultingPositionViewport.z = 1;
                break;
            case 3:

                resultingPositionViewport.x = 1;
                resultingPositionViewport.z = Random.value;
                break;
        }

        Vector3 resultingPositionWorld = Camera.main.ViewportToWorldPoint(resultingPositionViewport);
        resultingPositionWorld.y = 0;
        return resultingPositionWorld;
    }
    

    void printWave()
    {
        waveText.text = "Wave " + countWaves;
        waveText.enabled = true;
    }


















    //public void RegisterPlayer(GameObject playerObject)
    //{
    //    PlayerShip = playerObject;
    //}

    //public void UnregisterPlayer(GameObject gameObject)
    //{
    //    PlayerShip = null;
    //}

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //void Update()
    //{
    //    if (PlayerShip != null && !IsSpawningFinished)
    //    {
    //        CreatePlayableGrid();
    //        MarkPlayerSafeArea();
    //        SpawnNewAsteroids();
    //        IsSpawningFinished = true;
    //    }
    //}

    //private void MarkPlayerSafeArea()
    //{
    //    Vector2Int playerGridPos = GetCellCoordinates(PlayerShip.transform.position);

    //    //The player ship's pivot is located in grid cell (playerGridPos.x,playerGridPos.y)
    //    //Define a square, that is with a side of PlayerSafeCells + 1;
    //    //We will mark all cells in that square as Occupied, so that no asteroids can spawn there
    //    int minX = Mathf.Max(0, playerGridPos.x - PlayerSafeCells);
    //    int maxX = Mathf.Min(playerGridPos.x + PlayerSafeCells, PlayableAreaGrid.GetLength(0) - 1);
    //    int minY = Mathf.Max(0, playerGridPos.y - PlayerSafeCells);
    //    int maxY = Mathf.Min(playerGridPos.y + PlayerSafeCells, PlayableAreaGrid.GetLength(1) - 1);

    //    for (int xIter = minX; xIter <= maxX; ++xIter)
    //    {
    //        for (int yIter = minY; yIter <= maxY; ++yIter)
    //        {
    //            PlayableAreaGrid[xIter, yIter].isOccupied = true;
    //        }
    //    }
    //}

    //private float GetCameraDistance()
    //{
    //    return Camera.main.transform.position.y - transform.position.y;
    //}

    //private Vector3 GetPlayingFieldExtents()
    //{
    //    float cameraDistance = GetCameraDistance();
    //    Vector3 minPointVS = new Vector3(0, 0, cameraDistance);
    //    Vector3 maxPointVS = new Vector3(1, 1, cameraDistance);
    //    Vector3 minPoint = Camera.main.ViewportToWorldPoint(minPointVS);
    //    Vector3 maxPoint = Camera.main.ViewportToWorldPoint(maxPointVS);
    //    return maxPoint - minPoint;
    //}

    //private void CreatePlayableGrid()
    //{
    //    Vector3 playingFieldExtents = GetPlayingFieldExtents();
    //    Vector3 cellExtents = new Vector3(PlayableGridCellSize, 0.1f, PlayableGridCellSize);
    //    float halfCellSize = PlayableGridCellSize * 0.5f;

    //    //Determine size of the grid, based on the cell size and the playable area size
    //    int xSize = (int)Mathf.Floor(playingFieldExtents.x / PlayableGridCellSize);
    //    int zSize = (int)Mathf.Floor(playingFieldExtents.z / PlayableGridCellSize);
    //    PlayableAreaGrid = new PlayableGridCell[xSize, zSize];

    //    for (int i = 0; i < xSize; ++i)
    //    {
    //        for (int j = 0; j < zSize; ++j)
    //        {
    //            Vector3 cellOffset = new Vector3(i, 0, j) * PlayableGridCellSize + new Vector3(halfCellSize, playingFieldExtents.y, halfCellSize);
    //            Vector3 cellCenter = -playingFieldExtents * 0.5f + cellOffset;
    //            PlayableAreaGrid[i, j] = new PlayableGridCell
    //            {
    //                cellBounds = new Bounds(cellCenter, cellExtents),
    //                isOccupied = false
    //            };
    //        }
    //    }
    //}

    //private void SpawnNewAsteroids()
    //{
    //    List<Vector3> asteroidPositions = FindFreePositions(AsteroidsCount);
    //    for (int i = 0; i < asteroidPositions.Count; ++i)
    //    {
    //        Instantiate(AsteroidPrefab, asteroidPositions[i], Random.rotation);
    //    }
    //}

    //private List<Vector3> FindFreePositions(uint requestedPositionsCnt)
    //{
    //    //Create a list of all guaranteedly unoccupied cells
    //    List<PlayableGridCell> freeCells = new List<PlayableGridCell>();

    //    for (int i = 0; i < PlayableAreaGrid.GetLength(0); ++i)
    //    {
    //        for (int j = 0; j < PlayableAreaGrid.GetLength(1); ++j)
    //        {
    //            if (PlayableAreaGrid[i, j].isOccupied == false)
    //            {
    //                freeCells.Add(PlayableAreaGrid[i, j]);
    //            }
    //        }
    //    }

    //    var result = new List<Vector3>();
    //    for (uint i = 0; i < requestedPositionsCnt; ++i)
    //    {
    //        if (freeCells.Count > 0)
    //        {
    //            int chosenCellIndex = Random.Range(0, freeCells.Count);
    //            PlayableGridCell chosenCell = freeCells[chosenCellIndex];
    //            result.Add(chosenCell.cellBounds.center);
    //            freeCells.RemoveAt(chosenCellIndex);
    //        }
    //    }

    //    return result;
    //}

    //enum Axis
    //{
    //    X = 0,
    //    Y = 1,
    //    Z = 2
    //};

    //private Vector2Int GetCellCoordinates(Vector3 position)
    //{
    //    return new Vector2Int(GetCellCoordinate(position, Axis.X, Axis.X), GetCellCoordinate(position, Axis.Z, Axis.Y));
    //}

    //private int GetCellCoordinate(Vector3 position, Axis worldAxis, Axis gridAxis)
    //{
    //    //Convert from world space to Grid space
    //    //-PlayingFieldExtents/2 is at 0,0
    //    //+PlayingFieldExtents/2 is at grid.Length, grid.Length
    //    int originCellIndex = PlayableAreaGrid.GetLength((int)gridAxis) / 2; //(0,0,0) in world space is in the middle of the grid
    //    int resultingCoordinate = (int)(Mathf.Floor((position[(int)worldAxis] / PlayableGridCellSize))) + originCellIndex;
    //    resultingCoordinate = Mathf.Clamp(resultingCoordinate, 0, PlayableAreaGrid.GetLength((int)gridAxis));
    //    return (int)resultingCoordinate;
    //}

    //void OnDrawGizmos()
    //{
    //    //Used to draw some deubg display, so we can visualize what is on the grid, and the player safe area
    //    if (!ShowDebugDraw || PlayableAreaGrid == null) return;
    //    for (int i = 0; i < PlayableAreaGrid.GetLength(0); ++i)
    //    {
    //        for (int j = 0; j < PlayableAreaGrid.GetLength(1); ++j)
    //        {
    //            PlayableGridCell cell = PlayableAreaGrid[i, j];
    //            Bounds cellBounds = cell.cellBounds;
    //            Gizmos.color = cell.isOccupied ? Color.red : Color.green;
    //            Gizmos.DrawCube(cellBounds.center, cellBounds.size - new Vector3(0.05f, 0, 0.05f));
    //        }
    //    }
    //}
}
