using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraRaycast : MonoBehaviour
{
    private GameObject raycastObj;

    [SerializeField] private int rayLenghth = 5;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Image pointer;

    private void Update()
    {
        RaycastHit hit;

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, rayLenghth, layerMask.value))
        {

            // if obgjest is interactable
            if (hit.collider.CompareTag("Interactable"))
            {
                raycastObj = hit.collider.gameObject;
                SetPointerActive(true);

                //Press LPM to interract
                if (Input.GetMouseButtonDown(0))
                {
                    raycastObj.GetComponent<ButtonController>().PressButton();
                    Debug.Log("Interactable test");
                }
            }

        }
        else SetPointerActive(false);
    }

    private void SetPointerActive(bool active) {

        if (active) pointer.color = Color.green;
        else pointer.color = Color.red;
    }

}
