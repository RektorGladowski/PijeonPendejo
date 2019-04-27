using UnityEngine;
using UnityEngine.Events;

namespace Variables._Definitions
{
    public class Variable<T> : ScriptableObject
    {
        [SerializeField] private T _value;

        public T Value
        {
            get
            {
                return _value;

            }
            set
            {
                _value = value;
                ValueChangedEvent.Invoke();
            }
        }

        public UnityEvent ValueChangedEvent;
    }
}
