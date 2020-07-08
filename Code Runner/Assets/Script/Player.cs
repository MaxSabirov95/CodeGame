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
    Vector2 tragetPosition;

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
        if (Input.GetKeyDown(KeyCode.D))
        {
            tragetPosition = new Vector2(transform.position.x + 1f, transform.position.y);
            if (checkWall(tragetPosition) && transform.position == new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y),0))
            {
                MoveToPosition();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            tragetPosition = new Vector2(transform.position.x - 1f, transform.position.y);
            if (checkWall(tragetPosition) && transform.position == new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0))
            {
                MoveToPosition();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tragetPosition = new Vector2(transform.position.x, transform.position.y + 1f);
            if (checkWall(tragetPosition) && transform.position == new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0))
            {
                MoveToPosition();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tragetPosition = new Vector2(transform.position.x, transform.position.y - 1f);
            if (checkWall(tragetPosition) && transform.position == new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0))
            {
                MoveToPosition();
            }
        }
    }

    void MoveToPosition()
    {
        iTween.MoveTo(gameObject, tragetPosition, timeMovement);
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
        if (checkTrap(tragetPosition))
        {
            SceneManager.LoadScene(0);
        }
    }


}
