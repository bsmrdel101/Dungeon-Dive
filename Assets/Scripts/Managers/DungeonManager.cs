using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("Rooms")]
    public GameObject[] FloorOneStartingRooms;
    public GameObject[] FloorOneRooms;
    public GameObject[] FloorTwoStartingRooms;
    public GameObject[] FloorTwoRooms;
    public GameObject[] FloorThreeStartingRooms;
    public GameObject[] FloorThreeRooms;


    private void Start()
    {
        GenerateDungeonArea(DatabaseManager.Save.Floor);
    }

    public int DistanceBetweenTwoPoints(Transform pos1, Transform pos2)
    {
        int xDiff = (int)Mathf.Abs(pos1.position.x - pos2.position.x);
        int yDiff = (int)Mathf.Abs(pos1.position.y - pos2.position.y);
        return Mathf.Max(xDiff, yDiff);
    }

    // Takes in tile coordinates
    // Returns the Tile component
    public Tile GetTileFromCoords(float x, float y)
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (tile.transform.position.x == x && tile.transform.position.y == y)
                return tile;
        }
        return null;
    }

    private void GenerateDungeonArea(int floor)
    {

    }
}
