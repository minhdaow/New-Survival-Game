using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Title("On Remove Prop")]
[Version(0,0,1)]
[Category("Characters/Visuals/Remove Prop")]
[Description("Executed when a Prop is removed from a Character")]
[Image(typeof(IconTennis), ColorTheme.Type.TextLight, typeof(OverlayPlus))]
[Keywords("Characters", "Remove", "Let", "Sheathe", "Put", "Holster", "Object")]
[Serializable]
public class EventCharacterOnRemovePropInstance : TEventCharacter
{
    [NonSerialized] private Character m_CachedCharacter;

    protected override void WhenEnabled(Trigger trigger, Character character)
    {
        this.m_CachedCharacter = character;
        character.Props.EventRemove += this.OnRemove;
    }

    protected override void WhenDisabled(Trigger trigger, Character character)
    {
        this.m_CachedCharacter = character;
        character.Props.EventRemove -= this.OnRemove;
    }

    private void OnRemove(Transform propBone)
    {
        GameObject character = this.m_CachedCharacter.gameObject;
        if (character == null) return;
        _ = this.m_Trigger.Execute(propBone.gameObject);
    }
}