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

    [SerializeField]
    Tilemap layer1Tilemap;
    Bounds layer1Bounds;

    [SerializeField]
    Tilemap layer2Tilemap;
    Bounds layer2Bounds;


    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GetComponent<Camera>();
        wallsTilemap.CompressBounds();
        layer1Tilemap.CompressBounds();
        layer2Tilemap.CompressBounds();

        wallsBounds = GetBounds(wallsTilemap);
        layer1Bounds = GetBounds(layer1Tilemap);
        layer2Bounds = GetBounds(layer2Tilemap);
        Debug.Log((wallsBounds.min, wallsBounds.max, wallsBounds.center));

        Vector2 minCorner = new Vector2(Mathf.Min(wallsBounds.min.x, layer1Bounds.min.x, layer2Bounds.min.x), Mathf.Min(wallsBounds.min.y, layer1Bounds.min.y, layer2Bounds.min.y));
        Vector2 maxCorner = new Vector2(Mathf.Max(wallsBounds.max.x, layer1Bounds.max.x, layer2Bounds.max.x), Mathf.Max(wallsBounds.max.y, layer1Bounds.max.y, layer2Bounds.max.y));
        cameraRect.min = minCorner;
        cameraRect.max = maxCorner;
        cameraMain.transform.position = (Vector3)cameraRect.center + Vector3.back;
        cameraMain.orthographicSize = cameraRect.width / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    Bounds GetBounds(Tilemap tilemap) {
        Bounds tempBounds = tilemap.gameObject.GetComponent<TilemapRenderer>().bounds;
        Bounds bounds = new Bounds();
        bounds.min = tilemap.gameObject.transform.TransformPoint(tempBounds.min) + (Vector3)Vector2.one*0.5f;
        bounds.max = tilemap.gameObject.transform.TransformPoint(tempBounds.max) + (Vector3)Vector2.one * 0.5f;
        return bounds;

    }


}
