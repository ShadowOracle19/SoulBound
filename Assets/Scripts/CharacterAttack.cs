using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterAttack", order = 1)]
public class CharacterAttack : ScriptableObject
{
    public string attackName;
    public string attackDescription;
}
