using UnityEngine;

public class ParkingControl : MonoBehaviour
{
    public Transform digerObject; // Di�er nesnenin transformu
    public float yakinlikMesafesi = 0.5f; // Kabul edilebilir yak�nl�k mesafesi
    public bool isGreen = false;
    public bool isParkOK = false;


    

    // Update is called once per frame
    public void FixedUpdate()
    {
        // Di�er nesnenin d�nya konumu
        Vector3 digerNesneKonumu = digerObject.position;

        // Bu nesnenin box collider'�
        BoxCollider buCollider = GetComponent<BoxCollider>();

        // Bu nesnenin box collider'�n�n boyutu
        Vector3 boyut = buCollider.size;

        // Bu nesnenin box collider'�n�n orta noktas�
        Vector3 buNesneOrtaNokta = transform.position + buCollider.center;

        // Di�er nesnenin box collider'�n�n boyutu
        BoxCollider digerCollider = digerObject.GetComponent<BoxCollider>();
        Vector3 digerBoyut = digerCollider.size;

        // Di�er nesnenin box collider'�n�n orta noktas�
        Vector3 digerOrtaNokta = digerObject.position + digerCollider.center;

        // �ki orta nokta aras�ndaki mesafeyi hesapla
        float mesafe = Vector3.Distance(buNesneOrtaNokta, digerOrtaNokta);

        // Mesafenin belirli bir mesafeden daha k�sa olup olmad���n� kontrol et
        if (mesafe < yakinlikMesafesi)
        {
            isGreen = true;
        }
        else
        {
            isGreen = false;
            
        }


        if (isGreen && Input.GetKey(KeyCode.P))
        {
            isParkOK = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
