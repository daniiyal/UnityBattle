using LibraryUnits;

public class LibraryUnitAdapter : LibraryUnit, IUnit
{
    private LibraryUnit libraryUnit;

    private void Awake()
    {
        libraryUnit = gameObject.GetComponent<LibraryUnit>();
    }

    public void TakeDamage(int damage)
    {
        libraryUnit.GetSpanking(damage);
    }

    public void Die()
    {
        libraryUnit.Croak();
    }

}