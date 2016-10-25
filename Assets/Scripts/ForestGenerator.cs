using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour {

    public GameObject foreast1;
    public GameObject foreast2;

    public GameObject[] foreast;
    private int foreastCount = 2;
    public void GeneratorForeast() {
        ++foreastCount;
        int index = Random.Range(0, 3);
        GameObject go = GameObject.Instantiate(foreast[index],new Vector3(0,0,3000*foreastCount),Quaternion.identity) as GameObject;

        foreast1 = foreast2;
        foreast2 = go;
    }
}
