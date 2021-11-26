using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public GameObject UpDoor;
    public GameObject RightDoor;
    public GameObject DownDoor;
    public GameObject LeftDoor;
    public int[] OpenDoors = new int[4];
    public Vector2 origin;
    public bool discovered = false;

    public void FixedUpdate()
    {
        if (GetEnemiesLeft() > 0)
        {
            UpDoor.GetComponent<SpriteRenderer>().enabled = true;
            UpDoor.GetComponent<Collider2D>().isTrigger = false;
            RightDoor.GetComponent<SpriteRenderer>().enabled = true;
            RightDoor.GetComponent<Collider2D>().isTrigger = false;
            DownDoor.GetComponent<SpriteRenderer>().enabled = true;
            DownDoor.GetComponent<Collider2D>().isTrigger = false;
            LeftDoor.GetComponent<SpriteRenderer>().enabled = true;
            LeftDoor.GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            UpdateDoors();
        }
    }

    public int GetEnemiesLeft()
    {
        return GetComponentsInChildren<Enemy>().Length;
    }

    public void UpdateDoors()
    {
        UpDoor.SetActive(true);

        RightDoor.SetActive(true);

        DownDoor.SetActive(true);

        LeftDoor.SetActive(true);

        if (OpenDoors[0] == 1)
        {
            //UpDoor.SetActive(true);
            UpDoor.GetComponent<DoorInfo>().roomOrigin = origin;
            UpDoor.GetComponent<Collider2D>().isTrigger = true;
            UpDoor.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (OpenDoors[1] == 1)
        {
            //RightDoor.SetActive(true);
            RightDoor.GetComponent<DoorInfo>().roomOrigin = origin;
            RightDoor.GetComponent<Collider2D>().isTrigger = true;
            RightDoor.GetComponent<SpriteRenderer>().enabled = false;

        }
        if (OpenDoors[2] == 1)
        {
            //DownDoor.SetActive(true);
            DownDoor.GetComponent<DoorInfo>().roomOrigin = origin;
            DownDoor.GetComponent<Collider2D>().isTrigger = true;
            DownDoor.GetComponent<SpriteRenderer>().enabled = false;

        }
        if (OpenDoors[3] == 1)
        {
            //LeftDoor.SetActive(true);
            LeftDoor.GetComponent<DoorInfo>().roomOrigin = origin;
            LeftDoor.GetComponent<Collider2D>().isTrigger = true;
            LeftDoor.GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}
