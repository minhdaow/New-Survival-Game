#if EBS_GAMECREATORV2_INVENTORY
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System.Threading.Tasks;
using UnityEngine;
using System;
using GameCreator.Runtime.Inventory;
using GameCreator.Runtime.Characters;
using EasyBuildSystem.Features.Runtime.Buildings.Placer;
using EasyBuildSystem.Features.Runtime.Buildings.Part;
using EasyBuildSystem.Features.Runtime.Buildings.Manager;

[Category("Easy Build System/Game Creator 2 - Inventory 2/Select Building Item")]
[Description("Allows to use a item to a specific building parts.\n" +
    "Note: It is important that the reference that you use here is present in the Building Manager of the scene.")]
[Serializable]
public class SelectBuildingItem : Instruction
{
    [SerializeField] private BuildingPart m_Part;

    [SerializeField] private PropertyGetItem m_Item = new PropertyGetItem();
    [SerializeField] private PropertyGetGameObject m_Bag = GetGameObjectPlayer.Create();

    protected override Task Run(Args args)
    {
        Item item = m_Item.Get(args);
        if (item == null) return DefaultResult;

        Bag bag = this.m_Bag.Get<Bag>(args);
        if (bag == null) return DefaultResult;

        BuildingPlacer.Instance.SelectBuildingPart(m_Part);
        BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.PLACE);

        BuildingPlacer.Instance.OnChangedBuildModeEvent.AddListener((BuildingPlacer.BuildMode mode) => 
        {
            BuildingManager.Instance.OnPlacingBuildingPartEvent.RemoveAllListeners();
        });

        BuildingManager.Instance.OnPlacingBuildingPartEvent.RemoveAllListeners();
        BuildingManager.Instance.OnPlacingBuildingPartEvent.AddListener((BuildingPart part) => 
        {
            bag.Content.RemoveType(item);

            if (bag.Content.CountType(item) == 0)
            {
                BuildingPlacer.Instance.ChangeBuildMode(BuildingPlacer.BuildMode.NONE);
            }
        });

        return DefaultResult;
    }
}
#endif