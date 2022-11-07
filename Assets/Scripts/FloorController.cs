using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{

    public Transform floorEnter;

    [SerializeField] private ButtonController button;

    [HideInInspector] public DoorController doors;

    public void OpenDoors() {
        doors.OpenDoors();
    }
    public void CloseDoors()
    {
        doors.CloseDoors();
    }
}
