using UnityEngine;
using System.Collections;

public class HumanShip : Ship {
	// Update is called once per frame
	public new void Update () {
        base.Update();

        HandleInput(); 
    }

    void HandleInput() {
        if (Input.GetKey(KeyCode.W)) {
            ThrustUp();
        } else if (Input.GetKey(KeyCode.S)) {
            ThrustDown();
        }

        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        } else if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }
}
