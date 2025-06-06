#if UNITY_EDITOR && SORTIFY_ATTRIBUTES
using UnityEditor;
using UnityEngine;

namespace Sortify
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use TagSelector with string.");
            }
        }
    }
}
#endif
