using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
    public float zoomIncrement = .5f;
    public float dragSpeed = 2;

    private Vector3 dragOrigin;

    void Update() {
        Pan();
        Zoom();
    }

    void Pan() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }

    void Zoom() {
        float zoom = Camera.main.orthographicSize - Input.mouseScrollDelta.y * zoomIncrement;
       
        Camera.main.orthographicSize = Mathf.Max(zoom, 1);
    }
}
