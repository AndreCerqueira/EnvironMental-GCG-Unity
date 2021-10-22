using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome
{
    // Basic Variables
    public string name;
    public string description;

    //public Material[] materials;

    public Biome(string _name, string _description) 
    {
        name = _name;
        description = _description;
    }

}

//[System.Serializable]
/*
public class Material
{
    public string name;
}*/