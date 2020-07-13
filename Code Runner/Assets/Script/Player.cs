using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.TilemapTools;


//Class made mainly for player controls and tile checking.
//Death function, scene movement, refrences and other related functions are in the GameManager class.
public class Player : MonoBehaviour {
    public float timeMovement;
    Vector3Int targetPosition;
    bool canMove=true;

    void Update() {
        Vector2 input = new Vector2((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0)
                                  - (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0),
                                    (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0)
                                  - (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0));
        if (((input.x != 0 && input.y == 0) || (input.x == 0 && input.y !=0))  && (canMove)) {
            targetPosition = TilemapTools.VectorToInt(new Vector3(transform.position.x + input.x, transform.position.y + input.y, 0f));
            if (CheckWall(targetPosition) && transform.position == TilemapTools.VectorToInt(transform.position)) {
                MoveToPosition();
            }
        }
    }

    void MoveToPosition() {
        LeanTween.move(gameObject, (Vector3)targetPosition, timeMovement);
        BlackBoard.gameManager.Swap();
        StartCoroutine(TimeMove());
    }

    bool CheckWall(Vector3Int targetPosition) {
        return !BlackBoard.refrences.walls.HasTile(targetPosition);
    }

    void CheckTile(Vector3Int targetPosition) {
        foreach (Tilemap tilemap in BlackBoard.refrences.swappingLayers) {
            if (tilemap.isActiveAndEnabled) {
                TileBase tile = tilemap.GetTile(targetPosition);
                if (tile) {
                    BlackBoard.tiles.GeneralTileAction(tile, tilemap, targetPosition);
                }
            }
        }
        foreach (Tilemap tilemap in BlackBoard.refrences.generalLayers) {
            if (tilemap.isActiveAndEnabled) {
                TileBase tile = tilemap.GetTile(targetPosition);
                if (tile) {
                    BlackBoard.tiles.GeneralTileAction(tile, tilemap, targetPosition);
                }
            }
        }
    }

    IEnumerator TimeMove() {
        canMove = false;
        yield return new WaitForSeconds(timeMovement);
        CheckTile(targetPosition);
        canMove = true;
    }
}
