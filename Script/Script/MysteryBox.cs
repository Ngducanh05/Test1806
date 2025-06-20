

using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject coinPrefab; 
    public Transform spawnPoint;  
    private bool isUsed = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isUsed && collision.gameObject.CompareTag("PlayerAnh"))
        {
            isUsed = true;
            Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
           
        }
    }
}
