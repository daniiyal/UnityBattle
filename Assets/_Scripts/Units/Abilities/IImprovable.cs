using System;
using System.Collections.Generic;
using UnityEngine;

public interface IImprovable
{
    List<ImprovementType> improvements { get; set; }
    List<GameObject> improvementGameObjects { get; set; }
    bool CanImprove(ImprovementType type);
}
