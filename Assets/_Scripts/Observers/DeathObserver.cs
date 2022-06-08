using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DeathObserver : IObserver
{
    public void Update(Unit unit)
    {
        unit.DeathSound.Play();
    }
}
