using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour
{

    public enum ElevatorState {
        Moving, Waiting, ReadyToGo
    }

    [Range(0.1f, 5)]
    public float speed = 1;
    public List<FloorController> floors = new List<FloorController>();
    public FloorController startFloor;

    public AudioClip whenArrived;
    public AudioClip backgroundMusic;

    [SerializeField] private AudioSource audio;
    [SerializeField] private Transform buttonPanel;
    [SerializeField] private GameObject button;
    [SerializeField] private float buttonDistance = .33f;
    [SerializeField] private DoorController doors;


    [HideInInspector] public int targetFloor;

    private ElevatorState state = ElevatorState.Waiting;
    private FloorController currentFloor;



    void Start()
    {
        GenerateButtons();
        currentFloor = startFloor;
        audio.clip = backgroundMusic;
        audio.loop = true;
        audio.Play();

        foreach (FloorController e in floors) {
            if (e == startFloor) e.OpenDoors();
            else e.CloseDoors();
        }
        transform.position = startFloor.floorEnter.position;

    }

    void Update()
    {
        if (state == ElevatorState.Moving)
        {
            //solution easy to replace if needed
            transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, GetTargetPosition()) < .01f)
            {
                currentFloor = floors[targetFloor];
                audio.PlayOneShot(whenArrived);
                doors.OpenDoors();
                currentFloor.OpenDoors();
                state = ElevatorState.Waiting;
            }
        }
    }

    //Try to close the doors and start moving after time
    private IEnumerator StartMoving(float time) {
        do {
            yield return new WaitForSeconds(time / 2);

            doors.CloseDoors();
            currentFloor.CloseDoors();

            if (doors.m_State == DoorController.DoorState.Close && currentFloor.doors.m_State == DoorController.DoorState.Close) {
                state = ElevatorState.ReadyToGo;
            }

        }while (state == ElevatorState.Waiting);

        yield return new WaitForSeconds(time / 2);
        state = ElevatorState.Moving;
    }

    #region Buttons

    private void GenerateButtons() {
        Vector3 startPos =  new Vector3(buttonPanel.localPosition.x, buttonPanel.localPosition.y * -.2f, buttonPanel.localPosition.z);
        float row = startPos.y;

        for (int i = 0; i < floors.Count; i++) {

             row = startPos.y + (i * buttonDistance);

            SpawnButton(i+1, 0, row, -.01f);

        }
    
    }

    private void SpawnButton(int buttonId, float posX, float posY, float posZ) {
        GameObject button = Instantiate(this.button);
        button.transform.SetParent(buttonPanel.transform, true);
        button.transform.localPosition = new Vector3(posX, posY, posZ);

        Quaternion q = gameObject.transform.rotation;
        button.transform.rotation = buttonPanel.transform.rotation;
        button.GetComponent<Button>().onClick.AddListener((delegate { PressButon(buttonId);}));
        button.GetComponent<ButtonController>().defaultText = buttonId.ToString();

    }

    public void PressButon(int buttonId)
    {
        Debug.Log("Button pressed: " + buttonId);

        if (state == ElevatorState.Waiting && floors[buttonId - 1] != currentFloor)
        {
            targetFloor = buttonId - 1;
            StartCoroutine(StartMoving(5));
        }
    }

    #endregion

    private Vector3 GetTargetPosition() {
        return GetTargetPosition(targetFloor);
    }

    private Vector3 GetTargetPosition(int pos)
    {
        return floors[pos].floorEnter.position;
    }

}
