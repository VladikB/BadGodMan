using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3rdPerson : MonoBehaviour
{
    public CharacterController charControl;
    public Transform cam;

    private float speed = 10f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothSpeed;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //angle for movement
        float targetAngle;
        float angle;
        //0f to prevent movement into floor/flying
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //calculate angle
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smooth out rotation
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothSpeed, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //movement
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            charControl.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
