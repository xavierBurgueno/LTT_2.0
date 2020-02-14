using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [Tooltip("turret base object")]
    [SerializeField] List<GameObject> turretBase;
    [Tooltip("Turret gun object")]
    [SerializeField] List<GameObject> turretGun;

    private GameObject player;

    private float smooth;

    void Awake()
    {
        player = GameObject.Find("PlayerShip");

        smooth = 2.0f;
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        TurretRotation();
    }

    void TurretRotation()
    {
        int i;

        for (i = 0; i < turretBase.Count; i++)
        {
            Vector3 playerPos = transform.InverseTransformPoint(player.transform.position);

            Vector3 basePos = transform.InverseTransformPoint(turretBase[i].transform.position);

            Quaternion baseRot = Quaternion.LookRotation(playerPos - basePos);
            baseRot.x = 0.0f;
            baseRot.z = 0.0f;

            Vector3 gunPos = turretGun[i].transform.InverseTransformPoint(turretBase[i].transform.position);

            Quaternion gunRot = Quaternion.LookRotation(playerPos - gunPos);
            gunRot.y = 0.0f;
            gunRot.z = 0.0f;

            turretBase[i].transform.localRotation = Quaternion.Lerp(turretBase[i].transform.localRotation, baseRot, smooth * Time.deltaTime);
            turretGun[i].transform.localRotation = Quaternion.Lerp(turretGun[i].transform.localRotation, gunRot, smooth * Time.deltaTime);

            //yield return new WaitForSeconds(.1f);
        }
    }
}
