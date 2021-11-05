using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot
{
    // espaco para por o item no slot
    public Image espaco { get; set; }

    // nome do item
    public string item { get; set; }

    // objeto de texto da quantidade de objetos
    public GameObject quantObj { get; set; }

    // quantidade de objetos
    public int quant { get; set; }

    // se o slot esta vazio ou nao
    public bool full { get; set; }

}
