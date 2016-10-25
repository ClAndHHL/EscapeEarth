using UnityEngine;
using System.Collections;

public enum GameState
{
    Menu,
    Play,
    End,
}

public class GameController : MonoBehaviour {

    public static GameState mGameState = GameState.Menu;
    public GameObject tapToStart;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && GameController.mGameState != GameState.Play ) {
            GameController.mGameState = GameState.Play;
            tapToStart.SetActive(false);
        }
	}
}
