using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Tiles {

    [Serializable]
    public class Tiles {
        [SerializeField]
        TileBase trap1;
        [SerializeField]
        TileBase trap2;
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
            }
            else {
                throw new NotImplementedException();
            }
        }

        private void NextLevel() {
            BlackBoard.scenesManager.NextLevel();
        }

        private void trap() {
            BlackBoard.scenesManager.Death();
        }
    }
}
