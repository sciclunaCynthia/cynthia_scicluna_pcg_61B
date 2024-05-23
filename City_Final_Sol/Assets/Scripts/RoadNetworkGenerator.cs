using UnityEngine;
using System.Collections.Generic;

public class RoadNetworkGenerator : MonoBehaviour
{
    public GameObject roadSegmentPrefab;
    public GameObject curveRightPrefab;
    public GameObject curveLeftPrefab;
    public GameObject housePrefab;
    public GameObject treePrefab;
    public GameObject playerCarPrefab;
    public int numberOfSegments = 10;
    public float segmentLength = 10f;
    public float houseSpacing = 15f;
    public int houseFrequency = 1;
    public int treeFrequency = 3;
    public Terrain terrain; // Reference to the terrain

    private List<Vector3> roadPositions;
    private List<Vector3> directions;
    private GameObject playerCar;
    private Vector3 endPoint;
    private Vector3 terrainMin;
    private Vector3 terrainMax;

    void Start()
    {
        roadPositions = new List<Vector3>();
        directions = new List<Vector3>();

        // Get the terrain boundaries
        terrainMin = terrain.transform.position;
        terrainMax = terrainMin + terrain.terrainData.size;

        GenerateRoadNetwork();

        // Spawn the player car at the start point
        Vector3 startPoint = GetValidSpawnPosition(roadPositions[0]);
        playerCar = Instantiate(playerCarPrefab, startPoint, Quaternion.identity);

        // Set the end point
        endPoint = roadPositions[roadPositions.Count - 1];
    }

    void Update()
    {
        // Check if the player car has reached the end point
        if (Vector3.Distance(playerCar.transform.position, endPoint) < 5f)
        {
            Debug.Log("Player reached the end point. Game Over!");
            // Stop the game or display a game over screen
            Time.timeScale = 0;
        }
    }

    void GenerateRoadNetwork()
    {
        Vector3 currentPosition = terrainMin; // Start from the terrain's minimum position
        Vector3 direction = Vector3.forward;

        for (int i = 0; i < numberOfSegments; i++)
        {
            // Ensure the new position is within terrain bounds
            Vector3 nextPosition = currentPosition + direction * segmentLength;
            if (!IsWithinTerrain(nextPosition))
            {
                direction = GetRandomDirection(direction);
                nextPosition = currentPosition + direction * segmentLength;

                // Ensure the new direction keeps the position within bounds
                while (!IsWithinTerrain(nextPosition))
                {
                    direction = GetRandomDirection(direction);
                    nextPosition = currentPosition + direction * segmentLength;
                }
            }

            roadPositions.Add(currentPosition);
            directions.Add(direction);
            PlaceRoadSegment(currentPosition, direction);

            if (i > 0)
            {
                Vector3 prevPosition = roadPositions[i - 1];
                Vector3 prevDirection = directions[i - 1];
                Vector3 connectionDirection = GetConnectionDirection(prevPosition, currentPosition);

                if (connectionDirection != prevDirection)
                {
                    PlaceCurve(prevPosition, prevDirection, connectionDirection);
                }
            }

            if (i % 3 == 0)
            {
                direction = GetRandomDirection(direction);
            }

            currentPosition = nextPosition;

            if (i % houseFrequency == 0)
            {
                PlaceHousesAlongRoad(currentPosition, direction);
            }

            if (i % treeFrequency == 0)
            {
                PlaceTreesAlongRoad(currentPosition, direction);
            }
        }
    }

    bool IsWithinTerrain(Vector3 position)
    {
        return position.x >= terrainMin.x && position.x <= terrainMax.x &&
               position.z >= terrainMin.z && position.z <= terrainMax.z;
    }

    void PlaceRoadSegment(Vector3 position, Vector3 direction)
    {
        Instantiate(roadSegmentPrefab, position, Quaternion.LookRotation(direction));
    }

    void PlaceCurve(Vector3 position, Vector3 prevDirection, Vector3 connectionDirection)
    {
        GameObject curvePrefab = GetCurvePrefab(prevDirection, connectionDirection);
        Instantiate(curvePrefab, position, Quaternion.LookRotation(connectionDirection));
    }

    GameObject GetCurvePrefab(Vector3 prevDirection, Vector3 connectionDirection)
    {
        if (Vector3.Cross(prevDirection, connectionDirection).y > 0)
        {
            return curveRightPrefab;
        }
        else
        {
            return curveLeftPrefab;
        }
    }

    Vector3 GetConnectionDirection(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = (toPosition - fromPosition).normalized;

        if (direction == Vector3.forward || direction == -Vector3.forward ||
            direction == Vector3.right || direction == -Vector3.right)
        {
            return direction;
        }

        if (Vector3.Dot(direction, Vector3.forward) > 0)
        {
            return Vector3.forward;
        }
        else if (Vector3.Dot(direction, -Vector3.forward) > 0)
        {
            return -Vector3.forward;
        }
        else if (Vector3.Dot(direction, Vector3.right) > 0)
        {
            return Vector3.right;
        }
        else
        {
            return -Vector3.right;
        }
    }

    Vector3 GetRandomDirection(Vector3 currentDirection)
    {
        int randomValue = Random.Range(0, 3);
        switch (randomValue)
        {
            case 0:
                return Quaternion.Euler(0, 90, 0) * currentDirection;
            case 1:
                return Quaternion.Euler(0, -90, 0) * currentDirection;
            default:
                return currentDirection;
        }
    }

    void PlaceHousesAlongRoad(Vector3 position, Vector3 direction)
    {
        Vector3 leftOffset = Quaternion.Euler(0, -90, 0) * direction * houseSpacing;
        Vector3 rightOffset = Quaternion.Euler(0, 90, 0) * direction * houseSpacing;

        CreateHouse(position + leftOffset);
        CreateHouse(position + rightOffset);
    }

    void PlaceTreesAlongRoad(Vector3 position, Vector3 direction)
    {
        Vector3 leftOffset = Quaternion.Euler(0, -90, 0) * direction * (houseSpacing / 2);
        Vector3 rightOffset = Quaternion.Euler(0, 90, 0) * direction * (houseSpacing / 2);

        Instantiate(treePrefab, position + leftOffset, Quaternion.identity);
        Instantiate(treePrefab, position + rightOffset, Quaternion.identity);
    }

    void CreateHouse(Vector3 position)
    {
        GameObject houseObject = new GameObject("House");
        HouseGenerator houseGenerator = houseObject.AddComponent<HouseGenerator>();
        houseGenerator.GenerateHouse(position);
    }

    Vector3 GetValidSpawnPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 50, Vector3.down, out hit, 100f))
        {
            return hit.point + Vector3.up * 0.5f;
        }
        return position;
    }
}
