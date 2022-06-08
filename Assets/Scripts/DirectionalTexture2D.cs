using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DirectionalTexture2D
{
	//tooltip 14 unique textures
	public Texture2D noneTexture;
	public Texture2D allTexture;
	public Texture2D leftTexture;
	public Texture2D rightTexture;
	public Texture2D topTexture;
	public Texture2D bottomTexture;
	public Texture2D leftRightTexture;
	public Texture2D leftTopTexture;
	public Texture2D leftBottomTexture;
	public Texture2D rightTopTexture;
	public Texture2D rightBottomTexture;
	public Texture2D topBottomTexture;
	public Texture2D leftRightTopTexture;
	public Texture2D leftTopBottomTexture;
	public Texture2D leftRightBottomTexture;
	public Texture2D rightTopBottomTexture;
	public enum Sides { None, Left, Right, Top, Bottom }

	public Texture2D GetTexture(Sides sides)
	{
		if (sides == Sides.None) return noneTexture;
		if (sides == Sides.Left) return leftTexture;
		if (sides == Sides.Right) return rightTexture;
		if (sides == Sides.Top) return topTexture;
		if (sides == Sides.Bottom) return bottomTexture;

		if (sides == (Sides.Left & Sides.Right)) return leftRightTexture;
		if (sides == (Sides.Left & Sides.Top)) return leftTopTexture;
		if (sides == (Sides.Left & Sides.Bottom)) return leftBottomTexture;
		if (sides == (Sides.Right & Sides.Top)) return rightTopTexture;
		if (sides == (Sides.Right & Sides.Bottom)) return rightBottomTexture;
		if (sides == (Sides.Top & Sides.Bottom)) return topBottomTexture;

		if (sides == (Sides.Left & Sides.Right & Sides.Top)) return leftRightTopTexture;
		if (sides == (Sides.Left & Sides.Top & Sides.Bottom)) return leftTopBottomTexture;
		if (sides == (Sides.Left & Sides.Right & Sides.Bottom)) return leftRightBottomTexture;
		if (sides == (Sides.Right & Sides.Top & Sides.Bottom)) return rightTopBottomTexture;

		return allTexture;
	}
}
// #if UNITY_EDITOR

[CustomPropertyDrawer(typeof(DirectionalTexture2D))]
[CanEditMultipleObjects]
public class LookAtPointEditor : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		int indent = EditorGUI.indentLevel;

		Rect line = position;
		line.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.LabelField(line, "Environment textures");
		EditorGUI.indentLevel++;

		line.y += EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(line, property.FindPropertyRelative("noneTexture"), new GUIContent("no walls"));
		line.y += EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(line, property.FindPropertyRelative("allTexture"), new GUIContent("all walls"));


		SerializedProperty[] singleProperties = new SerializedProperty[] {
		property.FindPropertyRelative("leftTexture"),
		property.FindPropertyRelative("rightTexture"),
		property.FindPropertyRelative("topTexture"),
		property.FindPropertyRelative("bottomTexture"),
		};

		SerializedProperty[] doubleProperties = new SerializedProperty[] {
		property.FindPropertyRelative("leftRightTexture"),
		property.FindPropertyRelative("leftTopTexture"),
		property.FindPropertyRelative("leftBottomTexture"),
		property.FindPropertyRelative("rightTopTexture"),
		property.FindPropertyRelative("rightBottomTexture"),
		property.FindPropertyRelative("topBottomTexture"),
		};

		SerializedProperty[] tripleProperties = new SerializedProperty[] {
		property.FindPropertyRelative("leftRightTopTexture"),
		property.FindPropertyRelative("leftTopBottomTexture"),
		property.FindPropertyRelative("leftRightBottomTexture"),
		property.FindPropertyRelative("rightTopBottomTexture")
		};

		line.y += EditorGUIUtility.singleLineHeight;
		EditorGUI.LabelField(line, "Single Walled Sides");
		EditorGUI.indentLevel++;
		for (int i = 0; i < singleProperties.Length; i++)
		{
			line.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(line, singleProperties[i], new GUIContent(singleProperties[i].name));
		}
		EditorGUI.indentLevel--;

		line.y += EditorGUIUtility.singleLineHeight;
		EditorGUI.LabelField(line, "Double Sides");
		EditorGUI.indentLevel++;
		for (int i = 0; i < doubleProperties.Length; i++)
		{
			line.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(line, doubleProperties[i], new GUIContent(doubleProperties[i].name));
		}
		EditorGUI.indentLevel--;

		line.y += EditorGUIUtility.singleLineHeight;
		EditorGUI.LabelField(line, "Triple Sides");
		EditorGUI.indentLevel++;
		for (int i = 0; i < tripleProperties.Length; i++)
		{
			line.y += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(line, tripleProperties[i], new GUIContent(tripleProperties[i].name));
		}
		EditorGUI.indentLevel--;

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 30;
	}
}
