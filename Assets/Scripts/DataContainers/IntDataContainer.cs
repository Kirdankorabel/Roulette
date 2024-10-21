using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntDataContainer", menuName = "ScriptableObjects/IntDataContainer", order = 1)]
public class IntDataContainer : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<int> _values = new List<int>();

    public string Name => _name;
    public List<int> Values => _values;
}
