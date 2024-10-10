using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.MLAgents;
using TMPro;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyScript : Agent
{

    public TMP_InputField matriz;
    bool statusmove=true, statusShoot=true;
    public List<ProjectileObject> projectilList;
    public int[] positionsPoint = new int[] { -210, -70, 70, 210 };
    public int indexPosition;
    public GameObject player;
    public GameObject shooter;
    public Player playerData;
    public bool[] vectorCuadrantes = new bool[24];
    //isntance singleton
    public static EnemyScript instance;
    private Coroutine moveCoroutine;
    public GameObject battlePanel;

    private void Awake() {
        playerData = new Player("ship1", 100, 100, 50, 80, 50);
    }

    void Start() {
        indexPosition = 1;
        int xPoint = positionsPoint[indexPosition];
        transform.localPosition = new Vector3(xPoint, transform.localPosition.y, transform.localPosition.z);
        //Debug.Log(projectilList.Count + " elementos de proyectiles");
    }

    void Update() {
        pruebaMvimientos();
    }

    public void moveShip(string orientation) {
        if(statusmove==true){
        switch (orientation) {
            case "right":
                indexPosition++;
                if (indexPosition >= positionsPoint.Length) {
                    indexPosition--;
                    Debug.Log("fuera de rangoD");
                } else {
                    Debug.Log($"nueva posicion {positionsPoint[indexPosition]}");
                    StartMovementCoroutine(positionsPoint[indexPosition], "right");
                }
                break;
            case "left":
                indexPosition--;
                if (indexPosition < 0) {
                    indexPosition++;
                    Debug.Log("fuera de rangoI");
                } else {
                    Debug.Log($"nueva posicion {positionsPoint[indexPosition]}");
                    StartMovementCoroutine(positionsPoint[indexPosition], "left");
                }
                break;
        }
        StartCoroutine("coldownMove");
        }else{  
            Debug.Log("movimiento cargando");
        }
    }

    private void StartMovementCoroutine(int targetPosition, string orientation) {
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveShip(targetPosition, transform.localPosition.x, orientation));
    }

    IEnumerator MoveShip(int targetPosition, float actualPosition, string orientation) {
        while ((orientation == "right" && actualPosition < targetPosition) ||
               (orientation == "left" && actualPosition > targetPosition)) {
            actualPosition = Mathf.MoveTowards(actualPosition, targetPosition, 1f);
            transform.localPosition = new Vector3(actualPosition, transform.localPosition.y, transform.localPosition.z);
            yield return new WaitForSeconds(0.0005f); // Disminuye el tiempo de espera para un movimiento más suave
        }
        transform.localPosition = new Vector3(targetPosition, transform.localPosition.y, transform.localPosition.z);
    }

    public void shootPjectile(string projectileNamePAram, string typeweapon) {
        Debug.Log("lanzado "+projectileNamePAram);
        if(statusShoot==true){
        ProjectileObject projectile;
        if (projectilList.Any(obj => obj.projectileName == projectileNamePAram)) {
            Debug.Log("si hay");
            projectile = projectilList.First(obj => obj.projectileName == projectileNamePAram);
            ProjectilScript contr = projectile.projectileObj.GetComponent<ProjectilScript>();
            if (typeweapon == "ArmaSecundaria") {
                contr.damage = playerData.secondaryDamage;
            } else {
                contr.damage = playerData.primaryDamage;
            }
                // Obtén la posición global del shooter
                Vector3 shooterPosition = shooter.transform.position;
                
                // Instancia el proyectil en la posición y orientación del shooter
                GameObject instantiatedProjectile = Instantiate(projectile.projectileObj, shooterPosition, shooter.transform.rotation);
                
                // Copia el tamaño del shooter al proyectil
                instantiatedProjectile.transform.localScale = shooter.transform.lossyScale;

                // Haz que el proyectil sea hijo del battlePanel
                instantiatedProjectile.transform.SetParent(battlePanel.transform, true);

                // Inicia la corrutina de movimiento del proyectil
                //StartCoroutine(instantiatedProjectile.GetComponent<ProjectilScript>().moveProjectil());
        } else {
            Debug.Log("no hay el proyectil");
        }
        StartCoroutine("coldownShooter");
    
        }else{
            Debug.Log("disparador cargando");
        }
    }

    private void doMov(int action) {
        
        if (action == 0 && statusmove) {
            moveShip("left");
            //StartCoroutine(ExecuteWhileTimePasses(1f,inputVector,action,reward));
        } else if (action == 1  && statusmove) {
            moveShip("right");
            //StartCoroutine(ExecuteWhileTimePasses(1f,inputVector,action,reward));
        } else if (action == 2  && statusShoot) {
            shootPjectile("torreta", "ArmaSecundaria");
            //StartCoroutine(ExecuteWhileTimePasses(4f,inputVector,action,reward));
        } else if (action == 3 && statusShoot) {
            shootPjectile("misil", "ArmaSecundaria");
            //StartCoroutine(ExecuteWhileTimePasses(4f,inputVector,action,reward));
        } else if(action == 4    && statusShoot){
            shootPjectile("plasma", "ArmaPrincipal");
            //StartCoroutine(ExecuteWhileTimePasses(4f,inputVector,action,reward));
        }
    }
    IEnumerator coldownMove(){
        statusmove=false;
        yield return new WaitForSeconds(0.5f);
        statusmove=true;

    }
    IEnumerator coldownShooter(){
        statusShoot=false;
        yield return new WaitForSeconds(2f);
        statusShoot=true;

    }
    public void printVector() {
        
    string msj = "";
    for (int i = 0; i < 24; i++) {
        msj += (vectorCuadrantes[i] ? "1" : "0") + " ";
        if ((i + 1) % 4 == 0) {
            Debug.Log(msj);
            msj += "\n";
        }
    }
    matriz.text=msj;
    
    }
    public override void OnEpisodeBegin()
    {
        
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if(BattleMng.instance.playStatus){
        foreach(var a in vectorCuadrantes){
            sensor.AddObservation(a);//cada estado de los cuadrantes se agregara como una observacion
        }
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = UnityEngine.Random.Range(0, 5);*/
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if(BattleMng.instance.playStatus){
        int action = actions.DiscreteActions[0];
        doMov(action);
        }
    
    }

    public void AddRewardMove(){
        AddReward(+3);
        EndEpisode();
    }
     private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("proyectil")) {
            //takeDamage(other.gameObject.GetComponent<ProjectilScript>().damage);
            Destroy(other.gameObject);
            Debug.Log("impacto reduciendo rward");
            AddReward(-3);
            EndEpisode();
        }
    }

    private void pruebaMvimientos() {
        if (Input.GetKeyDown(KeyCode.H)) {
            doMov(0);
        } else if (Input.GetKeyDown(KeyCode.K)) {
            doMov(1);
        } else if (Input.GetKeyDown(KeyCode.U)) {
            doMov(3);
        } else if (Input.GetKeyDown(KeyCode.I)) {
            doMov(4);
        }
    }
}