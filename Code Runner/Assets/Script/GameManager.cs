using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public Tilemap trapsLayer1;
    [HideInInspector]
    public Tilemap trapsLayer2;
    [HideInInspector]
    public Tilemap walls;

    bool swap = true;

    private void Awake() {
        BlackBoard.gameManager = this;
    }

    void Start() {
        trapsLayer1 = GameObject.FindGameObjectWithTag("Layer1").GetComponent<Tilemap>();
        trapsLayer2 = GameObject.FindGameObjectWithTag("Layer2").GetComponent<Tilemap>();
        walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
        Swap(swap);
    }

    void Swap(bool swapValue) {
        swap = swapValue;
        trapsLayer1.gameObject.SetActive(swap);
        trapsLayer2.gameObject.SetActive(!swap);
    }
    public void Swap() {
        Swap(!swap);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }
}
