using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Knight : Unit, IImprovable
{

    private void Awake()
    {
        improvements = new List<ImprovementType>();
        improvementGameObjects = new List<GameObject>();
    }
    public List<ImprovementType> improvements { get; set; }
    public List<GameObject> improvementGameObjects { get; set; }

    public ImprovementType ImprovementType => throw new NotImplementedException();

    public IUnit Unit => throw new NotImplementedException();

    public bool CanImprove(ImprovementType type)
    {
        foreach (var item in improvements)
        {
            if(item == type)
            {
                return false;
            }
        }

        return true;
    }

    public IUnit Clone()
    {
        var clonedUnit = (IUnit)MemberwiseClone();
        return clonedUnit;
    }


  
}