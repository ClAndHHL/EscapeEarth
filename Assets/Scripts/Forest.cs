using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    public Transform player;
    public GameObject[] obstacles;
    private WayPointsGizmos waypoints;
    public float startLen = 50;
    private float minLen = 100;
    private float maxLen = 200;

    private float startPos = 0;
    private float endPos = 0;

    private int targetWayPointIndex;
    private ForestGenerator forestDenerator; 
    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag(TagsLayer.Player).transform;
        waypoints = transform.Find("waypoints").GetComponent<WayPointsGizmos>();
        startPos = this.transform.position.z - 3000;
        endPos = this.transform.position.z;
        targetWayPointIndex = waypoints.wayPoints.Length - 2;
        forestDenerator = Camera.main.GetComponent<ForestGenerator>();
    }
    void Start() {
        GeneratorObstacle();
    }
    void Update() {
        if (player.position.z > transform.position.z + 200) {
            Camera.main.SendMessage("GeneratorForeast");
            GameObject.Destroy(this.gameObject);
        }
    }
    void GeneratorObstacle() {
        float z = startPos + startLen;
        do
        {
            z += Random.Range(minLen, maxLen);
            Vector3 pos = GetWaypointByZ(z);
            int obsIndex = Random.Range(0, obstacles.Length);
            GameObject go =  GameObject.Instantiate(obstacles[obsIndex], pos, Quaternion.identity) as GameObject;
            go.transform.parent = this.transform;
        } while (z < endPos);

    }

    Vector3 GetWaypointByZ(float z) {
        Transform[] wayPoints = waypoints.wayPoints;
        int index = 0;
        for (int i = 0;i<wayPoints.Length -1; ++i) {
            if (wayPoints[i].position.z>=z && z >= wayPoints[i + 1 ].position.z  ) {
                index = i;
                break;
            }
        }
        //index    index + 1
        Vector3 a = wayPoints[index].position;
        Vector3 b = wayPoints[index +1].position;

        return Vector3.Lerp(b, a, (z - b.z) / (a.z - b.z));
    }

    public Vector3 GetNextTargetPoint() {
        while (true) {
            if (waypoints.wayPoints[targetWayPointIndex].position.z - player.transform.position.z < 10)
            {
                targetWayPointIndex--;

                if (targetWayPointIndex<0) {
                    forestDenerator.GeneratorForeast();
                    //Destroy(this.gameObject, 2);
                    return forestDenerator.foreast1.GetComponent<Forest>().GetNextTargetPoint();
                }
            }
            else {
                return waypoints.wayPoints[targetWayPointIndex].position;
            }


        }

    }

}
