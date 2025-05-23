﻿/// <summary>
/// Project : Easy Build System
/// Class : BuildingArea.cs
/// Namespace : EasyBuildSystem.Features.Runtime.Buildings.Area
/// Copyright : © 2015 - 2022 by PolarInteractive
/// </summary>

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Manager;

using EasyBuildSystem.Features.Runtime.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EasyBuildSystem.Features.Runtime.Buildings.Area
{
    [HelpURL("https://polarinteractive.gitbook.io/easy-build-system/components/building-area")]
    public class BuildingArea : MonoBehaviour
    {
        #region Fields

        public enum ShapeType { SPHERE, BOUNDS }

        [SerializeField] ShapeType m_Shape = ShapeType.SPHERE;
        public ShapeType Shape { get { return m_Shape; } }

        [SerializeField, Range(1, 100)] int m_Priority = 1;
        public int Priority { get { return m_Priority; } }

        [SerializeField] float m_Radius = 5f;
        public float Radius
        {
            get 
            {
                float scaledRadius = m_Radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
                return scaledRadius;
            }
        }

        [SerializeField] Bounds m_Bounds = new Bounds(Vector3.zero, Vector3.one);
        public Bounds Bounds 
        {
            get 
            {
                Vector3 scaledSize = Vector3.Scale(m_Bounds.size, transform.lossyScale);
                return new Bounds(m_Bounds.center, scaledSize);
            }
        }

        [SerializeField] bool m_CanPlacingAnyBuildingParts = true;
        public bool CanPlacingAnyBuildingParts { get { return m_CanPlacingAnyBuildingParts; } }

        [SerializeField] List<BuildingPart> m_CanPlacingSpecificBuildingParts = new List<BuildingPart>();

        [SerializeField] bool m_CanEditingAnyBuildingParts = true;
        public bool CanEditingAnyBuildingParts { get { return m_CanEditingAnyBuildingParts; } }

        [SerializeField] List<BuildingPart> m_CanEditingSpecificBuildingParts = new List<BuildingPart>();

        [SerializeField] bool m_CanDestroyingAnyBuildingParts = true;
        public bool CanDestroyingAnyBuildingParts { get { return m_CanDestroyingAnyBuildingParts; } }

        [SerializeField] List<BuildingPart> m_CanDestroyingSpecificBuildingParts = new List<BuildingPart>();

        [SerializeField] List<BuildingPart> m_RegisteredBuildingParts = new List<BuildingPart>();

        #region Events

        /// <summary>
        /// Event triggered when a Building Part is registered in the area.
        /// </summary>
        [Serializable]
        public class RegisteredBuildingPartEvent : UnityEvent<BuildingPart> { }
        public RegisteredBuildingPartEvent OnRegisteredBuildingPartEvent = new RegisteredBuildingPartEvent();

        /// <summary>
        /// Event triggered when a Building Part is unregistered from the area.
        /// </summary>
        [Serializable]
        public class UnregisteredBuildingPartEvent : UnityEvent { }
        public UnregisteredBuildingPartEvent OnUnregisteredBuildingPartEvent = new UnregisteredBuildingPartEvent();

        #endregion

        #endregion

        #region Unity Methods

        void OnEnable()
        {
            if (BuildingManager.Instance != null)
            {
                BuildingManager.Instance.RegisterBuildingArea(this);
            }
        }

        void OnDisable()
        {
            if (BuildingManager.Instance != null)
            {
                BuildingManager.Instance.UnregisterBuildingArea(this);
            }
        }

        void OnDestroy()
        {
            if (BuildingManager.Instance != null)
            {
                BuildingManager.Instance.UnregisterBuildingArea(this);
            }
        }

        void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (m_Shape == ShapeType.SPHERE)
            {
                float scaledRadius = m_Radius * Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);

                Handles.color = !m_CanPlacingAnyBuildingParts ? Color.red / 4f : Color.cyan / 4f;
                Handles.DrawSolidArc(transform.position, transform.up, transform.right, 360f, scaledRadius);
                Handles.color = !m_CanPlacingAnyBuildingParts ? Color.red : Color.cyan;
                Handles.DrawWireDisc(transform.position, transform.up, scaledRadius);
            }
            else
            {
                Vector3 scaledSize = Vector3.Scale(m_Bounds.size, transform.localScale);
                Bounds scaledBounds = new Bounds(m_Bounds.center, scaledSize);

                Bounds bounds = MathExtension.GetWorldBounds(transform, scaledBounds);
                Gizmos.color = !m_CanPlacingAnyBuildingParts ? Color.red / 4f : Color.cyan / 4f;
                Gizmos.DrawCube(bounds.center, scaledBounds.size);
                Handles.color = !m_CanPlacingAnyBuildingParts ? Color.red : Color.cyan;
                Handles.DrawWireCube(bounds.center, scaledBounds.size);
            }
#endif
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Checks if a Building Part can be placed in this area.
        /// </summary>
        /// <param name="buildingPart">The Building Part to be placed.</param>
        /// <returns>True if the Building Part can be placed, false otherwise.</returns>
        public bool CanPlacingBuildingPart(BuildingPart buildingPart)
        {
            if (m_CanPlacingSpecificBuildingParts.Count == 0)
            {
                return false;
            }

            return m_CanPlacingSpecificBuildingParts.Find(entry => 
                entry.GetGeneralSettings.Identifier == buildingPart.GetGeneralSettings.Identifier);
        }

        /// <summary>
        /// Checks if a Building Part can be destroyed in this area.
        /// </summary>
        /// <param name="buildingPart">The Building Part to be destroyed.</param>
        /// <returns>True if the Building Part can be destroyed, false otherwise.</returns>
        public bool CanDestroyBuildingPart(BuildingPart buildingPart)
        {
            if (m_CanDestroyingSpecificBuildingParts.Count == 0)
            {
                return false;
            }

            return m_CanDestroyingSpecificBuildingParts.Find(entry => 
                entry.GetGeneralSettings.Identifier == buildingPart.GetGeneralSettings.Identifier);
        }

        /// <summary>
        /// Checks if a Building Part can be edited in this area.
        /// </summary>
        /// <param name="buildingPart">The Building Part to be edited.</param>
        /// <returns>True if the Building Part can be edited, false otherwise.</returns>
        public bool CanEditingBuildingPart(BuildingPart buildingPart)
        {
            if (m_CanEditingSpecificBuildingParts.Count == 0)
            {
                return false;
            }

            return m_CanEditingSpecificBuildingParts.Find(entry =>
                entry.GetGeneralSettings.Identifier == buildingPart.GetGeneralSettings.Identifier);
        }

        /// <summary>
        /// Register the Building Part in this area.
        /// </summary>
        /// <param name="buildingPart">The Building Part to register.</param>
        public void RegisterBuildingPart(BuildingPart buildingPart)
        {
            if (buildingPart == null)
            {
                return;
            }

            buildingPart.AttachedBuildingArea = this;

            m_RegisteredBuildingParts.Add(buildingPart);

            OnRegisteredBuildingPartEvent?.Invoke(buildingPart);
        }

        /// <summary>
        /// Unregister the Building Part from this area. 
        /// </summary>
        /// <param name="buildingPart">The Building Part to unregister.</param>
        public void UnregisterBuildingPart(BuildingPart buildingPart)
        {
            if (buildingPart == null)
            {
                return;
            }

            buildingPart.AttachedBuildingArea = null;

            m_RegisteredBuildingParts.Remove(buildingPart);

            OnUnregisteredBuildingPartEvent?.Invoke();
        }

        #endregion
    }
}