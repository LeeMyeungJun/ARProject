using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "DefenceGameWave/Scriptable", order = 1)]
public class EnemyWaveConfig : ScriptableObject
{
    public int spawnCnt = 0;
    public bool isBoss; 
    public EnemyType[] enemys; 
}
