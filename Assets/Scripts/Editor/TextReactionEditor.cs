//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//
//[CustomEditor(typeof(TextReaction))]
//public class TextReactionEditor : ReactionEditor {
//
//	private SerializedProperty messageProperty;
//	private SerializedProperty textColorProperty;
//	private SerializedProperty delayProperty;
//
//	private const string textReactionpropMessageName = "message";
//	private const string textReactionpropTextColorName = "textColor";
//	private const string textReactionpropDelayName = "delay";
//	private const float areaWidthOffset = 19f;
//	private const float messageGUILines = 3f;
//
//	protected override void Init(){
//	
//		messageProperty = serializedObject.FindProperty (textReactionpropMessageName);
//		textColorProperty = serializedObject.FindProperty (textReactionpropTextColorName);
//		delayProperty = serializedObject.FindProperty (textReactionpropDelayName);
//	}
//
//	protected override void DrawReaction(){
//		EditorGUILayout.BeginHorizontal ();
//		EditorGUILayout.LabelField ("Message", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
//		messageProperty.stringValue = EditorGUILayout.TextArea (messageProperty.stringValue,
//			GUILayout.Height(EditorGUIUtility.singleLineHeight * messageGUILines));
//
//		EditorGUILayout.EndHorizontal ();
//
//		EditorGUILayout.PropertyField (textColorProperty);
//		EditorGUILayout.PropertyField (delayProperty);
//	}
//
//	protected override string GetFoldoutLabel(){
//		return "Text Reaction";
//	}
//}
