using System.Collections;
using UnityEngine;

public interface IUnit
{
    public Stats Stats { get; set; }

    public GameObject Prefab { get; }

    public Tile currentTile { get; set; }

    public ScriptableUnit ScriptableUnit { get; set; }

    public Vector3 Position { get; set; }

    public Faction Faction { get; }

    public Animator Animator { get; set; }

    bool isAlive { get; }

    void TakeDamage(int damage);
    public void Die();
    public void InitializeTile(GameObject unit);
}

