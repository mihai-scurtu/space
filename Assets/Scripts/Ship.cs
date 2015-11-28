using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    public float forwardVelocity = 20f;
    public float rotationSpeed = 250f;
    
    protected Rigidbody2D rigidBody;

    // Use this for initialization
    public void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected void ThrustUp() {
        Thrust(forwardVelocity);
    }

    protected void ThrustDown() {
        Thrust(-forwardVelocity);
    }

    protected void Thrust(float increment) {
        rigidBody.AddForce(transform.up * increment * Time.deltaTime);
    }

    protected void RotateLeft() {
        Rotate(rotationSpeed);
    }

    protected void RotateRight() {
        Rotate(-rotationSpeed);
    }

    protected void RotateTowards(Vector3 targetPosition) {
        Vector3 vectorToTarget = targetPosition - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }

    protected void Rotate(float increment) {
        Vector3 rotationVector = transform.rotation.eulerAngles;

        rotationVector.z += increment * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    protected void EmergencyBrake() {
        rigidBody.AddForce(-rigidBody.velocity.normalized * forwardVelocity * 2 * Time.deltaTime);
    }
}
