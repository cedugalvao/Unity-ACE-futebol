using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool chute = false;
    public bool gol = false;

    private float forceKick = 6.0f;

    public GameManagerScript gameManager;
    private Transform playerClosest;

    private GameObject jogadorComABola;
    private char time = 'N';

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !chute && gameManager.matchon && !gol) // ...e apertar j 
        {
            chute = true;
            if (time == 'A')
            {
                playerClosest = gameManager.MenorDistancia(jogadorComABola.transform, false, A);
            }
            else if (time == 'B')
            {
                playerClosest = gameManager.MenorDistancia(jogadorComABola.transform, false, B);
            }
        }

        if (playerClosest != null) // existe o player
        {
            // 1 que e uma margem para pegar o player do lado
            if (chute)
            {
                float distanciaProMaisProximo = Vector3.Distance(jogadorComABola.transform.position, playerClosest.position);

                if (distanciaProMaisProximo <= forceKick)
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, playerClosest.position, 10 * Time.deltaTime);
                }
            }
        }
        else // nao tem "opcoes"
        {
            if (chute)
            {
                if (time == 'A')
                {
                    jogadorComABola.GetComponent<PlayerA>();
                }
                else if (time == 'B')
                {
                    jogadorComABola.GetComponent<PlayerB>();
                }
                // tocar pro gol
                if (jogadorComABola != null)
                {
                    if (time == 'A')
                    {
                        float distance = Vector3.Distance(this.transform.position, gameManager.GolB.position);
                    }
                    else if (time == 'B')
                    {
                        float distance = Vector3.Distance(this.transform.position, gameManager.GolA.position);
                    }
                    
                    if (distance > forceKick)
                    {

                        if (time == 'A')
                        {
                            playerClosest = gameManager.MenorDistancia(jogadorComABola.transform, true, A);
                        }
                        else if (time == 'B')
                        {
                            playerClosest = gameManager.MenorDistancia(jogadorComABola.transform, true, B);
                        }

                        float distanciaProMaisProximo = Vector3.Distance(jogadorComABola.transform.position, playerClosest.position);

                        if (distanciaProMaisProximo <= forceKick)
                        {
                            // ROTACAO
                            // pode fazer uma animacao rotacionando um pouco na update
                            jogadorComABola.transform.Rotate(new Vector3(0, 0, 180));
                            this.transform.position = Vector2.MoveTowards(this.transform.position, playerClosest.position, 10 * Time.deltaTime);
                        }
                        else
                        {
                            // NAO CHUTA
                        }
                    }
                    else
                    {
                        if (time == 'A')
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, gameManager.GolB.position, 10 * Time.deltaTime);
                        }
                        else if (time == 'B')
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, gameManager.GolA.position, 10 * Time.deltaTime);
                        }
                    }

                }
                /*else // TeamB
                {
                    this.transform.position = Vector2.MoveTowards(this.transform.position, gameManager.GolA.position, 10 * Time.deltaTime);
                }*/
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // mudar para adicionar para colidir com player (seria pegar o prefab do player e adicionar tag nele)
        if (other.collider.CompareTag("PlayerA"))
        {
            jogadorComABola = other.gameObject;
            time = 'A';
            chute = false;
        }
        else if (other.collider.CompareTag("PlayerB"))
        {
           jogadorComABola = other.gameObject;
            time = 'B';
            chute = false; 
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("PlayerA") && jogadorComABola.transform.rotation.z != 0) // se era o player e o player tinha rotacionado
        {
            // voltar a rotacao
            StartCoroutine(EsperarSairDaCaixaColisao(other.transform));
        }
        else if (other.collider.CompareTag("PlayerB") && jogadorComABola.transform.rotation.z != 0)
        {
            StartCoroutine(EsperarSairDaCaixaColisao(other.transform));  
        }
    }

    private IEnumerator EsperarSairDaCaixaColisao(Transform colidiu)
    {
        yield return new WaitForSeconds(0.2f);
        colidiu.Rotate(new Vector3(0, 0, -180));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("golA") || other.CompareTag("golB"))
        {
            chute = false;
            gol = true;
        }
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
