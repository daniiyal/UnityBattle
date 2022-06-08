using System;
using System.Collections;
using UnityEngine;

namespace LibraryUnits
{
    public interface ILibraryUnit
    {

        public Stats Stats { get; set; }

        public GameObject Prefab { get; }

        public Tile currentTile { get; set; }

        public ScriptableUnit ScriptableUnit { get; set; }

        public Vector3 Position { get; set; }

        public Faction Faction { get; }

        public Animator Animator { get; set; }

        bool isAlive { get; }
        bool isDamaged { get; }

        void GetSpanking(int damage);
        public void Croak();
        public void InitializeTile(GameObject unit);
    }
}
