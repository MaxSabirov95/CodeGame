using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.TilemapTools {

    [Serializable]
    public class Tiles {
        [SerializeField]
        TileBase trap1;
        [SerializeField]
        TileBase trap2;
        [SerializeField]
        TileBase key;
        [SerializeField]
        TileBase closedExitTile;
        [SerializeField]
        TileBase openExitTile;

        public void TrapTileAction(TileBase tile) {
            if (tile == trap1 || tile == trap2) {
                trap();
            }
            else {
                throw new NotImplementedException();
            }
        }

        public void GeneralTileAction(TileBase tile) {
            if (tile == openExitTile) {
                NextLevel();
            }if (tile == key) {
                KeyAction();
            }
            else {
                throw new NotImplementedException();
            }
        }

        private void KeyAction() {
            foreach (Tilemap tilemap in BlackBoard.refrences.generalLayers) {
                Bounds tilemapBounds = TilemapTools.GetBounds(tilemap);
                for (int i = 0; i < ; i++) {

                }
            }
        }

        private void NextLevel() {
            BlackBoard.gameManager.PassLevel();
        }

        private void trap() {
            BlackBoard.gameManager.Death();
        }
    }



    public static class TilemapTools {
        public static Vector3Int VectorToInt(Vector3 vector) {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }

        public static Vector2Int VectorToInt(Vector2 vector) {
            return (Vector2Int)VectorToInt((Vector3)vector);
        }

        public static Bounds GetBounds(Tilemap tilemap) {
            BlackBoard.refrences.walls.CompressBounds();
            Bounds tempBounds = tilemap.gameObject.GetComponent<TilemapRenderer>().bounds;
            Bounds bounds = new Bounds();
            bounds.min = tilemap.gameObject.transform.TransformPoint(tempBounds.min) + (Vector3)Vector2.one * 0.5f;
            bounds.max = tilemap.gameObject.transform.TransformPoint(tempBounds.max) + (Vector3)Vector2.one * 0.5f;
            return bounds;
        }
    }

}
