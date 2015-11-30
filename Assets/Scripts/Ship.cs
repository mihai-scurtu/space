using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
    public static float DAMAGE = 10f;

    public float forwardVelocity = 20f;
    public float rotationSpeed = 250f;

    public GameObject bulletObject;
    public float bulletSpeed = 100f;
    public float reloadTime = 1f;

    public float health = 100f;

    protected Rigidbody2D rigidBody;

    protected float shootCooldown = 0f;

    // Use this for initialization
    public void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }

        if (health < 0) {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            health -= Ship.DAMAGE;
            Destroy(coll.gameObject);
        }
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

    protected void Shoot() {
        if (shootCooldown > 0) {
            return;
        }

        GameObject bullet = Instantiate<GameObject>(bulletObject);

        bullet.transform.position = transform.position + transform.up * 0.2f;
        bullet.transform.rotation = transform.rotation;

        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);

        shootCooldown = reloadTime;
    }

    protected float AngleTo(Transform targetTransform) {
        Vector3 vectorToTarget = targetTransform.position - transform.position;
        return Vector3.Angle(transform.up, vectorToTarget);
    }
}
