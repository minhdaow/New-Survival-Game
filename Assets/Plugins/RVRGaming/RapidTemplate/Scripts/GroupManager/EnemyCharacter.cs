using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;

namespace GroupManager
{
    public class EnemyCharacter : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private bool isDead;
        [SerializeField] private bool isMoving;
        [SerializeField] private bool isAttacking;

        [Header("Pre-Run Conditions")]
        [SerializeField] public RunConditionsList onCheck = new RunConditionsList();

        [Header("IsAttacking: On")]
        [SerializeField] public InstructionList onOn = new InstructionList();
        [Header("IsAttacking: Off")]
        [SerializeField] public InstructionList onOff = new InstructionList();

        [Header("IsMoving: On")]
        [SerializeField] public InstructionList onOn1 = new InstructionList();
        [Header("IsMoving: Off")]
        [SerializeField] public InstructionList onOff1 = new InstructionList();

        [Header("IsDead: On")]
        [SerializeField] public InstructionList onOn3 = new InstructionList();

        public bool IsDead { get => isDead; private set => isDead = value; }
        public bool IsMoving { get => isMoving; private set => isMoving = value; }
        public bool IsAttacking { get => isAttacking; private set => isAttacking = value; }

        private bool CheckCondition()
        {
            return onCheck.Check(new Args(this.gameObject));
        }

        public void SetDead(bool state)
        {
            if (!CheckCondition()) return;
            if (IsDead != state)
            {
                IsDead = state;

                if (state)
                {
                    _ = this.onOn3.Run(new Args(this.gameObject));
                }
            }
        }

        public void SetMoving(bool state)
        {
            if (IsDead) return; // Prevent moving if dead
            if (!CheckCondition()) return;
            bool prevState = IsMoving;
            IsMoving = state;
            if (prevState != state)
            {
                if (state) _ = this.onOn1.Run(new Args(this.gameObject));
                else _ = this.onOff1.Run(new Args(this.gameObject));
            }
        }

        public void SetAttacking(bool state)
        {
            if (IsDead) return; // Prevent attacking if dead
            if (!CheckCondition()) return;
            bool prevState = IsAttacking;
            IsAttacking = state;
            if (prevState != state)
            {
                if (state)
                {
                    _ = this.onOn.Run(new Args(this.gameObject));
                    SetMoving(false);
                }
                else _ = this.onOff.Run(new Args(this.gameObject));
            }
        }
    }
}
