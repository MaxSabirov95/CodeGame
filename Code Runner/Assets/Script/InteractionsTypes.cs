using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

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
        [SerializeField]
        TileBase teleporter;
        [SerializeField]
        TileBase button;
        [SerializeField]
        TileBase door;


        public void GeneralTileAction(TileBase _tile, Tilemap _tilemap, Vector3Int _tilePosition) {
            if (_tile == trap1 || _tile == trap2) {
                Trap();
            }
            else if (_tile == openExitTile) {
                NextLevel();
            }
            else if (_tile == key) {
                KeyAction(_tilemap, _tilePosition);
            }
            else if (_tile == swapper) {
                SwapperAction(_tilemap, _tilePosition);
            }
            else if (_tile == teleporter) {
                TeleporterAction(_tilePosition);
            }
        }

        private void TeleporterAction(Vector3Int _tilePosition) {
            foreach (Refrences.TileInteraction tileInteraction in BlackBoard.refrences.tilesInteractions) {
                if (tileInteraction.tile == TilemapTools.InteractionsTypes.Teleporter && tileInteraction.position == (Vector2Int)_tilePosition) {
                    BlackBoard.player.moveController.SetPosition((Vector3Int)tileInteraction.targetPosition);
                }
            }
        }

        private void SwapperAction(Tilemap _tilemap, Vector3Int _tilePosition) {
            BlackBoard.gameManager.Swap();
            _tilemap.SetTile(_tilePosition, null);
        }

        private void KeyAction(Tilemap _tilemap, Vector3Int _tilePosition) {
            foreach (Tilemap tilemap in BlackBoard.refrences.generalLayers) {
                tilemap.SwapTile(closedExitTile, openExitTile);
            }
            _tilemap.SetTile(_tilePosition, null);
        }

        private void NextLevel() {
            BlackBoard.scenesManager.NextLevel();
        }

        private void Trap() {
            BlackBoard.scenesManager.Death();
        }
    }



    public static class TilemapTools {
        public enum InteractionsTypes { Button, ClosedExit, OpenExit, Teleporter }
        public static Vector3Int VectorToInt(Vector3 _vector) {
            return new Vector3Int(Mathf.RoundToInt(_vector.x), Mathf.RoundToInt(_vector.y), Mathf.RoundToInt(_vector.z));
        }

        public static Vector2Int VectorToInt(Vector2 _vector) {
            return (Vector2Int)VectorToInt((Vector3)_vector);
        }

        public static Bounds GetBounds(Tilemap _tilemap) {
            BlackBoard.refrences.walls.CompressBounds();
            Bounds tempBounds = _tilemap.gameObject.GetComponent<TilemapRenderer>().bounds;
            Bounds bounds = new Bounds {
                min = _tilemap.gameObject.transform.TransformPoint(tempBounds.min) + (Vector3)Vector2.one * 0.5f,
                max = _tilemap.gameObject.transform.TransformPoint(tempBounds.max) + (Vector3)Vector2.one * 0.5f
            };
            return bounds;
        }

        public static Vector2Int GetInteractions(TilemapTools.InteractionsTypes _tileType, Vector3Int _tilePosition) {
            foreach (Refrences.TileInteraction tileInteraction in BlackBoard.refrences.tilesInteractions) {
                if(tileInteraction.tile == _tileType && tileInteraction.position == (Vector2Int)_tilePosition) {
                    return tileInteraction.targetPosition;
                }
            }
            throw new NotImplementedException();
        }
    }

}
