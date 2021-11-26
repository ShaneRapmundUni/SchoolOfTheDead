using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int totalRoomsMin;
    public int totalRoomsMax;
    [SerializeField]
    private int rooms;
    public List<GameObject> spawnRooms;
    public List<GameObject> shopRooms;
    public List<GameObject> itemRooms;
    public List<GameObject> BossRooms;
    public List<GameObject> otherRooms;

    //void GenerateDungeon()
    //{
    //    rooms = Random.Range(totalRoomsMin, totalRoomsMax);
    //    bool shopRoom, itemRoom, bossRoom, spawnRoom;
    //    shopRoom = itemRoom = bossRoom = spawnRoom = false;
    //    string[] AssignedRooms = new string[16];

    //    for (int i = 0; i < rooms; i++)
    //    {
    //        int randomPosition;
    //        do
    //        {
    //            randomPosition = Random.Range(0, 16);
    //        }
    //        while (AssignedRooms[randomPosition] != null);

    //        if (spawnRoom == false)
    //        {
    //            AssignedRooms[randomPosition] = "S";
    //            spawnRoom = true;
    //            continue;
    //        }
    //        if (bossRoom == false)
    //        {
    //            AssignedRooms[randomPosition] = "B";
    //            bossRoom = true;
    //            continue;
    //        }
    //        if (itemRoom == false)
    //        {
    //            AssignedRooms[randomPosition] = "I";
    //            itemRoom = true;
    //            continue;
    //        }
    //        if (shopRoom == false)
    //        {
    //            AssignedRooms[randomPosition] = "$";
    //            shopRoom = true;
    //            continue;
    //        }
    //        AssignedRooms[randomPosition] = "O";
    //    }
    //    for (int i = 0; i < AssignedRooms.Length; i++)
    //    {
    //        if (AssignedRooms[i] == null)
    //        {
    //            AssignedRooms[i] = "_";
    //        }
    //    }

    //    Debug.Log("- - - -");
    //    Debug.Log(AssignedRooms[0] + " " + AssignedRooms[1] + " " + AssignedRooms[2] + " " + AssignedRooms[3]);
    //    Debug.Log(AssignedRooms[4] + " " + AssignedRooms[5] + " " + AssignedRooms[6] + " " + AssignedRooms[7]);
    //    Debug.Log(AssignedRooms[8] + " " + AssignedRooms[9] + " " + AssignedRooms[10] + " " + AssignedRooms[11]);
    //    Debug.Log(AssignedRooms[12] + " " + AssignedRooms[13] + " " + AssignedRooms[14] + " " + AssignedRooms[15]);
    //    Debug.Log("- - - -");
    //}

    public List<GameObject> SpawnedRooms;
    public List<Vector2> roomLocations;

    public void ResetDungeon()
    {
        foreach (GameObject go in SpawnedRooms)
        {
            GameObject.Destroy(go);
        }
        SpawnedRooms.Clear();
        roomLocations.Clear();
    }
    public void GenerateDungeon()
    {
        rooms = Random.Range(totalRoomsMin, totalRoomsMax);
        //Vector2[] roomLocation = new Vector2[rooms];

        SpawnedRooms.Add(GameObject.Instantiate(spawnRooms[Random.Range(0, spawnRooms.Count)], new Vector2(0, 0), Quaternion.identity));
        //roomLocation[0] = new Vector2(0, 0);
        roomLocations = new List<Vector2>();
        roomLocations.Add(new Vector2(0, 0));
        Vector2 currentLocation = new Vector2(0, 0);
        int roomCount = 1;

        while (roomCount < rooms)
        {
            int direction = Random.Range(0, 4);
            Vector2 newRoom = currentLocation;
            if (direction == 0) //Up
            {
                 newRoom = new Vector2(currentLocation.x, currentLocation.y + 32);
            }
            if (direction == 1) // Right
            {
                newRoom = new Vector2(currentLocation.x + 32, currentLocation.y);
            }
            if (direction == 2) // Down
            {
                newRoom = new Vector2(currentLocation.x, currentLocation.y - 32);
            }
            if (direction == 3) // Left
            {
                newRoom = new Vector2(currentLocation.x - 32, currentLocation.y);
            }
            currentLocation = newRoom;
            if (roomLocations.Contains(newRoom))
            {
                continue;
            }
            else
            {
                SpawnedRooms.Add(GameObject.Instantiate(otherRooms[Random.Range(0, otherRooms.Count)], newRoom, Quaternion.identity));
                SpawnedRooms[SpawnedRooms.Count - 1].GetComponent<RoomInfo>().origin = newRoom;
                roomLocations.Add(newRoom);
            }
            roomCount++;
        }
        GameObject roomToRemove = SpawnedRooms[SpawnedRooms.Count - 1];
        SpawnedRooms.RemoveAt(SpawnedRooms.Count - 1);
        GameObject.Destroy(roomToRemove);
        SpawnedRooms.Add(GameObject.Instantiate(BossRooms[Random.Range(0, BossRooms.Count)], currentLocation, Quaternion.identity));
        SpawnedRooms[SpawnedRooms.Count - 1].GetComponent<RoomInfo>().origin = currentLocation;

        int randomItemLocation = Random.Range(1, SpawnedRooms.Count - 1);
        int randomShopLocation;
        do
        {
            randomShopLocation = Random.Range(1, SpawnedRooms.Count - 1);
        } while (randomShopLocation == randomItemLocation);

        roomToRemove = SpawnedRooms[randomItemLocation];
        SpawnedRooms[randomItemLocation] = GameObject.Instantiate(itemRooms[Random.Range(0, itemRooms.Count)], roomToRemove.transform.position, Quaternion.identity);
        SpawnedRooms[randomItemLocation].GetComponent<RoomInfo>().origin = roomToRemove.transform.position;
        GameObject.Destroy(roomToRemove);

        roomToRemove = SpawnedRooms[randomShopLocation];
        SpawnedRooms[randomShopLocation] = GameObject.Instantiate(shopRooms[Random.Range(0, shopRooms.Count)], roomToRemove.transform.position, Quaternion.identity);
        SpawnedRooms[randomShopLocation].GetComponent<RoomInfo>().origin = roomToRemove.transform.position;
        GameObject.Destroy(roomToRemove);


        for (int i = 0; i < SpawnedRooms.Count; i++)
        {
            RoomInfo roomInfo = SpawnedRooms[i].GetComponent<RoomInfo>();
            if (roomLocations.Contains(roomInfo.origin + new Vector2(0, 32)))
            {
                roomInfo.OpenDoors[0] = 1;
            }
            if (roomLocations.Contains(roomInfo.origin + new Vector2(32, 0)))
            {
                roomInfo.OpenDoors[1] = 1;
            }
            if (roomLocations.Contains(roomInfo.origin + new Vector2(0, -32)))
            {
                roomInfo.OpenDoors[2] = 1;
            }
            if (roomLocations.Contains(roomInfo.origin + new Vector2(-32, 0)))
            {
                roomInfo.OpenDoors[3] = 1;
            }
            roomInfo.UpdateDoors();
        }
    }
}
