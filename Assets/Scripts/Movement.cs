using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    Vector2 movement, vision;
    float rightTrigger, leftTrigger, topSpeed = 50f;
    [SerializeField] WheelCollider frontRight, frontLeft, backRight, backLeft;

    [SerializeField] Transform frontRightTransform, frontLeftTransform, backLeftTransform, backRightTransform;

    public float acceleration = 10000f;
    public float brakingForce = 10000f;
    public float maxTurnAngle = 30f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate() {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        movement = gamepad.leftStick.ReadValue();
        vision = gamepad.rightStick.ReadValue();
        if (rb.velocity.magnitude > topSpeed) {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }

        leftTrigger = gamepad.leftTrigger.ReadValue();
        if (leftTrigger > 0) {
            currentBrakeForce = brakingForce;
            currentAcceleration = 0f;
        } else {
            currentBrakeForce = 0f;
        }
        rightTrigger = gamepad.rightTrigger.ReadValue();
        if (rightTrigger > 0) {
            currentAcceleration = acceleration;
        } else {
            currentAcceleration = 0f;
        }

        if (gamepad.buttonNorth.isPressed || gamepad.dpad.up.isPressed) {
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector3(0f, 0f, 0f);
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;
        backLeft.motorTorque = currentAcceleration;

        frontLeft.brakeTorque = currentBrakeForce;
        frontRight.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;

        currentTurnAngle = maxTurnAngle * movement.x;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);
    }

    void UpdateWheel(WheelCollider collider, Transform transform) {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);
        transform.position = position;
        transform.rotation = rotation * Quaternion.Euler(0f, 0f, 90f);
    }
}
