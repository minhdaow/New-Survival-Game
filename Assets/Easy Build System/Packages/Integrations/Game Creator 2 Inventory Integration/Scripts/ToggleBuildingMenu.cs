#if EBS_GAMECREATORV2_INVENTORY
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;
using System.Threading.Tasks;

[Category("Easy Build System/Game Creator 2 - Inventory 2/Toggle Building Item")]
[Description("Allows to use a item to a specific building parts.\n" +
    "Note: It is important that the reference that you use here is present in the Building Manager of the scene.")]
[Serializable]
public class ToggleBuildingMenu : Instruction
{
    protected override Task Run(Args args)
    {
        if (UICircularBuildingMenuForGCInventory.Instance.IsOpened)
        {
            UICircularBuildingMenuForGCInventory.Instance.CloseMenu();
        }
        else
        {
            UICircularBuildingMenuForGCInventory.Instance.OpenMenu();
        }

        return DefaultResult;
    }
}
#endif