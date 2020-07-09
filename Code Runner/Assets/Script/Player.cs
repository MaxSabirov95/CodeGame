using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float timeMovement;
    GameObject[] walls;
    GameObject[] traps;
    Vector2 targetPosition;

    private void Awake()
    {
        traps = GameObject.FindGameObjectsWithTag("Trap");
    }

    private void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    void Update()
    {
        Vector2 input = new Vector2((Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)?1:0) - (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)?1:0), (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)?1:0) - (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)?1:0));
        if(input != Vector2.zero) {
            targetPosition = new Vector2(transform.position.x + input.x, transform.position.y + input.y);
            if (checkWall(targetPosition) && (Vector2)transform.position == new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y))) {
                MoveToPosition();
            }
        }
    }

    void MoveToPosition()
    {
        iTween.MoveTo(gameObject, targetPosition, timeMovement);
        BlackBoard.gameManager.Swap();
        StartCoroutine(timeMove());
    }

    bool checkWall(Vector2 targetPosition)
    {
        foreach(GameObject wall in walls)
        {
            if((Vector2)wall.transform.position == targetPosition)
            {
                return false;
            }
        }
        return true;
    }

    bool checkTrap(Vector2 targetPosition)
    {
        foreach (GameObject trap in traps)
        {
            if ((Vector2)trap.transform.position == targetPosition && trap.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator timeMove()
    {
        yield return new WaitForSeconds(timeMovement);
        if (checkTrap(targetPosition))
        {
            SceneManager.LoadScene(0);
        }
    }


}
