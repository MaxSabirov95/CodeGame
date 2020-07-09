using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    Camera cameraMain;
    Rect cameraRect;

    [SerializeField]
    Tilemap wallsTilemap;
    Bounds wallsBounds;

    void Start()
    {
        cameraMain = GetComponent<Camera>();

        wallsTilemap.CompressBounds();

        wallsBounds = GetBounds(wallsTilemap);

        cameraRect.min = wallsBounds.min;
        cameraRect.max = wallsBounds.max;
        cameraMain.transform.position = (Vector3)cameraRect.center + Vector3.back;
        Debug.Log((cameraRect.height,cameraRect.width));
        cameraMain.orthographicSize = Mathf.Max(cameraRect.height/2,(cameraRect.width*0.5f)/cameraMain.aspect);

        Debug.DrawLine(wallsBounds.min, wallsBounds.max);
    }

    Bounds GetBounds(Tilemap tilemap) {
        Bounds tempBounds = tilemap.gameObject.GetComponent<TilemapRenderer>().bounds;
        Bounds bounds = new Bounds();
        bounds.min = tilemap.gameObject.transform.TransformPoint(tempBounds.min) + (Vector3)Vector2.one * 0.5f;
        bounds.max = tilemap.gameObject.transform.TransformPoint(tempBounds.max) + (Vector3)Vector2.one * 0.5f;
        return bounds;
    }
}
