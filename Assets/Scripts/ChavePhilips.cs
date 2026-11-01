using UnityEngine;

public class ChavePhilips : MonoBehaviour
{
    public ParafusoCooler parafusoCooler;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ParafusoCooler>() != null)
        {
            parafusoCooler = other.GetComponent<ParafusoCooler>();
        }
    }

    public void Ativar()
    {
        if (parafusoCooler != null)
        {
            parafusoCooler.Ativar();
        }
    }
}
