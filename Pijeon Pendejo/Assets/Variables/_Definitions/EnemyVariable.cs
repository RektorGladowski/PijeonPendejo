using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class EnemyVariable : ScriptableObject
{
    public GameObject Prefab;
    [Tooltip("The percentage probability that this prefab will be spawned")]
    public uint SpawnProbability;

#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyVariable))]
    public class TournamentInfoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EnemyVariable myScript = (EnemyVariable)target;
        }
    }

#endif
}
