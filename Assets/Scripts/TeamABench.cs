using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Essa classe, simbolicamente representa o banco de reserva dos jogadores, mas na pr�tica funciona
 como um bot�o que ap�s clicado permite que o usu�rio selecione uma tile em campo para posicionar
um jogador correspondente ao banco clicado*/
public class TeamABench : MonoBehaviour
{
    public bool isClicked = false;        // booleano de controle
    public GridManagerScript gridManager; // referencia ao gridmanager para utilizar m�todos dessa classe
    
    void Start()
    {
        // obtendo o componente script do gridmanager para utilizar alguns m�todos e var contidos nele
        gridManager = GameObject.FindGameObjectWithTag("gridmanager").GetComponent<GridManagerScript>();
    }
    
    private void OnMouseDown()
    {
        isClicked = !(isClicked);  // ativando/desativando o booleano de controle
        //Debug.Log(isClicked); // debug message
        if (isClicked)
        {
            // desativando a permiss�o para posicionar um jogador do time B
            gridManager.placeB = false;
            //ativando/desativando a permiss�o para posicionar um jogador do time A
            gridManager.placeA = !(gridManager.placeA); 
            
            // Debug.Log(gridManager.placeA); // debug message

            gridManager.placeBall = false;
        }
    }
}
