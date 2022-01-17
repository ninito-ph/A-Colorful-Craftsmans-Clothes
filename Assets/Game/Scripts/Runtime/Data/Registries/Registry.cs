using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Runtime.Data.Registries
{
    /// <summary>
    /// A registry is a class that keeps a permanent list of objects.
    /// Useful for loading things that are not necessarily in the scene.
    /// </summary>
    /// <typeparam name="T">The type of object contained in the registry</typeparam>
    public class Registry<T> : ScriptableObject where T : Object
    {
        #region Public Fields

        public List<T> Items = new List<T>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the registry, if it is not already in the registry.
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Add(T item)
        {
            if (!Items.Contains(item))
            {
                Items.Add(item);
            }
        }

        #endregion
        
        #region Protected Methods

#if UNITY_EDITOR
        [ContextMenu("Get All Items In Project")]
        protected virtual void GetAllProjectItems()
        {
            Items.Clear();
            
            foreach (T item in (T[])Resources.FindObjectsOfTypeAll(typeof(T)))
            {
                if (!EditorUtility.IsPersistent(item)) continue;
                Items.Add(item);
            }
        }
#endif

        #endregion
    }
}