using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Inventory1))]
public class InventoryEditor1 : Editor {
	private SerializedProperty itemImagesProperty;
	private SerializedProperty itemsProperty;

	private bool[] showItemSlots = new bool[Inventory1.numItemSlots];

	private const string inventoryPropItemImagesName = "itemImages1";
	private const string inventoryPropItemsName = "items1";

	private void OnEnable(){

		itemImagesProperty = serializedObject.FindProperty (inventoryPropItemImagesName);
		itemsProperty = serializedObject.FindProperty (inventoryPropItemsName);
	}

	public override void OnInspectorGUI(){

		serializedObject.Update ();

		for(int i = 0; i < Inventory1.numItemSlots; i++){
			ItemSlotGUI (i);
		}

		serializedObject.ApplyModifiedProperties ();
	}

	private void ItemSlotGUI(int index){

		EditorGUILayout.BeginVertical (GUI.skin.box);
		EditorGUI.indentLevel++;

		showItemSlots [index] = EditorGUILayout.Foldout (showItemSlots[index], "Item Slot " + index);

		if (showItemSlots [index]) {
			EditorGUILayout.PropertyField (itemImagesProperty.GetArrayElementAtIndex(index));
			EditorGUILayout.PropertyField (itemsProperty.GetArrayElementAtIndex(index));
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical ();
	}
}
