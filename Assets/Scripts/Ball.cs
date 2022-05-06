using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D fisica;
    public bool chute = false;
    private Vector2 vetorMovimento;

    public GameManagerScript gameManager;
    private Transform playerClosest;

    private GameObject jogadorComABola;

    // Start is called before the first frame update
    void Start()
    {
        this.fisica = this.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !chute && gameManager.matchon) // ...e apertar j 
        {
            chute = true;
            playerClosest = gameManager.MenorDistancia(jogadorComABola.transform);
        }

        if (playerClosest != null) // existe o player
        {
            // 1 que e uma margem para pegar o player do lado
            if (chute)
            {
                //myBall.transform.SetParent(null);
                this.transform.position = Vector2.MoveTowards(this.transform.position, playerClosest.position, 10 * Time.deltaTime);
                // chuta para frente (direita)
                //ballPhysic.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
        }

        if (this.transform.position.x >= 15.20f) // bateu na borda direita
        {
            this.fisica.velocity = new Vector2(0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // mudar para adicionar para colidir com player (seria pegar o prefab do player e adicionar tag nele)
        jogadorComABola = other.gameObject;
        chute = false;
    }

    /*private void FixedUpdate()
    {
        // ir diminuindo quando bater em parede?
        //this.fisica.velocity = Vector2.zero;
        if (this.chute)
        {
            //this.fisica.velocity = this.vetorMovimento * this.forcaChute;
            this.fisica.AddForce(this.vetorMovimento * this.forcaChute, ForceMode2D.Force);
            this.chute = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // verificar se foi o jogador (pra nao colidir com os tiles)
        Vector2 diferencaX, diferencaY;
        diferencaX = new Vector2(this.transform.position.x - colisao.transform.position.x, 0);
        diferencaY = new Vector2(0, this.transform.position.y - colisao.transform.position.y);
        Vector2 vetorMovimentar = diferencaX + diferencaY;

        this.vetorMovimento = vetorMovimentar.normalized;
        this.chute = true;
    }*/
}
