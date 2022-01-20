using System.Collections;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities.Harvestables
{
    /// <summary>
    /// A class that controls a harvestable object
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public sealed class Harvestable : MonoBehaviour, IInteractable
    {
        #region Private Fields

        [Header("Properties")]
        [SerializeField]
        private HarvestableProvider harvestableProvider;

        private HarvestableAttributes _harvestableAttributes;

        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;
        
        private bool _isHarvested = false;
        private Coroutine _regrowthCoroutine;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _spriteRenderer);
            TryGetComponent(out _collider);
            DrawNewHarvestable();
        }

        #endregion

        #region Private Methods

        private void Harvest()
        {
            _spriteRenderer.sprite = null;
            _isHarvested = true;
            _collider.enabled = false;
            _regrowthCoroutine = StartCoroutine(Regrow());

            if (_harvestableAttributes.HarvestEffect == null) return;
            Instantiate(_harvestableAttributes.HarvestEffect, transform.position, Quaternion.identity);
        }

        private void UnHarvest()
        {
            DrawNewHarvestable();
            _isHarvested = false;
            _collider.enabled = true;
        }

        /// <summary>
        /// Regrows the harvestable after a given amount of time
        /// </summary>
        private IEnumerator Regrow()
        {
            yield return new WaitForSeconds(_harvestableAttributes.RespawnTime);
            UnHarvest();
        }

        /// <summary>
        /// Draws a new harvestable from the provider
        /// </summary>
        public void DrawNewHarvestable()
        {
            _harvestableAttributes = harvestableProvider.GetHarvestable();
            _spriteRenderer.sprite = _harvestableAttributes.Graphic;
        }

        #endregion

        #region IInteractable Implementation

        public string InteractionToolTip => "Press E to harvest " + _harvestableAttributes.Name;

        public void InteractWithAs(IInteractor interactor)
        {
            if (_isHarvested) return;
            Harvest();
            interactor.GameObject.GetComponent<InventoryProvider>().Contents.AddItem(_harvestableAttributes);
        }

        #endregion
    }
}