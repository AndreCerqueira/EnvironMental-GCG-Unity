using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    private Canvas _canvas;
    private GameObject _inv;

    public static List<Slot> slot = new List<Slot>();

    public static bool inventarioCriado;

    // Start is called before the first frame update
    void Awake()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //_inv = _canvas.transform.Find("Inventory Window/Scroll View/Viewport/Content").gameObject;

        if(!inventarioCriado)
        {
            for (int i = 0; i < 8; i++)
            {
                Slot _slot = new Slot();
                
                _slot.espaco = _canvas.transform
                    .Find("Inventory Window/Scroll View/Viewport/Content/Slots/Slot (" + i + ")/espaco")
                    .GetComponent<Image>();   
                _slot.quantObj = _canvas.transform
                    .Find("Inventory Window/Scroll View/Viewport/Content/Slots/Slot (" + i + ")/quant")
                    .gameObject;
                _slot.quant = 0;
                _slot.full = false;
             
                slot.Add(_slot);
            }
            inventarioCriado = true;
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                slot[i].espaco = _canvas.transform
                    .Find("Inventory Window/Scroll View/Viewport/Content/Slots/Slot (" + i + ")/espaco")
                    .GetComponent<Image>();   
                slot[i].quantObj = _canvas.transform
                    .Find("Inventory Window/Scroll View/Viewport/Content/Slots/Slot (" + i + ")/quant")
                    .gameObject;
            }
        }
    }

    public void adicionarNoInventario(string nome, int quant)
    {
        bool repetido = false;
        int repetidoIndex = -1;

        for (int i = 0; i < slot.Count; i++)
        {
            if(slot[i].item == nome)
            {
                repetidoIndex = i;            
                repetido = true;
            }
        }

        for (int i = 0; i < slot.Count; i++)
        {
            if(repetido && repetidoIndex == i)
            {
                slot[i].quant += quant;
                Text texto = slot[i].quantObj.transform.Find("Text").GetComponent<Text>();
                texto.text = slot[i].quant.ToString();
                break;
            }
            else if(!slot[i].full && repetidoIndex < 0)
            {
                slot[i].item = nome;
                slot[i].espaco.enabled = true;
                slot[i].espaco.sprite = Resources.Load<Sprite>("Resources/" + nome);;
                slot[i].quant += quant;
                slot[i].quantObj.SetActive(true);
                Text texto = slot[i].quantObj.transform.Find("Text").GetComponent<Text>();
                texto.text = slot[i].quant.ToString();
                slot[i].full = true;
                break;
            }
        }
    }

    // sobrecarga de metodo para especificar a posicao do objeto no inventario (usado ao fazer load do mapa)
    public void adicionarNoInventario(string nome, int quant, int pos)
    {
        slot[pos].item = nome;
        slot[pos].espaco.enabled = true;
        slot[pos].espaco.sprite = Resources.Load<Sprite>("DropIcons/" + nome);;
        slot[pos].quant = quant;
        slot[pos].quantObj.SetActive(true);
        Text texto = slot[pos].quantObj.transform.Find("Text").GetComponent<Text>();
        texto.text = slot[pos].quant.ToString();
        slot[pos].full = true;
    }

    /*public void trocarDeSlot(Slot slot1, Slot slot2)
    {
        if(!slot2.espaco.enabled)
            slot2.espaco.enabled = true;

        if(!slot2.quantObj.activeSelf)
            slot2.quantObj.SetActive(true);

        Sprite _temp1 = slot1.espaco.sprite;
        slot1.espaco.sprite = slot2.espaco.sprite;
        slot2.espaco.sprite = _temp1;

        string _temp2 = slot1.item;
        slot1.item = slot2.item;
        slot2.item = _temp2;

        int _temp3 = slot1.quant;
        slot1.quant = slot2.quant;
        slot2.quant = _temp3;

        Text texto = slot1.quantObj.transform.Find("Text").GetComponent<Text>();
        texto.text = slot1.quant.ToString();
        texto = slot2.quantObj.transform.Find("Text").GetComponent<Text>();
        texto.text = slot2.quant.ToString();

        if(slot1.espaco.sprite == null)
        {
            slot1.espaco.enabled = !slot2.espaco.enabled;
            slot1.quantObj.SetActive(!slot2.quantObj.activeSelf);
            slot1.full = !slot1.full;
        }
        if(slot2.espaco.sprite == null)
        {
            slot2.espaco.enabled = !slot2.espaco.enabled;
            slot2.quantObj.SetActive(!slot2.quantObj.activeSelf);
            slot2.full = !slot2.full;
        }
    }*/

}
