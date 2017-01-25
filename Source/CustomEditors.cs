using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(PropertiesScript))]
[CanEditMultipleObjects]
public class PropertiesEditor : Editor
{
	SerializedProperty color;
	SerializedProperty container;
	SerializedProperty containerSound;
	SerializedProperty gammaDestructable;
	PropertiesScript ps;
	bool applicable;

	void OnEnable()
	{
		ps = target as PropertiesScript;
		color = serializedObject.FindProperty ("color");
		container = serializedObject.FindProperty ("container");
		containerSound = serializedObject.FindProperty ("containerSound");
		applicable = !(ps.gameObject.CompareTag ("Enemy") || ps.gameObject.CompareTag ("Chicken"));

		if (applicable)//enemies cannot be flagged destructable
		{
			gammaDestructable = serializedObject.FindProperty ("gammaDestructable");
		}
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField (color);
		EditorGUILayout.PropertyField (container);

		if(container.boolValue)
		{
			EditorGUILayout.PropertyField (containerSound);
		}

		if (applicable)
		{
			EditorGUILayout.PropertyField (gammaDestructable);
		}
			
		serializedObject.ApplyModifiedProperties();
	}

}
#endif
//[CustomEditor(typeof(EnemyPropertiesScript))]
//[CanEditMultipleObjects]
//public class EnemyPropertiesEditor : Editor
//{
//	SerializedProperty radioactive;
//	SerializedProperty soundproof;
//	//SerializedProperty sleepSprite;
//	//EnemyPropertiesScript ePS;
//
//	void OnEnable()
//	{
//		//ePS = target as EnemyPropertiesScript;
//		radioactive = serializedObject.FindProperty ("radioactive");
//		soundproof = serializedObject.FindProperty ("soundproof");
//		//sleepSprite = serializedObject.FindProperty ("sleepSprite");
//	}
//
//	public override void OnInspectorGUI()
//	{
//
//		serializedObject.Update();
//
//		EditorGUILayout.PropertyField(radioactive);
//		EditorGUILayout.PropertyField (soundproof);;
//
//		//if(soundproof.boolValue)
//		//{
//		//	EditorGUILayout.PropertyField (sleepSprite);// (serializedObject.FindProperty ("Transform"));
//		//}
//		serializedObject.ApplyModifiedProperties();
//		//}
//	}
//}