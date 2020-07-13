using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Assets.TilemapTools;

[Serializable]
public class Refrences {
    [SerializeField]
    TileAttachment[] tilesAttachments;
    [HideInInspector]
    public Tilemap[] trapsLayers;
    [HideInInspector]
    public Tilemap[] generalLayers;
    [HideInInspector]
    public Tilemap walls;

    [Serializable]
    class TileAttachment {
        [SerializeField]
        TilemapTools.Tiles tile;
        [SerializeField]
        Vector2Int position;
        [SerializeField]
        Vector2Int targetPosition;
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
        refrences.walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Trap");
        for (int i = 0; i < tempObjects.Length; i++) {
            refrences.trapsLayers[i] = tempObjects[i].GetComponent<Tilemap>();
        }
        tempObjects = GameObject.FindGameObjectsWithTag("General Layers");
        for (int i = 0; i < tempObjects.Length; i++) {
            refrences.generalLayers[i] = tempObjects[i].GetComponent<Tilemap>();
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
}




