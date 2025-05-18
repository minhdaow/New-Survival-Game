using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Rotate In Place")]
    [Description("Rotates the transform in place towards the chosen target over time.")]

    [Image(typeof(IconRotation), ColorTheme.Type.Yellow, typeof(OverlayHourglass))]

    [Category("Transforms/Rotate In Place")]
    [Version(1, 0, 0)]

    [Parameter("Target", "The desired targeted object to look at")]
    [Parameter("Space", "If the transformation occurs in local or world space")]
    [Parameter("Duration", "How long it takes to perform the transition")]
    [Parameter("Easing", "The change rate of the rotation over time")]
    [Parameter("Wait to Complete", "Whether to wait until the rotation is finished or not")]

    [Keywords("Rotate", "Rotation", "See", "Look")]
    [Serializable]
    public class InstructionRotateInPlaceWithDuration : TInstructionTransform
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectTransform.Create();

        [Space]
        [SerializeField] private Space m_Space = Space.World;
        [SerializeField] private Transition m_Transition = new Transition();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"{this.m_Transform} rotate in place toward {this.m_Target} over {this.m_Transition.Duration}s";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override async Task Run(Args args)
        {
            GameObject gameObject = this.m_Transform.Get(args);
            if (gameObject == null) return;

            GameObject target = this.m_Target.Get(args);
            if (target == null) return;

            Vector3 lookDirection = (target.transform.position - gameObject.transform.position).normalized;
            lookDirection.y = 0; // Ensure the rotation is only on the Y axis

            Quaternion initialRotation = gameObject.transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            ITweenInput tween = new TweenInput<Quaternion>(
                initialRotation,
                targetRotation,
                this.m_Transition.Duration,
                (a, b, t) =>
                {
                    gameObject.transform.rotation = Quaternion.LerpUnclamped(a, b, t);
                },
                Tween.GetHash(typeof(Transform), "rotation"),
                this.m_Transition.EasingType,
                this.m_Transition.Time
            );

            Tween.To(gameObject, tween);
            if (this.m_Transition.WaitToComplete) await this.Until(() => tween.IsFinished);
        }
    }
}
