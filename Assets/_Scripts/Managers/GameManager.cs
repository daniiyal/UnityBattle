using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour // Синглтон
{
    public static GameManager Instance;

    public GridManager gridManager;
    public GridInteractor gridInteractor;

    public UnitManager unitManager;

    public CommandManager commandManager;

    List<IObserver> observers;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }


    void Start() => ChangeState(GameState.StartGame);


    public IStrategy Strategy = DataHolder.Strategy;
    public IArmy LeftArmy { get; private set; }
    public IArmy RightArmy { get; private set; }

    public VictoryScreen VictoryScreen;
    public GameState State { get; private set; }

    public void ChangeState(GameState newState)
    {
        State = newState;
        Debug.Log(State);
        switch (newState)
        {
            case GameState.StartGame:
                HandleStarting();
                break;
            case GameState.SpawnLeftUnits:
                HandleSpawningLeftUnits();
                break;
            case GameState.SpawnRightUnits:
                HandleSpawningRightUnits();
                break;
            case GameState.NextTurn:
                break;
            case GameState.Decide:
                Decide();
                break;
            case GameState.WinScreen:
                ShowVictoryScreen();
                break;
            default:
                break;


        }
    }


    private void HandleStarting()
    {
        gridManager.GenerateGrid();

        observers = new List<IObserver>
            {
                new DeathObserver()
            };

        commandManager = new CommandManager();
        ChangeState(GameState.SpawnLeftUnits);
    }


    private void HandleSpawningLeftUnits()
    {
        //Создать левые юниты
        var factory = new ArmyFactory();

        LeftArmy = new Army(Faction.Left, factory, DataHolder.money);

        LeftArmy.unitGameObjects = Strategy.spawnUnits(LeftArmy);

        AddObservers(LeftArmy, observers);

        ChangeState(GameState.SpawnRightUnits);
    }

    private void HandleSpawningRightUnits()
    {
        //Создать правые юниты
        var factory = new ArmyFactory();

        RightArmy = new Army(Faction.Right, factory, DataHolder.money);
        RightArmy.unitGameObjects = Strategy.spawnUnits(RightArmy);

        AddObservers(RightArmy, observers);

        ChangeState(GameState.NextTurn);
    }


    public void NextTurn()
    {
        MeleeAttack();
        SpecialAbilityActions();
        CollectDeadUnits();

        commandManager.EndTurn();

        ChangeState(GameState.Decide);
    }


    private void Decide()
    {
        if (LeftArmy.IsAllDead || RightArmy.IsAllDead)
        {
            ChangeState(GameState.WinScreen);
        }


    }

    private void SpecialAbilityActions()
    {
        var queue = Strategy.GetUnitsWithSpecialAbility(LeftArmy, RightArmy);

        foreach (var unit in queue)
        {
            unit.GetComponent<ISpecialAbility>().DoSpecialAction();
        }
    }

    private void CollectDeadUnits()
    {
        LeftArmy.CollectDeadUnits();
        RightArmy.CollectDeadUnits();
    }

    private void MeleeAttack()
    {
        var queue = Strategy.MeleeAttackOpponentQueue(LeftArmy, RightArmy);

        foreach (var opponent in queue)
        {
            Attack(opponent.LeftUnit);
            Attack(opponent.RightUnit);
        }
    }

    private void Attack(GameObject unitGameObject)
    {
        var unit = unitGameObject.GetComponent<IUnit>();

        var currentTile = unit.currentTile;
        var targetTile = GetTargetTile(unit);

        ICommand command;
        if (targetTile.Unit != null)
        {
            var targetUnit = targetTile.Unit.GetComponent<IUnit>();

            command = new AttackCommand(unit, targetUnit, unit.Stats.AttackPower);
            commandManager.Execute(command);

            if (targetUnit.isAlive && targetUnit is IImprovable && ((IImprovable)targetUnit).improvements.Count > 0)
            {
                System.Random rand = new System.Random();
                if (rand.Next(1) == 0)
                {
                    command = new RemoveImprovementCommand(targetTile.Unit);
                    commandManager.Execute(command);
                }
            }
        }

        else
        {
            command = new TravelCommand(unitGameObject, currentTile, targetTile);
            commandManager.Execute(command);
        }

    }

    private Tile GetTargetTile(IUnit unit)
    {

        var currentTile = unit.currentTile;
        var currentTileIndex = gridInteractor.GridRepository.Tiles.IndexOf(currentTile);

        Tile targetTile;

        if (unit.Faction == Faction.Left)
            targetTile = gridInteractor.GridRepository.Tiles[currentTileIndex - 9 * unit.Stats.TravelDistance];
        else
            targetTile = gridInteractor.GridRepository.Tiles[currentTileIndex + 9 * unit.Stats.TravelDistance];


        return targetTile;
    }




    private void ShowVictoryScreen()
    {
        if (LeftArmy.IsAllDead)
            VictoryScreen.Setup("ПРАВЫЕ");
        else
            VictoryScreen.Setup("ЛЕВЫЕ");
    }


    private void AddObservers(IArmy army, List<IObserver> observers)
    {
        foreach (var unit in army.unitGameObjects)
            if (unit.GetComponent<Unit>() is IObservable observableUnit)
                foreach (var observer in observers)
                    observableUnit.Attach(observer);
    }


}


public enum GameState
{
    StartGame,
    SpawnLeftUnits,
    SpawnRightUnits,
    NextTurn,
    Decide,
    WinScreen
}