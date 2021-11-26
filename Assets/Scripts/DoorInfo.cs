using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour
{
    public Vector2 roomOrigin;
    DungeonGenerator dg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dg == null)
        {
            dg = GameManager.Instance.dg;
        }
        if (collision.tag != "Player")
        {
            return;
        }

        if (gameObject.name == "UpDoor")
        {
            int roomindex = dg.roomLocations.IndexOf(roomOrigin + new Vector2(0, 32));
            collision.gameObject.transform.position = dg.SpawnedRooms[roomindex].transform.Find("DownDoor").transform.position + new Vector3(0, 2, 0);
        }
        if (gameObject.name == "RightDoor")
        {
                int roomindex = dg.roomLocations.IndexOf(roomOrigin + new Vector2(32, 0));
                collision.gameObject.transform.position = dg.SpawnedRooms[roomindex].transform.Find("LeftDoor").transform.position + new Vector3(2, 0, 0);
        }
        if (gameObject.name == "DownDoor")
        {
            int roomindex = dg.roomLocations.IndexOf(roomOrigin + new Vector2(0, -32));
            collision.gameObject.transform.position = dg.SpawnedRooms[roomindex].transform.Find("UpDoor").transform.position + new Vector3(0, -2, 0);
        }
        if (gameObject.name == "LeftDoor")
        {
            int roomindex = dg.roomLocations.IndexOf(roomOrigin + new Vector2(-32, 0));
            collision.gameObject.transform.position = dg.SpawnedRooms[roomindex].transform.Find("RightDoor").transform.position + new Vector3(-2, 0, 0);
        }
    }
}
