using Unity.VisualScripting;
using UnityEngine;

public class ObjecSnapping : MonoBehaviour
{
    public GameObject sombra;
    public int chave;
    public GrabbableObject objeto;
    public bool encaixado;
    public bool objetoDentro;
    public Collider[] collidersIgnorados;

    private void Start()
    {
        objetoDentro = false;
        encaixado = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<GrabbableObject>() != null)
        {
            if (other.GetComponent<GrabbableObject>().chave == chave && encaixado == false)
            {
                sombra.SetActive(true);
                objeto = other.GetComponent<GrabbableObject>();
                objetoDentro = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<GrabbableObject>() != null)
        {
            if (other.GetComponent<GrabbableObject>().chave == chave && encaixado == false)
            {
                sombra.SetActive(false);
                objeto = null;
                objetoDentro = false;
            }
        }
    }

    public void Encaixar()
    {

        if (encaixado == false && objeto != null && objetoDentro == true)
        {
            Debug.Log("Passando aqui");

            encaixado = true;
            objetoDentro = false;

            Collider collider = objeto.GetComponent<Collider>();

            for (int i = 0; i < collidersIgnorados.Length; i++)
            {
                Physics.IgnoreCollision(collider, collidersIgnorados[i], true);
            }

            objeto.transform.SetParent(transform);
            objeto.GetComponent<Rigidbody>().isKinematic = true;
            objeto.transform.position = sombra.transform.position;
            objeto.transform.rotation = sombra.transform.rotation;

            sombra.SetActive(false);
        }
        else if(encaixado == true)
        {
            objeto.transform.SetParent(null);
            objeto.GetComponent<Rigidbody>().isKinematic = false;
            Collider collider = objeto.GetComponent<Collider>();

            for (int i = 0; i < collidersIgnorados.Length; i++)
            {
                Physics.IgnoreCollision(collider, collidersIgnorados[i], false);
            }
            encaixado = false;
            objeto = null;
        }
    }

    public void Desencaixar()
    {
        
    }

    }
