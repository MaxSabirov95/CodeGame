using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Assets.TilemapTools;

[Serializable]
public class Refrences {
    [SerializeField]
    public TileInteraction[] tilesInteractions;
    [HideInInspector]
    public Tilemap[] swappingLayers;
    [HideInInspector]
    public Tilemap[] generalLayers;
    [HideInInspector]
    public Tilemap walls;

    [Serializable]
    public class TileInteraction {
        [SerializeField]
        public TilemapTools.InteractionsTypes tile;
        [SerializeField]
        public Vector2Int position;
        [SerializeField]
        public Vector2Int targetPosition;
    }
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
        refrences.walls = GameObject.FindGameObjectWithTag("Wall Layer").GetComponent<Tilemap>();
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Swapping Layers");
        refrences.swappingLayers = new Tilemap[tempObjects.Length];
        for (int i = 0; i < tempObjects.Length; i++) {
            refrences.swappingLayers[i] = tempObjects[i].GetComponent<Tilemap>();
        }
        tempObjects = GameObject.FindGameObjectsWithTag("General Layers");
        refrences.generalLayers = new Tilemap[tempObjects.Length];
        for (int i = 0; i < tempObjects.Length; i++) {
            refrences.generalLayers[i] = tempObjects[i].GetComponent<Tilemap>();
        }
    }

    private void Start() {
        Swap(currentTrapLayer);
    }

    void Swap(int layer) {
        currentTrapLayer = layer % refrences.swappingLayers.Length;
        foreach (Tilemap tilemap in refrences.swappingLayers){
            tilemap.gameObject.SetActive(false);
        }
        refrences.swappingLayers[currentTrapLayer].gameObject.SetActive(true);
    }
    public void Swap() {
        Swap(currentTrapLayer + 1);
    }
}




