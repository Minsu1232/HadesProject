using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerClass")]
public class PlayerClassData : ScriptableObject
{
    public int initialHp;
    public int initialMp;
    public int initialDeffense;
    public int initialAttackPower;
    public int initialAttackSpeed;
    public int initialSpeed;

    public ClassType classType;
 
    
   public enum ClassType {

        Warrior,
        Magician

    }

    
   

}
