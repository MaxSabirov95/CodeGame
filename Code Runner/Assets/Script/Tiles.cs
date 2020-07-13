using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.TilemapTools {

    //Steps for adding a new tile:
    //1) Add a refrence to it.
    //2) Add a action to it in the Tile action class.
    //3) Add a enum for it in the TilemapTools.


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
        [SerializeField]
        TileBase swapper;

        public void TrapTileAction(TileBase tile) {
            if (tile == trap1 || tile == trap2) {
                trap();
            }
            else {
                throw new NotImplementedException();
            }
        }

        public void GeneralTileAction(TileBase tile, Vector3Int tilePosition) {
            if (tile == openExitTile) {
                NextLevel();
            }
            else if (tile == key) {
                KeyAction(tilePosition);
            }
            else if (tile == swapper) {
                SwapperAction(tilePosition);
            }
        }

        private void SwapperAction(Vector3Int tilePosition) {
            BlackBoard.gameManager.Swap();
            TilemapTools.RemoveTile(swapper, tilePosition);
        }

        private void KeyAction(Vector3Int tilePosition) {
            foreach (Tilemap tilemap in BlackBoard.refrences.generalLayers) {
                tilemap.SwapTile(closedExitTile, openExitTile);
            }
            TilemapTools.RemoveTile(key, tilePosition);
        }

        private void NextLevel() {
            BlackBoard.scenesManager.NextLevel();
        }

        private void trap() {
            BlackBoard.scenesManager.Death();
        }
    }



    public static class TilemapTools {
        public enum Tiles { Trap, Key, ClosedExit, OpenExit }
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

        public static void RemoveTile(TileBase tile,Vector3Int tilePosition) {
            foreach (Tilemap tilemap in BlackBoard.refrences.generalLayers) {
                if (tilemap.GetTile(tilePosition) == tile) {
                    tilemap.SetTile(tilePosition, null);
                }
            }
        }

        public static Vector2Int GetInteraction(TilemapTools.Tiles tile, Vector3Int tilePosition) {
            foreach (Refrences.TileInteraction tileInteraction in BlackBoard.refrences.tilesInteractions) {
                if(tileInteraction.tile == tile && tileInteraction.position == (Vector2Int)tilePosition) {
                    return tileInteraction.targetPosition;
                }
            }
            throw new NotImplementedException();
        }
    }

}
