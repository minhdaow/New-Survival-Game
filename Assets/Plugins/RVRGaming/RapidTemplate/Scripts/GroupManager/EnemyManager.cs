using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Perception;
using UnityEngine;
using GroupManager;

public class EnemyManager : MonoBehaviour
{
    public enum AggressionLevel
    {
        Mild,
        Moderate,
        Aggressive
    }

    [Header("Manager Settings")]
    [Space]
    public float detectionRadius = 10f;
    public AggressionLevel aggression = AggressionLevel.Aggressive;
    public List<EnemyCharacter> enemies = new List<EnemyCharacter>();
    private EnemyCharacter currentAttacker = null;
    [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
    [Space]

    [Header("Awareness Settings")]
    public bool enableAwareness = true;

    [Header("Communicate Between Managers")]
    [Space]
    public bool Communicate = true;
    public float CommunicationRange = 20f;
    public LayerMask CommunicationMask;
    private bool awarenessTriggered = false;
    private bool choosingNewAttacker = false;

    private void Awake()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= detectionRadius)
            {
                EnemyCharacter enemyCharacter = enemy.GetComponent<EnemyCharacter>();
                if (enemyCharacter != null)
                {
                    enemies.Add(enemyCharacter);
                }
            }
        }

        ChooseNewAttacker();
    }

    private void Update()
    {
        List<EnemyCharacter> enemiesToRemove = new List<EnemyCharacter>();

        foreach (EnemyCharacter enemy in enemies)
        {
            if (enemy.gameObject.tag == "Dead")
            {
                enemy.SetDead(true);
                enemiesToRemove.Add(enemy);
                continue;
            }

            if (enemy.IsDead)
            {
                enemiesToRemove.Add(enemy);
                continue;
            }

            if (enemy != currentAttacker)
            {
                enemy.SetMoving(true);
                enemy.SetAttacking(false);
            }

            if (currentAttacker != null && currentAttacker.IsAttacking)
            {
                if (enableAwareness)
                {
                    SetAwarenessForOtherEnemies();
                }

                if (!awarenessTriggered)
                {
                    awarenessTriggered = true;
                }
            }
        }

        foreach (EnemyCharacter deadEnemy in enemiesToRemove)
        {
            enemies.Remove(deadEnemy);
        }

        if (currentAttacker != null && (currentAttacker.IsDead || !currentAttacker.IsAttacking))
        {
            if (!choosingNewAttacker)
            {
                StartCoroutine(ChooseNewAttackerWithDelay());
            }
        }

        if (Communicate && awarenessTriggered)
        {
            CommunicateWithOtherManagers();
        }
    }

    private IEnumerator ChooseNewAttackerWithDelay()
    {
        choosingNewAttacker = true;

        switch (aggression)
        {
            case AggressionLevel.Mild:
                yield return new WaitForSeconds(1f);
                break;
            case AggressionLevel.Moderate:
                yield return new WaitForSeconds(0.5f);
                break;
            case AggressionLevel.Aggressive:
                // No delay
                break;
        }

        ChooseNewAttacker();
        choosingNewAttacker = false;
    }

    public void ChooseNewAttacker()
    {
        if (currentAttacker != null)
        {
            currentAttacker.SetAttacking(false);
        }

        List<EnemyCharacter> availableEnemies = enemies
            .Where(e => !e.IsDead)
            .ToList();

        if (availableEnemies.Count > 1)
        {
            availableEnemies = availableEnemies
                .Where(e => e != currentAttacker)
                .ToList();
        }

        if (availableEnemies.Count > 0)
        {
            EnemyCharacter newAttacker = availableEnemies[Random.Range(0, availableEnemies.Count)];
            if (newAttacker != null)
            {
                newAttacker.SetAttacking(true);
                newAttacker.SetMoving(false);
                currentAttacker = newAttacker;
            }
        }
    }


    private void SetAwarenessForOtherEnemies()
    {
        GameObject target = m_Target.Get(gameObject);

        if (target == null)
        {
            Debug.LogWarning("Target is null, awareness cannot be set");
            return;
        }

        foreach (EnemyCharacter enemy in enemies)
        {
            if (enemy != currentAttacker && !enemy.IsDead)
            {
                Perception perception = enemy.GetComponent<Perception>();
                if (perception != null)
                {
                    perception.SetAwareness(target, 1f);
                }
                else
                {
                    Debug.LogWarning("Enemy missing Perception component.");
                }
            }
        }
    }

    private void CommunicateWithOtherManagers()
    {
        EnemyManager[] otherManagers = FindObjectsByType<EnemyManager>(FindObjectsSortMode.None);

        foreach (EnemyManager otherManager in otherManagers)
        {
            if (otherManager == this) continue;

            float distance = Vector3.Distance(transform.position, otherManager.transform.position);

            if (distance <= CommunicationRange)
            {
                Vector3 direction = (otherManager.transform.position - transform.position).normalized;
                RaycastHit hitInfo;
                if (!Physics.Raycast(transform.position, direction, out hitInfo, distance, CommunicationMask))
                {
                    ShareAwarenessBetweenManagers(otherManager);
                }
            }
        }
    }

    private void ShareAwarenessBetweenManagers(EnemyManager otherManager)
    {
        if (!enableAwareness) return;

        GameObject target = m_Target.Get(gameObject);

        foreach (EnemyCharacter enemy in otherManager.enemies)
        {
            if (!enemy.IsDead)
            {
                Perception perception = enemy.GetComponent<Perception>();
                if (perception != null && target != null)
                {
                    perception.SetAwareness(target, 1f);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (Communicate)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, CommunicationRange);
        }
    }
}
