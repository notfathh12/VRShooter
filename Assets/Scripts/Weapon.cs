using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] private float recoilForce;
    [SerializeField] private float damage;

    private Rigidbody rigidBody;
    private XRGrabInteractable interactableWeapon;

    protected virtual void Awake()
    {
        interactableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();
        SetupInteractableWeaponEvents();
    }

    private void SetupInteractableWeaponEvents()
    {
        interactableWeapon.selectEntered.AddListener(PickUpWeapon);
        interactableWeapon.selectExited.AddListener(DropWeapon);
        interactableWeapon.activated.AddListener(StartShooting);
        interactableWeapon.deactivated.AddListener(OnDeactivate);
    }

    private void PickUpWeapon(SelectEnterEventArgs args)
    {
        args.interactorObject.transform.GetComponent<MeshHidder>().Hide();
    }

    private void DropWeapon(SelectExitEventArgs args)
    {
        args.interactorObject.transform.GetComponent<MeshHidder>().Show();
    }

    protected virtual void StartShooting(ActivateEventArgs args)
    {
        // Implementation your method
    }

    protected virtual void OnDeactivate(DeactivateEventArgs args)
    {
        StopShooting(args.interactorObject);
    }

    protected virtual void StopShooting(IXRInteractor interactor)
    {
        // Implementation your method
    }

    protected virtual void Shoot()
    {
        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }
}
