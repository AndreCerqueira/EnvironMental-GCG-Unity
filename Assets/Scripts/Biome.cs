using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Biome", menuName = "SustainableCities/Biome")]
public class Biome : ScriptableObject
{
    public string biomeName;
    public string biomeDetails;
    //public int tileTypeID;

    // durability
    // other things

    public Material[] materials;

}

[System.Serializable]
public class Material
{
    public string materialName;
}