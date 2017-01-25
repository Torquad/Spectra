//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//
//[CustomEditor(typeof(EnemyPropertiesScript))]
//[CanEditMultipleObjects]
//public class EnemyPropertiesEditor : Editor
//{
//	SerializedProperty sleepSprite;
//	EnemyPropertiesScript ePS;
//
//	void OnEnable()
//	{
//		ePS = target as EnemyPropertiesScript;
//		sleepSprite = serializedObject.FindProperty ("sleepSprite");
//	}
//
//	public override void OnInspectorGUI()
//	{
//		
//		serializedObject.Update();
//
//		ePS.radioactive = EditorGUILayout.Toggle ("Radioactive", ePS.radioactive);
//		ePS.soundproof = EditorGUILayout.Toggle ("Soundproof", ePS.soundproof);
//
//		if(ePS.soundproof)
//		{
//			EditorGUILayout.PropertyField (sleepSprite);// (serializedObject.FindProperty ("Transform"));
//		}
//		serializedObject.ApplyModifiedProperties();
//		//}
//	}
//}