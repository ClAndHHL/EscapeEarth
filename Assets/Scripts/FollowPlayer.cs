using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    private Transform player;
    private Vector3 offset;
    void Awake() {

        player = GameObject.FindGameObjectWithTag(TagsLayer.Player).transform;
        offset = transform.position - player.position;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.position + offset;
	}
}
