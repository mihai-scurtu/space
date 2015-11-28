using UnityEngine;
using System.Collections;

public class RemoveBullet : MonoBehaviour {
    public float lifeTime = 3f;
    
	void Start () {
        Destroy(gameObject, lifeTime);
	}
}
