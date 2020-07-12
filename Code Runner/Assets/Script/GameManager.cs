using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Assets.Tiles;

[Serializable]
public class Refrences {
    [SerializeField]
    public Tilemap[] trapsLayers;
    [SerializeField]
    public Tilemap[] generalLayers;

}

public class GameManager : MonoBehaviour {

    [SerializeField]
    Tiles tiles;
    [SerializeField]
    Refrences refrences;
    [HideInInspector]
    public Tilemap walls;

    int currentTrapLayer = 0;

    private void Awake() {
        BlackBoard.gameManager = this;
        BlackBoard.refrences = refrences;
        BlackBoard.tiles = tiles;
    }

    void Start() {
        walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        
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




