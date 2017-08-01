using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ButtonPopup))]
public class Edt_ButtonPopup : Editor 
{
	private ReorderableList listBtn;
	private ReorderableList listTxt;

	public void OnEnable() 
	{
		listBtn = new ReorderableList(serializedObject, serializedObject.FindProperty("m_listBtn"), true, true, true, true);
		listBtn.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			SerializedProperty element = listBtn.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("type"), GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, 160, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("button"), GUIContent.none);
		};
		listBtn.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Resources");
		};

		listTxt = new ReorderableList(serializedObject, serializedObject.FindProperty("m_listText"), true, true, true, true);
		listTxt.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			SerializedProperty element = listTxt.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("type"), GUIContent.none);
			EditorGUI.PropertyField(new Rect(rect.x + 70, rect.y, 160, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("text"), GUIContent.none);
		};
		listTxt.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Resources");
		};
	}

	public override void OnInspectorGUI() 
	{
		serializedObject.Update();
		listBtn.DoLayoutList();
		listTxt.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}
