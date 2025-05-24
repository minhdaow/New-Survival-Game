#if EBS_GAMECREATORV2
using System;
using System.Threading.Tasks;

using UnityEngine;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;

namespace EasyBuildSystem.Packages.Integrations.GameCreatorV2
{
    [Category("Easy Build System/Game Creator 2/Building Placer/Select Building Part")]
    [Description("Allows to select a specific building parts.\n" +
        "Note: It is important that the reference that you use here is present in the Building Manager of the scene.")]
    [Serializable]
    public class SelectBuildingPart : Instruction
    {
        [SerializeField] BuildingPart m_BuildingPart;

        protected override Task Run(Args args)
        {
            if (BuildingPlacer.Instance == null)
            {
                Debug.LogWarning("BuildingPlacer not found. Make sure the system is correctly set up in the scene!");
                return DefaultResult;
            }

            BuildingPlacer.Instance.SelectBuildingPart(m_BuildingPart);

            return DefaultResult;
        }
    }
}
#endif