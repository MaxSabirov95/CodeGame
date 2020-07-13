using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Assets.TilemapTools;

[Serializable]
public class Refrences {
    [HideInInspector]
    public Tilemap[] trapsLayers;
    [SerializeField]
    public Tilemap[] generalLayers;
    [HideInInspector]
    public Tilemap walls;

}

public class GameManager : MonoBehaviour {

    [SerializeField]
    Tiles tiles;
    [SerializeField]
    Refrences refrences;


    int currentTrapLayer = 0;

    private void Awake() {
        BlackBoard.gameManager = this;
        BlackBoard.refrences = refrences;
        BlackBoard.tiles = tiles;
        refrences.walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        Debug.Log(refrences.walls);
        GameObject[] tempTrapsObjects = GameObject.FindGameObjectsWithTag("Trap");
        for (int i = 0; i < tempTrapsObjects.Length; i++) {
            refrences.trapsLayers[i] = tempTrapsObjects[i].GetComponent<Tilemap>();
        }
    }

    void Swap(int layer) {
        currentTrapLayer = layer % refrences.trapsLayers.Length;
        foreach (Tilemap tilemap in refrences.trapsLayers){
            tilemap.gameObject.SetActive(false);
        }
        refrences.trapsLayers[currentTrapLayer].gameObject.SetActive(true);
    }
    public void Swap() {
        Swap(currentTrapLayer + 1);
    }

    public void Death() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }

    public void PassLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}




