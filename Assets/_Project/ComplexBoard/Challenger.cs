using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Challenger", fileName = "Challenger", order = 0)]
public class Challenger : ScriptableObject
{
    [SerializeField] private MapLayout map;
    [SerializeField] private List<ChallengerAttributes> attributes = new();
    [SerializeField] private ChallengerLevel level;
}
public enum ChallengerLevel
{
    Small,
    Medium,
    Boss
}

public class ChallengerAttributes
{
    
}