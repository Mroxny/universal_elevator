using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    [SerializeField] private Animator m_Animator;
    
    [HideInInspector] public DoorState m_State = DoorState.Open;
    private DoorState prevState;

    public enum DoorState {
        Open, Close, Waiting, Moving
    }


    //Check if the player interrupt closing the door
    private void OnTriggerEnter(Collider other)
    {
        if (m_State == DoorState.Moving && prevState != DoorState.Open && other.CompareTag("Player"))
        {
            Debug.Log("Doors Interrupted");
            m_Animator.SetTrigger("Wait");
            StopAllCoroutines();
            StartCoroutine(ChangeState(DoorState.Open, 0));

        }
    }

    //This solution can also be used, it is less efficient but gives a more reliable result

    /*private void OnTriggerStay(Collider other)
    {
        if (m_State == DoorState.Moving && prevState != DoorState.Open && other.tag == "Player")
        {
            Debug.Log("Doors Interrupted");
            m_Animator.SetTrigger("Wait");
            StopAllCoroutines();
            StartCoroutine(ChangeState(DoorState.Open, 0));

        }
    }*/

    // Change door state after certain time
    private IEnumerator ChangeState(DoorState state, float time) {
        m_State = DoorState.Moving;
        yield return new WaitForSeconds(time);
        m_State = state;
        Debug.Log("Changed state to " + state);
    }

    //Get full length of current animation
    private float GetCurrentAnimationTime(Animator targetAnim, int layer = 0)
    {
        AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);
        return animState.length;
    }

    public void OpenDoors() {
        if (m_State != DoorState.Open && m_State != DoorState.Moving) {
            m_Animator.SetTrigger("Open");
            prevState = DoorState.Open;
            StartCoroutine(ChangeState(DoorState.Open, GetCurrentAnimationTime(m_Animator)));
        }
    }

    public void CloseDoors() {
        if (m_State != DoorState.Close && m_State != DoorState.Moving) {
            m_Animator.SetTrigger("Close");
            prevState = DoorState.Close;
            StartCoroutine(ChangeState(DoorState.Close, GetCurrentAnimationTime(m_Animator)));
        }

    }
    
}
