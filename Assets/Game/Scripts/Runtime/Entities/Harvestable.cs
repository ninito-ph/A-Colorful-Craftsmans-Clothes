using System;
using System.Collections;
using Game.Runtime.Data.Attributes;
using Game.Runtime.Systems.Inventory;
using Ninito.UsualSuspects.Interactable;
using UnityEngine;

namespace Game.Runtime.Entities
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
        private HarvestableAttributes attributes;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;
        private bool _isHarvested = false;
        private Coroutine _regrowthCoroutine;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            TryGetComponent(out _spriteRenderer);
            TryGetComponent(out _collider);
        }

        private void OnValidate()
        {
            if (attributes == null) return;

            if (_spriteRenderer == null)
            {
                TryGetComponent(out _spriteRenderer);
            }
            
            _spriteRenderer.sprite = attributes.Graphic;
        }

        #endregion

        #region Private Methods

        private void Harvest()
        {
            _spriteRenderer.sprite = null;
            _collider.enabled = false;
            _isHarvested = true;
            _regrowthCoroutine = StartCoroutine(Regrow());

            if (attributes.HarvestEffect == null) return;
            Instantiate(attributes.HarvestEffect, transform.position, Quaternion.identity);
        }

        private void UnHarvest()
        {
            _spriteRenderer.sprite = attributes.Graphic;
            _collider.enabled = true;
            _isHarvested = false;
        }

        /// <summary>
        /// Regrows the harvestable after a given amount of time
        /// </summary>
        private IEnumerator Regrow()
        {
            yield return new WaitForSeconds(attributes.RespawnTime);
            UnHarvest();
        }

        #endregion

        #region IInteractable Implementation

        public string InteractionToolTip => "Press E to harvest " + attributes.Name;

        public void InteractWithAs(IInteractor interactor)
        {
            if (_isHarvested) return;
            Harvest();
            interactor.GameObject.GetComponent<ItemInventory>().AddItem(attributes);
        }

        #endregion
    }
}