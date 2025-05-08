using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using GameCreator.Runtime.Characters;

namespace GameCreator.Runtime.Inventory
{
    [Version(1, 0, 1)]
    
    [Title("Use Item")]
    [Description("Uses an Item from the a Bag")]

    [Category("Inventory/Use Item")]
    
    [Parameter("Item", "The Item to be used")]
    [Parameter("Bag", "The targeted Bag component")]

    [Keywords("Bag", "Inventory", "Consumable")]
    [Keywords("Use", "Activate", "Consume")]
    
    [Dependency("inventory", 2, 1, 3)]

    [Image(typeof(IconItem), ColorTheme.Type.Green, typeof(OverlayTick))]
    
    [Serializable]
    public class InstructionInventoryUseItem : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetItem m_Item = new PropertyGetItem();
        [SerializeField] private PropertyGetGameObject m_Bag = GetGameObjectPlayer.Create();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Use {this.m_Item} from {this.m_Bag}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            Item item = this.m_Item.Get(args);
            if (item == null) return DefaultResult;
            
            Bag bag = this.m_Bag.Get<Bag>(args);
            if (bag == null) return DefaultResult;

            RuntimeItem runtimeItem = bag.Content.FindRuntimeItem(item);
            if (string.IsNullOrEmpty(runtimeItem.RuntimeID.String)) return DefaultResult;

            bag.Content.Use(runtimeItem);
            return DefaultResult;
        }
    }
}