using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;
using GroupManager;
using GameCreator.Runtime.VisualScripting;

[Title("Rescan For Enemies")]
[Description("Scans the scene for enemies and updates the Enemy Manager's list")]

[Category("Enemy Manager/Rescan Enemies")]

[Parameter("EnemyManager", "The Enemy Manager object to update")]
[Keywords("Enemy", "Scan", "Rescan", "Manager", "Look for enemies")]
[Image(typeof(IconSearch), ColorTheme.Type.Yellow)]
[Serializable]
public class InstructionManagerRescan : Instruction
{
    [SerializeField] private PropertyGetGameObject m_EnemyManager = new PropertyGetGameObject();

    public override string Title => $"Rescan Enemies on {this.m_EnemyManager}";

    protected override async Task Run(Args args)
    {
        GameObject managerObj = this.m_EnemyManager.Get(args);
        if (managerObj == null)
        {
            Debug.LogWarning("EnemyManager object is null");
            return;
        }

        EnemyManager enemyManager = managerObj.GetComponent<EnemyManager>();
        if (enemyManager == null)
        {
            Debug.LogWarning("No EnemyManager component found on the object");
            return;
        }

        enemyManager.enemies.Clear();

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObj in allEnemies)
        {
            float distance = Vector3.Distance(managerObj.transform.position, enemyObj.transform.position);
            if (distance <= enemyManager.detectionRadius)
            {
                EnemyCharacter enemyCharacter = enemyObj.GetComponent<EnemyCharacter>();
                if (enemyCharacter != null)
                {
                    enemyManager.enemies.Add(enemyCharacter);
                }
            }
        }

        enemyManager.ChooseNewAttacker();

        await Task.Yield();
    }
}
