#if EBS_GAMECREATORV2
using System;
using System.Threading.Tasks;

using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

using EasyBuildSystem.Features.Runtime.Buildings.Manager.Saver;

namespace EasyBuildSystem.Packages.Integrations.GameCreatorV2
{
    [Category("Easy Build System/Game Creator 2/Building Saving/Save Building Parts")]
    [Description("Allows to save all the building parts present in the scene.\n" +
        "Note: This required a Building Saver component in the scene.")]
    [Serializable]
    public class SaveBuildingParts : Instruction
    {
        protected override Task Run(Args args)
        {
            BuildingSaver.Instance.ForceSave();
            return DefaultResult;
        }
    }
}
#endif