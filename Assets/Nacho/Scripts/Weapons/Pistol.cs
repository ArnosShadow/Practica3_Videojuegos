using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private GameObject bulletPrefab; // Prefab de como sería la bala disparada
    [SerializeField] private Transform firePoint; // Referencia al objeto vacio que de donde será disparada la bala

    public override void Fire()
    {
        if (canFire)
        {
            
        }
    }
}
