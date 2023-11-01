using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogoMuebles : MonoBehaviour
{
    public Sprite[] Muebles;
    private int PaginaAct = 0;

    public Image muebleSlot1;
    public Image muebleSlot2;
    public Image muebleSlot3;


    private void ActualizarMuebles()
    {
        int index1 = Mathf.Clamp(PaginaAct, 0, Muebles.Length - 1);
        int index2 = Mathf.Clamp(PaginaAct + 1, 0, Muebles.Length - 1);
        int index3 = Mathf.Clamp(PaginaAct + 2, 0, Muebles.Length - 1);

        muebleSlot1.sprite = Muebles[index1];
        muebleSlot2.sprite = Muebles[index2];
        muebleSlot3.sprite = Muebles[index3];

    }

    public void SiguientePagina()
    {
        PaginaAct += 3;
        if (PaginaAct >= Muebles.Length)
        {
            PaginaAct = 0;
        }

        ActualizarMuebles();
    }

    public void PaginaAnterior()
    {
        PaginaAct -= 3;
        if (PaginaAct < 0)
        {
            PaginaAct = Muebles.Length - 3;
        }

        ActualizarMuebles();
    }


}
