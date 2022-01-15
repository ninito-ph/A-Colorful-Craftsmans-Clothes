using UnityEngine;

namespace Game.Editor.Data.Attributes
{
    /// <summary>
    /// A base class that serves as a base for any attribute collection of any entity 
    /// </summary>
    public abstract class Attributes
    {
        [TextArea]
        public string Description;
    }
}