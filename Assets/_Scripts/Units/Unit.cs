using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit, IObservable
{
    [SerializeField] private ScriptableUnit scriptableUnit;
    [SerializeField] private Tile _currentTile;

    [SerializeField] private Animator animator;

    public AudioSource DeathSound;

    public List<IObserver> observers = new List<IObserver>();

    public Stats Stats { get; set; }

    public void SetStats(Stats stats) => Stats = stats;

    public void TakeDamage(int damage)
    {
        var _stats = Stats;

        _stats.Health -= damage;
        SetStats(_stats);

    }

    public void Die()
    {
        animator.SetTrigger("Die");
        Notify();
        StartCoroutine(OnCompleteDieAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
    }

    IEnumerator OnCompleteDieAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
        _currentTile.Unit = null;
    }


    public bool isAlive => Stats.Health > 0;

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

    public Faction Faction => scriptableUnit.Faction;


    public Animator Animator { 
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

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            
            observer.Update(this);
        }
    }
}