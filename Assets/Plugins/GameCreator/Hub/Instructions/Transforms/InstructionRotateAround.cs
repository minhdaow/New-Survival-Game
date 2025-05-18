using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.Audio;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 1)]
[Title("Rotate Around")]
[Description("One object is rotating around another object at a certain radius")]

[Image(typeof(IconRotation), ColorTheme.Type.Yellow)]

[Category("Transforms/Rotate Around")]

[Parameter("Target", "The desired rotation of the game object")]
[Parameter("GameObject", "Reference surround")]
[Parameter("Radius", "Circumferential radius")]
[Parameter("Speed", "Circling velocity")]

[Keywords("Rotate", "Angle", "Euler", "Tilt", "Pitch", "Yaw", "Roll")]
[Serializable]
public class InstructionRotateAround : Instruction
{
    [SerializeField] private PropertyGetGameObject m_Target = new PropertyGetGameObject();
    [SerializeField] private PropertyGetGameObject m_GameObject = new PropertyGetGameObject();
    [SerializeField] private PropertyGetDecimal m_Radius = new PropertyGetDecimal();
    [SerializeField] private PropertyGetDecimal m_Speed = new PropertyGetDecimal();

    protected override Task Run(Args args)
    {
        if (this.m_Target == null ||
            this.m_GameObject == null ||
            this.m_Radius == null ||
            this.m_Speed == null) return DefaultResult;

        float angle = (float)this.m_Speed.Get(args)*UnityEngine.Time.time;
        Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * (float)this.m_Radius.Get(args);
        this.m_GameObject.Get(args).transform.position = this.m_Target.Get(args).transform.position + newPosition;
        this.m_GameObject.Get(args).transform.LookAt(this.m_Target.Get(args).transform);
        return DefaultResult;
    }
}
