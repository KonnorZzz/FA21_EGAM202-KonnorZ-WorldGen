using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TerrainSquitch))]
[CanEditMultipleObjects]
public class TerrainSquitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TerrainSquitch squitch = (TerrainSquitch)target;
        if (GUILayout.Button("Pip"))
        {
            squitch.Pip();
        }
        if (GUILayout.Button("Set Elevation"))
        {
            squitch.SetElevation();
        }
        if (GUILayout.Button("Extrude Box"))
        {
            squitch.ExtrudeBox();
        }
        if (GUILayout.Button("Three Stairs"))
        {
            squitch.ThreeStairs();
        }
        if (GUILayout.Button("Extrude Cylinder"))
        {
            squitch.ExtrudeCylinder();
        }
        if (GUILayout.Button("Random Independent Profile"))
        {
            squitch.RandomIndependentProfile();
        }
        if (GUILayout.Button("Random Walk Profile"))
        {
            squitch.RandomWalkProfile();
        }
        if (GUILayout.Button("Single Step"))
        {
            squitch.SingleStep();
        }
        if (GUILayout.Button("Many Steps"))
        {
            squitch.ManySteps();
        }
    }
}
