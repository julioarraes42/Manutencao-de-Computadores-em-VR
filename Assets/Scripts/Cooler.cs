using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Cooler : MonoBehaviour, IInteractable
{
    public int chave = 1;
    public bool encaixado;
    public Transform localDoEncaixe;
    public Collider[] colisoesIgnoradas;
    public ParafusoCooler[] parafusosCooler;
    public int parafusosAtivos;
    public Transform posicaoEncaixado;
    public Transform posicaoFixo;
    public bool fixo;

    private void Start()
    {
        parafusosAtivos = 0;
        fixo = false;
    }

    public void Parafusar(bool tipoAcao)
    {
        
        if (tipoAcao)
        {
            parafusosAtivos += 1;
        }
        else 
        { 
            parafusosAtivos -= 1; 
        }

        if (parafusosAtivos == 4)
        {
            transform.position = posicaoFixo.position;
            fixo = true;
            GetComponent<XRGrabInteractable>().enabled = false;
        }
        else if (parafusosAtivos == 0)
        {
            transform.position = posicaoEncaixado.position;
            fixo = false;
            GetComponent<XRGrabInteractable>().enabled = true;
        }
        
    }


    public void OnTriggerStay(Collider other)
    {
        Slot slot = other.GetComponent<Slot>();
        if (slot != null)
        {
            if (slot.chave == this.chave && !slot.sombraAtiva && !encaixado)
            {
                slot.AtivarSombra();
                localDoEncaixe = slot.shadow.GetComponent<Transform>();
            }
            else if(encaixado && slot.sombraAtiva && fixo)
            {
                slot.DesativarSombra();
                slot.sombraAtiva = false;
                localDoEncaixe = null;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Slot slot = other.GetComponent<Slot>();
        if (slot != null)
        {
            if (slot.chave == this.chave && slot.sombraAtiva)
            {
                localDoEncaixe = null;
            }

            if(slot.chave == this.chave && slot.sombraAtiva && !encaixado)
            {
                slot.DesativarSombra();
            }
        }
    }


    public void Agarrar()
    {
        GetComponent<Collider>().isTrigger = true;
        transform.SetParent(null);

        encaixado = false;
    }

    public void Soltar()
    {
        GetComponent<Collider>().isTrigger = false;

        if (localDoEncaixe != null)
        {
            for (int i = 0; i < colisoesIgnoradas.Length; i++)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), colisoesIgnoradas[i], true);
            }

            GetComponent<Rigidbody>().isKinematic = true;

            transform.SetParent(localDoEncaixe.parent);
            transform.position = localDoEncaixe.position;
            transform.rotation = localDoEncaixe.rotation;
            encaixado = true;

            for (int i = 0; i< parafusosCooler.Length; i++)
            {
                parafusosCooler[i].interagivel = true;
                parafusosCooler[i].sombra.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < colisoesIgnoradas.Length; i++)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), colisoesIgnoradas[i], false);
            }

            for (int i = 0; i < parafusosCooler.Length; i++)
            {
                parafusosCooler[i].interagivel = false;
                parafusosCooler[i].sombra.SetActive(false);
            }

            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
