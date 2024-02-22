using UnityEngine;

public class ParkingControl : MonoBehaviour
{
    public Transform digerObject; // Diðer nesnenin transformu
    public float yakinlikMesafesi = 0.5f; // Kabul edilebilir yakýnlýk mesafesi
    public bool isGreen = false;
    public bool isParkOK = false;


    

    // Update is called once per frame
    public void FixedUpdate()
    {
        // Diðer nesnenin dünya konumu
        Vector3 digerNesneKonumu = digerObject.position;

        // Bu nesnenin box collider'ý
        BoxCollider buCollider = GetComponent<BoxCollider>();

        // Bu nesnenin box collider'ýnýn boyutu
        Vector3 boyut = buCollider.size;

        // Bu nesnenin box collider'ýnýn orta noktasý
        Vector3 buNesneOrtaNokta = transform.position + buCollider.center;

        // Diðer nesnenin box collider'ýnýn boyutu
        BoxCollider digerCollider = digerObject.GetComponent<BoxCollider>();
        Vector3 digerBoyut = digerCollider.size;

        // Diðer nesnenin box collider'ýnýn orta noktasý
        Vector3 digerOrtaNokta = digerObject.position + digerCollider.center;

        // Ýki orta nokta arasýndaki mesafeyi hesapla
        float mesafe = Vector3.Distance(buNesneOrtaNokta, digerOrtaNokta);

        // Mesafenin belirli bir mesafeden daha kýsa olup olmadýðýný kontrol et
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
