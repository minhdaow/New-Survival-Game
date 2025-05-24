#if EBS_GAMECREATORV2
using System;
using System.Threading.Tasks;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using EasyBuildSystem.Features.Runtime.Buildings.Manager.Saver;

namespace EasyBuildSystem.Packages.Integrations.GameCreatorV2
{
    [Category("Easy Build System/Game Creator 2/Building Saving/Load Building Parts")]
    [Description("Allows to load the last saved building parts.\n" +
        "Note: This required a Building Saver component in the scene.")]
    [Serializable]
    public class LoadBuildingParts : Instruction
    {
        protected override Task Run(Args args)
        {
            BuildingSaver.Instance.ForceLoad();
            return DefaultResult;
        }
    }
}
#endif