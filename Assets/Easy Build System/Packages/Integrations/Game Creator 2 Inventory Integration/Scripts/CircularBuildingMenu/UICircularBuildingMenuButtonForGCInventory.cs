#if EBS_GAMECREATORV2_INVENTORY
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICircularBuildingMenuButtonForGCInventory : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] RawImage m_UIIconImage;
    public RawImage UIIconImage { get { return m_UIIconImage; } }

    [SerializeField] string m_UIText;
    public string UIText { get { return m_UIText; } set { m_UIText = value; } }

    [SerializeField] string m_UIDescriptionText;
    public string UIDescriptionText { get { return m_UIDescriptionText; } set { m_UIDescriptionText = value; } }

    [SerializeField] UnityEvent m_Action;
    public UnityEvent Action { get { return m_Action; } set { m_Action = value; } }

    public void SetButton(UICircularBuildingMenuForGCInventory.CircularButtonSettings circularButton)
    {
        if (circularButton.Icon != null && circularButton.Icon is Texture2D)
        {
            m_UIIconImage.texture = circularButton.Icon;
        }

        m_UIText = circularButton.Name;

        if (circularButton.BuildingEntity != null && 
            circularButton.BuildingEntity.RequiredItems != null && 
            circularButton.BuildingEntity.RequiredItems.Length != 0)
        {
            for (int i = 0; i < circularButton.BuildingEntity.RequiredItems.Length; i++)
            {
                if (circularButton.BuildingEntity.RequiredItems[i].Item != null)
                {
                    if (UICircularBuildingMenuForGCInventory.Instance == null)
                    {
                        continue;
                    }

                    string color = "red";

                    bool hasEnought = UICircularBuildingMenuForGCInventory.Instance.HasRequiredItem(circularButton.BuildingEntity.RequiredItems[i]);

                    if (hasEnought)
                    {
                        color = "#90FF4D";
                    }
                    else
                    {
                        color = "#FF4D4D";
                    }

                    m_UIDescriptionText += "<color=" + color + ">Require " + circularButton.BuildingEntity.RequiredItems[i].Item.name
                        + " x" + circularButton.BuildingEntity.RequiredItems[i].Amount + "</color>\n";
                }
            }
        }
        else
        {
            m_UIDescriptionText = circularButton.Description;
        }

        m_Action = circularButton.Action;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_Action.Invoke();
    }
}
#endif