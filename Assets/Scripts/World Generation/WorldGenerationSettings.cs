/********************************************************************************
file: WorldGenerationSettings.cs
author: Joshua C. Lo
class: CS4700 - Game Development

assignemnt: Program 3
date last modified: 11/26/2026

purpose: The purpose of this program is to hold the parameters of the world generation
without having to manually input the parameters.
********************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "WorldGenerationSettings", menuName = "Scriptable Objects/WorldGenerationSettings")]
public class WorldGenerationSettings : ScriptableObject
{
    //World size
    public float worldWidth = 100f;
    public float worldHeight = 100f;
    public float tileSize = 5f;
    //Seed generation
    public int seed = 1111111;
    public bool randomSeed = true;
    //Perlin noise scale
    public float noiseScale = 0.07f;
    //Terrain generation
    public float waterGeneration = 0.30f;
    public float roughGeneration = 0.58f;
    public float cliffGeneration = 0.78f;
    //Terrain smoothing
    public int smoothing = 2;
    //Terrain tile generation 
    public float minBuildablePercent = 0.30f;
    public float minWalkablePercent = 0.30f;
    public float maxImpassablePercent = 0.10f;
    //Map generation count to find a "good seed"
    public int maxGen = 10;
    //Resource hohoho
    public int resources = 10;
}
