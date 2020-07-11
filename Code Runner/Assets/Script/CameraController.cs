using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{     
    [Range(0,10)]
    [SerializeField]
    float borderSizeX;
    [Range(0, 10)]
    [SerializeField]
    float borderSizeY;
    [Range(0,10)]
    [SerializeField]
    float offsetX;
    [Range(0, 10)]
    [SerializeField]
    float offsetY;
    Camera cameraMain;
    Rect cameraRect;

    [SerializeField]
    Tilemap wallsTilemap;
    Bounds wallsBounds;

    Vector2 offset;

    void Start()
    {
        offset = new Vector2(offsetX, offsetY);
        cameraMain = GetComponent<Camera>();
        UpdateCameraPosition();

        
        //Debug.DrawLine(wallsBounds.min, wallsBounds.max);
    }


    void UpdateCameraPosition()
        {
            wallsTilemap.CompressBounds();

            wallsBounds = GetBounds(wallsTilemap);

            cameraRect.min = wallsBounds.min;
            cameraRect.max = wallsBounds.max;
            cameraMain.transform.position = (Vector3)cameraRect.center + Vector3.back + (Vector3)offset;
            cameraMain.orthographicSize = Mathf.Max((cameraRect.height + borderSizeY + offset.y*2) / 2, ((cameraRect.width + borderSizeX + offset.x*2) / 2) / cameraMain.aspect);
        }

    Bounds GetBounds(Tilemap tilemap) {
        Bounds tempBounds = tilemap.gameObject.GetComponent<TilemapRenderer>().bounds;
        Bounds bounds = new Bounds();
        bounds.min = tilemap.gameObject.transform.TransformPoint(tempBounds.min) + (Vector3)Vector2.one * 0.5f;
        bounds.max = tilemap.gameObject.transform.TransformPoint(tempBounds.max) + (Vector3)Vector2.one * 0.5f;
        return bounds;
    }
}
