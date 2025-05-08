using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 1)]

    [Title("Remove Prop From Bone")]
    [Description("Removes a prefab Prop (if any) from a Character")]

    [Category("Characters/Visuals/Remove Prop From Bone")]

    [Parameter("Character", "The character target")]
    [Parameter("Bone", "The bone where the prefab needs to be removed")]

    [Keywords("Characters", "Detach", "Let", "Sheathe", "Put", "Holster", "Object")]
    [Image(typeof(IconTennis), ColorTheme.Type.TextNormal, typeof(OverlayMinus))]

    [Serializable]
    public class InstructionCharacterRemovePropFromBone : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();

        [Space]
        [SerializeField] private Bone m_Bone = new Bone(HumanBodyBones.RightHand);

        public override string Title => $"Remove Prop from {this.m_Bone} of {this.m_Character}";

        protected override Task Run(Args args)
        {
            Character character = this.m_Character.Get<Character>(args);
            if (character == null) return DefaultResult;

            if (!character.Props.HasAtBone(this.m_Bone)) return DefaultResult;

            character.Props.RemoveAtBone(this.m_Bone);
            return DefaultResult;
        }
    }
}