using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Variables._Definitions
{
    [CreateAssetMenu]
    public class GOVariable : Variable<GameObject>
    {

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GOVariable))]
    public class GOVariableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GOVariableEditor myScript = (GOVariableEditor)target;
        }
    }

#endif
}
