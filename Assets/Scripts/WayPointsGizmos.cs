using UnityEngine;
using System.Collections;

public class WayPointsGizmos : MonoBehaviour {

    public Transform[] wayPoints;

    void OnDrawGizmos() {
        iTween.DrawLine(wayPoints);
    }



}
