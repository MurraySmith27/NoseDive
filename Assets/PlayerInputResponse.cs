using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputResponse : MonoBehaviour
{

    public Rigidbody rb;
    public Transform shipTransform;
    public float baseSpeed;
    public float rollSpeed;
    public float pitchSpeed;
    public float turnSpeed;

    public float topSpeed;

    public float drag;

    [SerializeField]
    public float angleToWorldUp;

    public float gravityAccel;
    public float goUpSlowdown;
    private PlayerJetActions playerJetActions;

    [SerializeField] private float gainedSpeed = 0;


    public void Awake()
    {
        playerJetActions = new PlayerJetActions();

        playerJetActions.Player.Enable();

        if (!rb)
            rb = GetComponent<Rigidbody>();
    }


    public void FixedUpdate()
    {
        //Handle input for tilting and rolling
        float roll_axis = playerJetActions.Player.Roll.ReadValue<float>();
        float pitch_axis = playerJetActions.Player.Pitch.ReadValue<float>();
        
        //TODO: Lock rotations so you can't go upside down.
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(-roll_axis * rollSpeed, -pitch_axis * pitchSpeed, 0) * Time.fixedDeltaTime));

        //Move forward
        //rb.AddForce(gameObject.transform.forward * speed);

        float speed = baseSpeed + gainedSpeed;

        Vector3 forwardProjectedOntoRightAndWorldUp = Vector3.ProjectOnPlane(shipTransform.forward, Vector3.Cross(shipTransform.right, Vector3.up));

        angleToWorldUp = Vector3.Angle(forwardProjectedOntoRightAndWorldUp, Vector3.up);
        bool zero_mod = true;
        if (shipTransform.forward.y < 0)
            angleToWorldUp = 180 - angleToWorldUp;

        if (shipTransform.right.y > 0)
            zero_mod = false;

        float globalTilt = (float)Math.Max(Math.Sin((angleToWorldUp) * 2 * Math.PI / 180), 0);

        gainedSpeed = Math.Min(Math.Max(gainedSpeed + Convert.ToInt32(zero_mod) * gravityAccel * globalTilt, 0f), topSpeed);

        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.up * 100, Color.red, 10f);
        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.right * 100, Color.blue, 10f);
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.green, 10f);
        Debug.DrawRay(gameObject.transform.position, Vector3.ProjectOnPlane(shipTransform.forward, Vector3.Cross(shipTransform.right, Vector3.up)));
        Debug.DrawRay(gameObject.transform.position, Vector3.up * 100, Color.yellow, 10f);

        Debug.Log($"speed: {speed}");
        shipTransform.Translate(shipTransform.right * speed, Space.World);
        gainedSpeed *= (1-drag);
        gainedSpeed = Math.Min(Math.Max(gainedSpeed - Convert.ToInt32(!zero_mod) * goUpSlowdown * globalTilt, 0f), topSpeed);
    }
}
