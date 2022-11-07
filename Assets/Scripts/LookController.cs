using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour {

    public float mouseSensitivity = 300f;

    [SerializeField]
    private GameObject player;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = GetMouse();


        xRotation -= mousePos.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * mousePos.x); 

    }

    #region Mouse

    private Vector2 GetMouse() {

        //Reading mouse input 
        float mX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        return new Vector2(mX, mY);
    }

    #endregion
}
