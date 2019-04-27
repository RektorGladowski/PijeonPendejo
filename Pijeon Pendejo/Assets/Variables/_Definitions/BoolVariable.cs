using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Variables._Definitions
{
  [CreateAssetMenu]
  public class BoolVariable : Variable<bool>
  {

  }

#if UNITY_EDITOR
  [CustomEditor(typeof(BoolVariable))]
  public class BoolVariableEditor : Editor
  {
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      BoolVariable myScript = (BoolVariable)target;
      if (GUILayout.Button("Invoke"))
      {
        myScript.ValueChangedEvent.Invoke();
      }
    }
  }

#endif
}
