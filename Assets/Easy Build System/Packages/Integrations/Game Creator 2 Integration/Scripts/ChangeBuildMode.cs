#if EBS_GAMECREATORV2
using System;
using System.Threading.Tasks;

using UnityEngine;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using EasyBuildSystem.Features.Runtime.Buildings.Placer;

namespace EasyBuildSystem.Packages.Integrations.GameCreatorV2
{
    [Title("Change Building Mode")]
    [Category("Easy Build System/Game Creator 2/Building Placer/Change Build Mode")]
    [Description("Allows to select a specific build mode.")]
    [Serializable]
    public class ChangeBuildMode : Instruction
    {
        [SerializeField] BuildingPlacer.BuildMode m_BuildMode;

        protected override Task Run(Args args)
        {
            BuildingPlacer.Instance.ChangeBuildMode(m_BuildMode);
            return DefaultResult;
        }
    }
}
#endif