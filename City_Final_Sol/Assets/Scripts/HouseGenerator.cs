using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    public float houseWidth = 4f;
    public float houseHeight = 6f;
    public float houseLength = 6f;
    public float doorWidth = 1f;
    public float doorHeight = 2f;
    public float windowWidth = 1f;
    public float windowHeight = 1f;

    public Color wallColor = Color.white;
    public Color doorColor = Color.yellow;
    public Color windowColor = Color.blue;

    public void GenerateHouse(Vector3 position)
    {
        // Create the house base (walls)
        GameObject houseBase = CreateCube(houseWidth, houseHeight, houseLength, wallColor);
        houseBase.transform.position = position + new Vector3(0, houseHeight / 2, 0);

        // Create the door
        GameObject door = CreateCube(doorWidth, doorHeight, 0.1f, doorColor);
        door.transform.position = position + new Vector3(0, doorHeight / 2, houseLength / 2 + 0.05f);

        // Create windows
        GameObject window1 = CreateCube(windowWidth, windowHeight, 0.1f, windowColor);
        window1.transform.position = position + new Vector3(houseWidth / 2 + 0.05f, houseHeight / 2, 0);

        GameObject window2 = CreateCube(windowWidth, windowHeight, 0.1f, windowColor);
        window2.transform.position = position + new Vector3(-houseWidth / 2 - 0.05f, houseHeight / 2, 0);
    }

    GameObject CreateCube(float width, float height, float length, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(width, height, length);
        cube.GetComponent<Renderer>().material.color = color;
        return cube;
    }
}
