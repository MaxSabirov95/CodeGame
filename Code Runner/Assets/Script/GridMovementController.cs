using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementController : MonoBehaviour
{
    public void Move(Vector3Int _movement, float _time) {
        LeanTween.move(gameObject, _movement, _time);
    }

    public void SetPosition(Vector3Int _position) {
        transform.position = (Vector3)_position;
    }
}
