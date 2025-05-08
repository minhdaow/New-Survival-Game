using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Characters;
using UnityEngine;
using GameCreator.Runtime.Melee;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Compare Melee Damage Value")]
    [Description("Returns true if the comparison between a number and the current value of the MeleeDamage component is satisfied")]

    [Category("Melee/Compare Current Value")]

    [Keywords("Melee", "Damage", "Hit", "Current Value")]
    [Image(typeof(IconMeleeSkill), ColorTheme.Type.Blue)]
    [Serializable]
    public class ConditionMeleeDamageCompareCurrentValue : Condition
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private CompareDouble m_Comparison = new CompareDouble(3f);

        protected override string Summary => $"Current Value of {this.m_Target} {this.m_Comparison}";

        protected override bool Run(Args args)
        {
            GameObject target = this.m_Target.Get(args);
            if (target == null) return false;

            MeleeDamage meleeDamage = target.GetComponent<MeleeDamage>();
            if (meleeDamage == null) return false;

            return this.m_Comparison.Match(meleeDamage.currentValue, args);
        }
    }
}
