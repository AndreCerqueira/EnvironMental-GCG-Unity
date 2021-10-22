using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome
{
    // Basic Variables
    public string name;
    public string details;

    // Workers

    //public Material[] materials;

    public Biome(string _name, string _details) 
    {
        name = _name;
        details = _details;
    }
}

//[System.Serializable]
/*
public class Material
{
    public string name;
}*/