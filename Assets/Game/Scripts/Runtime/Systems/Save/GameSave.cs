using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems.Save
{
    /// <summary>
    /// A class that represents a save file.
    /// </summary>
    [Serializable]
    public class GameSave
    {
        [SerializeField]
        public readonly InventorySave InventorySave;
    }
}