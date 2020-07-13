using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    public float timeMovement;
    Vector2Int targetPosition;
    bool canMove=true;

    void Update() {
        Vector2 input = new Vector2((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0) - 
                                    (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0),
                                    (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0) - 
                                    (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0));
        if ((input != Vector2.zero) && (canMove)) {
            targetPosition = VectorToInt(new Vector2(transform.position.x + input.x, transform.position.y + input.y));
            if (checkWall(targetPosition) && transform.position == VectorToInt(transform.position)) {
                MoveToPosition();
            }
        }
    }

    void MoveToPosition() {
        //iTween.MoveTo(gameObject, (Vector2)targetPosition, timeMovement);
        LeanTween.move(gameObject, (Vector2)targetPosition, timeMovement);
        BlackBoard.gameManager.Swap();
        StartCoroutine(timeMove());
    }

    bool checkWall(Vector2Int targetPosition) {
        return !BlackBoard.gameManager.walls.HasTile((Vector3Int)targetPosition);
    }

    bool checkTrap(Vector2Int targetPosition) {
        return (BlackBoard.gameManager.trapsLayer1.isActiveAndEnabled
            && BlackBoard.gameManager.trapsLayer1.HasTile((Vector3Int)targetPosition))
            ||
            (BlackBoard.gameManager.trapsLayer2.isActiveAndEnabled
            && BlackBoard.gameManager.trapsLayer2.HasTile((Vector3Int)targetPosition));
    }

    IEnumerator timeMove() {
        canMove = false;
        yield return new WaitForSeconds(timeMovement);
        if (checkTrap(targetPosition)) {
            BlackBoard.gameManager.ReloadScene();
        }
        canMove = true;
    }

    Vector3Int VectorToInt(Vector3 vector) {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
    Vector2Int VectorToInt(Vector2 vector) {
        return (Vector2Int)VectorToInt((Vector3)vector);
    }
}
