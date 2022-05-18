using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Classe responsável por gerenciar e representar um jogador do time B*/
public class PlayerB : MonoBehaviour
{
    public GameManagerScript gameManager;  //Acesso ao gamemanager e grid manager para fazer uso de métodos
    public GridManagerScript gridManager;  //E variáveis dessas classes
    
    public bool withballB = false;
    private bool chute = false;
    
    [SerializeField]
    private GameObject myBall;

    void Start()
    {
        // Obtendo o script desses Game Objects
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManagerScript>();
        gridManager = GameObject.FindGameObjectWithTag("gridmanager").GetComponent<GridManagerScript>();
        myBall = GameObject.FindGameObjectWithTag("ball");
    }

    void Update()
    {
        //Como os jogadores são separados, essa checagem era feito em todas, e como só um possuía a bola
        //no else, acabava sempre SetParent(null) quando tinha mais de um jogadpr em cena
        if (withBallB) // Se estiver com a bola...
        {
            myBall.transform.SetParent(this.transform); // a bola se torna o filho do jogador
        }
        //Agora, antes de setar null, procura-se em toda a cena por um jogador com a bola
        //se nenhum for encontrado, a bola não está na posse de ninguém
        else if (!gameManager.FindBall()) // se a bola nao estiver com ninguem
        {
            myBall.transform.SetParent(null); //o pai vai comprar cigarro
        }
    }

    //Ao clicar sobre um jogador do time B
    private void OnMouseDown()
    {
        //Se estivermos na fase de posicionamento remover o player do campo
        if (!gameManager.matchon)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        this.withBallB = false; // ninguém está com a bola
        this.myBall.transform.SetParent(null); // ela vira órfã

        gameManager.teamB.Remove(this);
        Destroy(gameObject);
        gridManager.ToggleVacantTile(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            withBallB = true;

            // player A, como ataca pra direita, a bola vai estar mais a direita do player
            myBall.transform.position = new Vector3(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            withBallB = false;
        }
    }
}
