using UnityEngine;
using System.Collections;

public class AiShip : Ship
{
    public GameObject target;
    public float targetDistance = 3f;

    public float maxVelocity = 1f;

    protected Transform targetTransform;
    protected Rigidbody2D targetRigidBody;

    public new void Start() {
        base.Start();

        targetTransform = target.GetComponent<Transform>();
        targetRigidBody = target.GetComponent<Rigidbody2D>();
    }

    public new void Update() {
        base.Update();

        if (target) {
            if (rigidBody.velocity.magnitude >  maxVelocity) {
                // RotateTowards(rigidBody.velocity);
                // ThrustDown();

                EmergencyBrake();
            } else {
                RotateTowards(targetTransform.position + (Vector3) targetRigidBody.velocity);
                KeepDistance(targetTransform);
            }

            // this will only work if timeout is zero
            Shoot();
        }
    }

    void KeepDistance(Transform targetTransform) {
        Vector3 vectorToTarget = targetTransform.position - transform.position;

        if (rigidBody.velocity.magnitude > forwardVelocity) {
            return;
        }

        if (vectorToTarget.magnitude < targetDistance) {
            ThrustDown();
        } else {
            ThrustUp();
        }
    }
}
