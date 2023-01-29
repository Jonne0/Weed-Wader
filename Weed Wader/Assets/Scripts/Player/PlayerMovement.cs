using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontalInput = 0;
    private float _verticalInput = 0;
    public float movementSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        MovePlayer();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (_horizontalInput != 0 || _verticalInput != 0)
            RotatePlayer();
    }

    private void GetPlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(_horizontalInput, _verticalInput, 0);
        transform.Translate(directionVector.normalized * Time.deltaTime * movementSpeed, Space.World);
    }

    private void RotatePlayer()
    {
        //Atan2 - Return value is the angle between the x-axis and a 2D vector starting at zero and terminating at (x,y).
        float angle = Mathf.Atan2(_verticalInput, _horizontalInput) * Mathf.Rad2Deg;
        //TODO, set sprite based on angle
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
