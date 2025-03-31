using UnityEngine;

public abstract class Weapon : Pickup
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float reloadTime;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
    }

    protected override void Collect(GameObject player)
    {
        base.Collect(player);
        WeaponManager weaponManager = player.GetComponent<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.AddWeapon(this);
        }
    }

    public abstract void Fire();
    public abstract void Reload();

    public virtual bool CanFire()
    {
        return !isReloading && currentAmmo > 0 && Time.time >= nextTimeToFire;
    }
}
