#if EBS_GAMECREATORV2
using System;
using System.Threading.Tasks;

using UnityEngine;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Manager;

namespace EasyBuildSystem.Packages.Integrations.GameCreatorV2
{
    [Category("Easy Build System/Game Creator 2/Building Manager/Place Building Part")]
    [Description("Allows to place a specific building part at position, rotation, scale.")]
    [Serializable]
    public class PlaceBuildingPart : Instruction
    {
        [SerializeField] BuildingPart m_BuildingPart;
        [SerializeField] Vector3 m_Position;
        [SerializeField] Vector3 m_Rotation;
        [SerializeField] Vector3 m_Scale;

        protected override Task Run(Args args)
        {
            BuildingManager.Instance.PlaceBuildingPart(m_BuildingPart, m_Position, m_Rotation, m_Scale, true);
            return DefaultResult;
        }
    }
}
#endif