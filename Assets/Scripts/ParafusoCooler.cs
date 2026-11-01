using Unity.VisualScripting;
using UnityEngine;

public class ParafusoCooler : Parafuso
{
    public bool interagivel;
    public Transform posicaoAtivo;
    public Transform posicaoDesativo;
    public Cooler cooler;

    private void Start()
    {
        ativo = false;
        interagivel = false;
    }
    public void Ativar()
    {
        if (interagivel)
        {
            if(ativo == false)
            {
                transform.position = posicaoAtivo.position;
                sombra.SetActive(false);
                cooler.Parafusar(true);
                ativo = true;
            }
            else
            {
                transform.position = posicaoDesativo.position;
                sombra.SetActive(true);
                cooler.Parafusar(false);
                ativo = false;
            }
        }
    }
}
