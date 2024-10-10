using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{
    public EnemyScript enemy;
    public List<ProjectileObject> projectilList;
    public  int[] positionsPoint= new int[]{-210,-70,70,210};
    public int indexPosition;
    public GameObject player;
    public GameObject shooter;
    public Player playerData; 
    public GameObject battlePanel;
    // Start is called before the first frame update

    private void Awake() {
        playerData= new Player(PreparationMng.Instance.player.shipName,PreparationMng.Instance.player.health,PreparationMng.Instance.player.shield,
        PreparationMng.Instance.player.secondaryDamage,PreparationMng.Instance.player.primaryDamage,PreparationMng.Instance.player.inmunity);
    }
    void Start()
    {
        indexPosition=1;
        int xPoint = positionsPoint[indexPosition];
        transform.localPosition=new Vector3(xPoint,transform.localPosition.y,transform.localPosition.z);
        Debug.Log(projectilList.Count +"elementos de proyectiles");
        //shootPjectile("misil","ArmaSecundaria");
        //moveShip("right");   
    }
    // Update is called once per frame
    void Update()
    {
        if(playerData.health>100){
            playerData.health=0;

        }else if(playerData.health<0){
            playerData.health=0;
        }
        if(playerData.shield>100){
            playerData.shield=100;

        }else if(playerData.shield<0){
            playerData.shield=0;
        }
        /*if(playerData.health==0){
            Destroy(this.gameObject);

        }*/
    }
    public void moveShip(string orientation){
        switch (orientation){
            case "right":
            indexPosition++;
            if(indexPosition>=positionsPoint.Length||indexPosition<0){
                indexPosition--;
                Debug.Log("fuera de rango");

            }else{
                Debug.Log($"nueva posicion {positionsPoint[indexPosition]}");
                /*Vector3 newPos= new Vector3 (positions[indexPosition],transform.localPosition.y,transform.localPosition.z);
                transform.localPosition=newPos;*/
                StartCoroutine(MoveShip(positionsPoint[indexPosition],transform.localPosition.x,"right"));

                Debug.Log("termino la funcion");
            }
            break;
            case "left":
            indexPosition--;
             if(indexPosition>=positionsPoint.Length||indexPosition<0){
                indexPosition++;
                Debug.Log("fuera de rango");

            }else{
                Debug.Log($"nueva posicion {positionsPoint[indexPosition]}");
                /*Vector3 newPos= new Vector3 (positions[indexPosition],transform.localPosition.y,transform.localPosition.z);
                transform.localPosition=newPos;*/
                StartCoroutine(MoveShip(positionsPoint[indexPosition],transform.localPosition.x,"left"));
            }
            break;
        }   
    }
    private void takeDamage (int damage){
        int shieldDamage = (int)(damage*0.8);
        if(shieldDamage<playerData.shield){
            playerData.shield=playerData.shield-shieldDamage;
            playerData.health=playerData.health-(damage-shieldDamage);
        }else {
            playerData.health=playerData.health+(playerData.shield-damage);
        }
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("proyectil")){
            takeDamage(other.gameObject.GetComponent<ProjectilScript>().damage);
            Destroy(other.gameObject);
            Debug.Log("chocoNave");
            enemy.AddRewardMove();
        }
    }
    public void shootPjectile(string projectileNamePAram, string typeweapon){
        ProjectileObject projectile;

        if(projectilList.Any(obj=>obj.projectileName==projectileNamePAram)){
            Debug.Log("si hay");
            projectile = (from obj in projectilList where obj.projectileName==projectileNamePAram select obj).First();
            ProjectilScript contr = projectile.projectileObj.GetComponent<ProjectilScript>();
            if(typeweapon=="ArmaSecundaria"){
                contr.damage=playerData.secondaryDamage;
            }else{
                contr.damage=playerData.primaryDamage;
            }
            Vector3 shooterPosition = shooter.transform.position;
                
                // Instancia el proyectil en la posición y orientación del shooter
                GameObject instantiatedProjectile = Instantiate(projectile.projectileObj, shooterPosition, shooter.transform.rotation);
                
                // Copia el tamaño del shooter al proyectil
                instantiatedProjectile.transform.localScale = shooter.transform.lossyScale;

                // Haz que el proyectil sea hijo del battlePanel
                instantiatedProjectile.transform.SetParent(battlePanel.transform, true);

                // Inicia la corrutina de movimiento del proyectil
                StartCoroutine(instantiatedProjectile.GetComponent<ProjectilScript>().moveProjectil());
        }else{

            Debug.Log("no hay el prouecvtifasdf");
        }
        /*ProjectilScript contr = projectile.projectileObj.GetComponent<ProjectilScript>();
        if(typeweapon=="ArmaSecundaria"){
            contr.damage=playerData.secondaryDamage;

        }else{
            contr.damage=playerData.primaryDamage;
        }
        Instantiate(projectile.projectileObj,shooter.transform);*/
         

    }
    public void repair(string partId){
        if(partId=="Escudo"){
            playerData.shield+=20;

        }else if(partId=="Nave"){
            playerData.health+=20;
        }
    }
}
