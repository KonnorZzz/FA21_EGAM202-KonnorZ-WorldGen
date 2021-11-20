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
        if (GUILayout.Button("Hundred Stairs"))
        {
            squitch.HundredStairs();
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
        if (GUILayout.Button("Smooth Function"))
        {
            squitch.SmoothFunction();
        }
        if (GUILayout.Button("Install Water"))
        {
            squitch.InstallWater();
        }
        if (GUILayout.Button("Fill Niche"))
        {
            squitch.FillNiche();
        }
        if (GUILayout.Button("King Konnor Creates City"))
        {
            squitch.CityofKonnor();
        }
        if (GUILayout.Button("Hill Generator"))
        {
            squitch.GenerateHills();
        }
        if (GUILayout.Button("Triangular Column"))
        {
            squitch.TriangularColumn();
        }
        if (GUILayout.Button("Mountain"))
        {
            squitch.Mountain();
        }
        if (GUILayout.Button("Sheep"))
        {
            squitch.FillSheep();
        }
        if (GUILayout.Button("Tree"))
        {
            squitch.FillTree();
        }
        if (GUILayout.Button("Make it Fancy"))
        {
            squitch.MakeItFancy();
        }
        if (GUILayout.Button("DeleteWater"))
        {
            squitch.DeleteWater();
        }
        if (GUILayout.Button("DeleteNiche"))
        {
            squitch.DeleteNiche();
        }
        if (GUILayout.Button("DeleteSheep"))
        {
            squitch.DeleteSheep();
        }
        if (GUILayout.Button("DeleteTree"))
        {
            squitch.DeleteTree();
        }

    }
}
