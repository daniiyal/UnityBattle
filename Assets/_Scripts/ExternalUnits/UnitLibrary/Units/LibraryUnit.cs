using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LibraryUnits
{
    public class LibraryUnit : MonoBehaviour, ILibraryUnit
    {
        [SerializeField] private ScriptableUnit scriptableUnit;
        [SerializeField] private Tile _currentTile;

        [SerializeField] private Animator animator;


        public void GetSpanking(int damage)
        {
            var _stats = Stats;

            _stats.Health -= damage;
            Stats = _stats;

        }

        public void Croak()
        {
            animator.SetTrigger("Die");
            StartCoroutine(OnCompleteDieAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
        }

        IEnumerator OnCompleteDieAnimation(float time)
        {
            yield return new WaitForSeconds(time);
            this.gameObject.SetActive(false);
            _currentTile.Unit = null;
        }


        public bool isAlive => Stats.Health > 0;

        public bool isDamaged => Stats.Health < Stats.MaxHealth;

        public GameObject Prefab => ScriptableUnit.UnitPrefab;

        public Tile currentTile
        {
            get
            {
                return _currentTile;
            }
            set
            {
                _currentTile = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public Faction Faction => ScriptableUnit.Faction;


        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }

        public ScriptableUnit ScriptableUnit
        {
            get
            {
                return scriptableUnit;
            }
            set
            {
                scriptableUnit = value;
            }
        }

        public Stats Stats { get; set;}

        public void InitializeTile(GameObject unit)
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider.GetComponent<Tile>())
                {
                    transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.1f, hit.transform.position.z);
                    var tile = hit.collider.GetComponent<Tile>();
                    tile.Unit = unit;
                    _currentTile = tile;

                    //_gridInteractor = tile.GridInteractor;

                }
            }

        }
    }
}