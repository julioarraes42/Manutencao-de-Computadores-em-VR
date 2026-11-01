using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject shadow;
    public int chave;
    public bool sombraAtiva;


    public void AtivarSombra()
    {
        shadow.SetActive(true);
        sombraAtiva = true;
    }

    public void DesativarSombra()
    {
        shadow.SetActive(false);
        sombraAtiva = false;
    }
}
