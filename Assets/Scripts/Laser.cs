using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
//    [SerializeField] private Transform firePoint;
//    [SerializeField] private LineRenderer lineRenderer;
//    [SerializeField] private int damage;
//   
//    private void Start()
//    {
//        lineRenderer.enabled = false;
//    }
//    
//    private void Shoot()
//    {
//        lineRenderer.enabled = true;
//        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
//        if (hitInfo)
//        {
//            hitInfo.collider.gameObject.GetComponent<Projectile>().ReceiveDamage(damage);
//            lineRenderer.SetPosition(0, firePoint.position);
//            lineRenderer.SetPosition(1, hitInfo.point);
//        }
//        else
//        {
//            lineRenderer.SetPosition(0, firePoint.position);
//            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
//        }
//    }
//
//    
//    private void Update()
//    {
//        if (Input.GetKey(KeyCode.E)) Shoot();
//        else lineRenderer.enabled = false;
//
//    }

    
}
