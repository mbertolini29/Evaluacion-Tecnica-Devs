using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public static PlayerNetwork instance;

    [Header("Movement Controller")]
    public Vector3 moveInput;
    public float moveSpeed = 10f;

    [Header("Camera Controller")]
    public Transform camTransform;
    public float mouseSensitivity = 2f;

    //angulo maximo para mover la camara hacia arriba
    public float maxViewAngle = 60f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //si no sos el propietario, regresa
        //para que no se ejecute en otros prefab
        if (!IsOwner) return;

        //para que el movimiento y la camara se mueva en base al jugador,
        Vector3 verMove = transform.up * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");
        MovePlayer(verMove, horiMove);
        transform.position += moveInput * moveSpeed * Time.deltaTime;
        //RotationPlayer();
    }

    void MovePlayer(Vector3 verMove, Vector3 horiMove)
    {
        moveInput = horiMove + verMove;
        moveInput.Normalize(); 
        //normaliza el movimiento,sino
        //cuando presionas hacia delante y al costado, va en diagonal mas rapido que yendo hacia un lado.
    }

    void RotationPlayer()
    {
        //control rotacion camara
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), 
                                         Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        //quaternion ayuda a que la rotacion sea suave..
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                              transform.rotation.eulerAngles.y + mouseInput.x,
                                              transform.rotation.eulerAngles.z);

        //camTransform.rotation = Quaternion.Euler(camTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        ////bloquear que el jugador mire cierta cantidad para arriba
        //if (camTransform.rotation.eulerAngles.x > maxViewAngle &&
        //   camTransform.rotation.eulerAngles.x < 180f)
        //{
        //    camTransform.rotation = Quaternion.Euler(maxViewAngle,
        //                                             camTransform.rotation.eulerAngles.y,
        //                                             camTransform.rotation.eulerAngles.z);
        //}
        //else if (camTransform.rotation.eulerAngles.x > 180f &&
        //        camTransform.rotation.eulerAngles.x < 360f - maxViewAngle)
        //{
        //    camTransform.rotation = Quaternion.Euler(-maxViewAngle,
        //                                              camTransform.rotation.eulerAngles.y,
        //                                              camTransform.rotation.eulerAngles.z);
        //}
    }
}
