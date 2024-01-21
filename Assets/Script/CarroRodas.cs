using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroRodas : MonoBehaviour
{
    public WheelCollider[] wCollider;
    public Transform[] rodas;
    private Vector3 posicao;
    private Quaternion rotacao;

    public float torque, friccao, freio, ang;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < wCollider.Length; x++)
        {
            //Remover tremedeira do carro
            wCollider[x].ConfigureVehicleSubsteps (1, 12, 15);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rodas();
    }

    void FixedUpdate()
    {
        wCollider[0].steerAngle = ang * Input.GetAxis("Horizontal");
        wCollider[1].steerAngle = ang * Input.GetAxis("Horizontal");

        for(int x = 0; x < wCollider.Length; x++)
        {
             //Fazer o carro ir para frente e para tras 
            wCollider[x].motorTorque = Input.GetAxis("Vertical") * torque;
              //Freiar o carro e definir teclas W-S frente e tras
            wCollider[x].brakeTorque = (Input.GetKey(KeyCode.Space))
             ? freio : friccao - Mathf.Abs(Input.GetAxis("Vertical") * friccao);

        }
    }

    void Rodas()
    {
        for(int x = 0; x < wCollider.Length; x++)
        {
            //Junção entre as rodas e a gravidade WCollider - posiçaõ e rotação
            wCollider[x].GetWorldPose(out posicao, out rotacao);
            rodas[x].position = posicao;
            rodas[x].rotation = rotacao;

             if(x < 2)
             {
                //Virar o carro usando as teclas A-D 
                wCollider[x].steerAngle = Mathf.Lerp(wCollider[x].steerAngle, 
                ang * Input.GetAxis("Horizontal"), Time.deltaTime * 10);
             }
        
        }
    }
}
