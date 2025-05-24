#if EBS_GAMECREATORV2_INVENTORY
using GameCreator.Runtime.Inventory;
using System;

using UnityEngine;

public class GameCreatorBuildingEntity : MonoBehaviour
{
    [Serializable]
    public class RequiredItemData
    {
        [SerializeField] Item m_Item;
        public Item Item { get { return m_Item; } }

        [SerializeField] int m_Amount;
        public int Amount { get { return m_Amount; } }
    }

    [SerializeField] RequiredItemData[] m_RequiredItems;
    public RequiredItemData[] RequiredItems { get { return m_RequiredItems; } }

    [SerializeField] RequiredItemData[] m_RefundItems;
    public RequiredItemData[] RefundItems { get { return m_RefundItems; } }
}
#endif