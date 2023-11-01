using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBoton : MonoBehaviour
{
    public GameObject panel;
    private AnimacionPanel animacionPanel;
    public bool activo;

    private void Start()
    {
        //referencia scrpt
        animacionPanel = panel.GetComponent<AnimacionPanel>();
        panel.SetActive(panel.activeSelf);

    }

    public void MostrarOcultarPanel()
    {


        if (panel.activeSelf)
        {
            animacionPanel.OcultarPanel();
            StartCoroutine("Esperar");

        }
        else if (!panel.activeSelf)
        {
            panel.SetActive(true);
            Debug.Log("AAAAAAAAAAAAA");
            animacionPanel.MostrarPanel();
            //StartCoroutine("Esperar2");
        }

    }

    IEnumerator Esperar()
    {
        yield return new WaitForSecondsRealtime(1);
        panel.SetActive(!panel.activeSelf);
    }


}
