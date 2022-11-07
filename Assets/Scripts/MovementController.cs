using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    [SerializeField] private CharacterController cController;

    public float moveSpeed = 10f;
    public float gravity = -9.8f;

    public Transform groundCheck;
    public float groundDistance = .4f;
    public LayerMask groundLayerMask;

    private Vector3 velocity;
    private bool isGrounded;


    // Update is called once per frame
    void Update(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        Vector2 input = GetMoveInput();

        Vector3 movement = transform.right * input.x + transform.forward * input.y;
        cController.Move(movement * moveSpeed * Time.deltaTime);

        //Handle gravity
        velocity.y += gravity *Time.deltaTime;
        cController.Move(velocity * Time.deltaTime);

    }


    #region Input

    private Vector2 GetMoveInput() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        return new Vector2(x, y);
    }

    #endregion
}
