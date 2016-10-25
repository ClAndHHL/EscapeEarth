using UnityEngine;
using System.Collections;

public class SirenRotate : MonoBehaviour {

    public float rotateSpeed = 180;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime);
	}
}
