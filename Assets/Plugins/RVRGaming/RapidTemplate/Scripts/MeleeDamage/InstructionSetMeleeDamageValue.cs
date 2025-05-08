using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
using GameCreator.Runtime.Melee;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Set Melee Damage Value")]
    [Description("Sets the current value of the MeleeDamage component on a target GameObject to a specified value.")]
    [Category("Melee/Damage/Set Value")]
    [Parameter("Target", "The GameObject that has the MeleeDamage component")]
    [Parameter("Value", "The value to set as the current value")]
    [Keywords("Melee", "Damage", "Set", "Value", "Hit")]
    [Image(typeof(IconMeleeSkill), ColorTheme.Type.Pink)]
    public class InstructionSetMeleeDamageValue : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetDecimal m_Value = new PropertyGetDecimal(0f);

        public override string Title => $"Set Melee Damage Value to {this.m_Value} on {this.m_Target}";

        protected override async Task Run(Args args)
        {
            GameObject target = this.m_Target.Get(args);
            if (target == null) return;

            MeleeDamage meleeDamage = target.GetComponent<MeleeDamage>();
            if (meleeDamage == null) return;

            meleeDamage.currentValue = (float)this.m_Value.Get(args);
            await Task.Yield();
        }
    }
}
