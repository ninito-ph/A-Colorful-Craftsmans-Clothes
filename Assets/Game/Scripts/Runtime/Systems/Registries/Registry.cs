using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Systems
{
    /// <summary>
    /// A registry is a class that keeps a permanent list of objects.
    /// Useful for loading things that are not necessarily in the scene.
    /// </summary>
    /// <typeparam name="T">The type of object contained in the registry</typeparam>
    public abstract class Registry<T> : ScriptableObject
    {
        #region Public Fields

        public List<T> Items = new List<T>();

        #endregion
    }
}