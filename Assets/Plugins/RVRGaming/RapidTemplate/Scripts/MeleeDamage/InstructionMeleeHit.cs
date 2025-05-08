using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
using GameCreator.Runtime.Melee;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Apply Melee Hit")]
    [Description("Subtracts a hit from the Melee Damage component on a character")]
    [Category("Melee/Damage/Apply Hit")]
    [Parameter("Target", "The GameObject that has the MeleeDamage component")]
    [Parameter("Hit Amount", "The amount to subtract from the current value (default is 1)")]
    [Keywords("Melee", "Damage", "Hit")]
    [Image(typeof(IconMeleeSkill), ColorTheme.Type.Red)]
    public class InstructionMeleeHit : Instruction
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private float m_HitAmount = 1f;

        public override string Title => $"Apply Melee Hit (-{m_HitAmount}) on {this.m_Target}";

        protected override async Task Run(Args args)
        {
            GameObject target = this.m_Target.Get(args);
            if (target == null) return;

            MeleeDamage meleeDamage = target.GetComponent<MeleeDamage>();
            if (meleeDamage == null) return;

            meleeDamage.TakeHit(m_HitAmount);

            await Task.Yield();
        }
    }
}
