using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    // Build time in seconds
    public float buildTime = 3f;
    public GameObject shipPrefab;

    protected GameObject spawn;
    protected float buildCooldown = 0;

	// Use this for initialization
	void Start () {
        spawn = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (buildCooldown <= 0) {
            Spawn(shipPrefab);
        } else {
            buildCooldown -= Time.deltaTime;
        }
	}

    public void Spawn(GameObject prefab) {
        GameObject shipObject = Instantiate(prefab);

        shipObject.transform.position = spawn.transform.position;
        shipObject.transform.rotation = spawn.transform.rotation;

        buildCooldown = buildTime;
    }
}
