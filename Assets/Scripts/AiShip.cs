using UnityEngine;
using System.Collections;

public class AiShip : Ship
{
    public GameObject target;
    public float targetDistance = 3f;

    public float maxVelocity = 1f;

    protected Transform targetTransform;
    protected Rigidbody2D targetRigidBody;

    public string targetTag = "Ship";

    public new void Start() {
        base.Start();

        if (target) {
            UpdateTarget(target);
        }
    }

    public new void Update() {
        base.Update();

        if (!target) {
            AcquireTarget();
        }

        if (target) {
            if (rigidBody.velocity.magnitude >  maxVelocity) {
                // RotateTowards(rigidBody.velocity);
                // ThrustDown();

                EmergencyBrake();
            } else {
                RotateTowards(targetTransform.position + (Vector3) targetRigidBody.velocity);
                KeepDistance(targetTransform);
            }

            if (shootCooldown <= 0 && ShouldShootTarget()) {
                Shoot();
            }
            

            // Debug.DrawLine(transform.position, target.transform.position, Color.red);
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

    void AcquireTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        ArrayList otherTargets = new ArrayList();

        foreach (GameObject obj in targets) {
            if (obj != this.gameObject) {
                otherTargets.Add(obj);
            }
        }

        if (otherTargets.Count > 0) {
            int pos = Random.Range(0, otherTargets.Count);
            
            UpdateTarget((GameObject) otherTargets[pos]);
        } else {
            // if there are no targets stop
            this.EmergencyBrake();
        }
    }

    void UpdateTarget(GameObject obj) {
        target = obj;
        targetTransform = target.GetComponent<Transform>();
        targetRigidBody = target.GetComponent<Rigidbody2D>();
    }

    bool ShouldShootTarget() {
        if (Mathf.Abs(AngleTo(targetTransform)) < 5) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetTransform.position - transform.position, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ships"));

            if (hits.Length > 1) {
                // to avoid hitting the main collider
                RaycastHit2D hit = hits[1];

                if (hit.transform != null) {
                    string hitTag = hit.transform.gameObject.tag;


                    if (hitTag == targetTag) {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
