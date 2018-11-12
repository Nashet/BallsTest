using BallsTest;
using System;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Checks if values are set correctly
/// </summary>
[CustomEditor(typeof(BallsGenerator))]
public class BallsGeneratorEditor : Editor
{
    private GameObject previousValue;
    private bool firstCheck = true;

    // don't use it for MonoBehaviour
    protected BallsGeneratorEditor()
    {
    }

    private void OnSceneGUI()
    {
        var generator = target as BallsGenerator;

        if (generator.ballPrefab != previousValue || firstCheck)
        {
            firstCheck = false;
            if (generator.ballPrefab == null)
                EditorUtility.DisplayDialog("", nameof(generator.ballPrefab) + " value shouldn't be null", "Ok");
            else
            {
                if (generator.ballPrefab.GetComponent<Ball>() == null)
                    EditorUtility.DisplayDialog("", nameof(generator.ballPrefab) + " object should have " + typeof(Ball).Name + " script", "Ok");
            }
        }

        previousValue = generator.ballPrefab;
    }
}