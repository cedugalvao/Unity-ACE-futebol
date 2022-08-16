using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Essa classe é responsável por fazer a transição posicionamento-partida, bem como gerir a partida
 será nessa classe que chamaremos a função da IA*/
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] public int maxPlayers = 4; // número max de jogadores em cada time
    [SerializeField] public Color _matchColor;  // cor transparente das tiles no decorrer da partida
    
    public bool matchon;                        // booleano que controla se a partida começou ou não
    private int turno = 0;                      // turno da partida
    
    private float mDistance;
    private float distance = 0f;
    private Transform mPosition;

    public List<PlayerA> teamA = new List<PlayerA>(); // Lista com os jogadores do time A
    public List<PlayerB> teamB = new List<PlayerB>(); // Lista com os jogadores do time B

    [SerializeField]
    public Transform GolA;
    [SerializeField]
    public Transform GolB;

    private Ball ball;

    private bool chute = false;

    public SpriteRenderer _nrenderer;           // componente reder que será usado adiante pra modificar
                                                // a cor das tiles

    private void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        // se o time A estiver todo em campo, e o espaço houver sido apertado
        if (Input.GetKeyDown(KeyCode.Space) && teamA.Count >= maxPlayers && teamB.Count >= maxPlayers)
        {
            matchon = true;
            Match();
            // Debug.Log("Match started"); // debug message
        }

        /*
        if (Input.GetKeyDown(KeyCode.J) && !chute && matchon && !ball.gol) // ...e apertar j 
        {
            playerClosest = gameManager.MenorDistancia(jogadorComABola.transform, false, time);
            chute = true;
        }

        if (chute)
        {
            this.Chutar();
        }*/
    }

    //func responsável pela partida, gestão de turnos e execução da IA 
    private void Match()
    {
        // se for o turno 0 fazemos o grid ficar transparente
        if (turno == 0)
        {
            InvisibleGrid();
            for (int i = 0; i < teamA.Count; i++)
            {
                //Debug.Log(teamA[i].transform.position);

                // debug, checando se tinha mais de 1 filho
                // pois todos já tem 1, o trinagulo na frente
                /*if (teamA[i].transform.childCount > quantFilhosJogador)
                {
                    Debug.Log("com a bola");
                }*/
            }
            turno += 1;
        }

        // a partir do primeiro turno executamos a IA para fazermos a partida
        else
        {
            //fazer o algoritmo da IA rodar aqui
            turno += 1;
        }        

    }

    public bool FindBall()
    {
        //procurando se alguém tem a bola
        for (int i = 0; i < teamA.Count; i++)
        {
            if (teamA[i].withBallA)
            {
                return true;
            }
            
        }

        for (int i = 0; i < teamB.Count; i++)
        {
            if (teamB[i].withBallB)
            {
                return true;
            }
        }

        return false;
    }

    //NOVO
    public Transform MenorDistancia(Transform playerPosition, bool tocarPraTras, char time)
    {
        mPosition = null;
        mDistance = 99999f;

        for (int i = 0; i < maxPlayers; i++) // Ainda não tem expulsão no jogo, se tiver, mudar para team.Count
        {
            if (time == 'A')
            {
                if (!teamA[i].withBallA)
                {
                    if (!tocarPraTras)
                    {
                        if (teamA[i].transform.position.x >= playerPosition.position.x) // so pode passar para frente
                        {
                            distance = Vector3.Distance(playerPosition.position, teamA[i].transform.position);

                            if (distance < mDistance)
                            {
                                //Debug.Log(teamA[i].transform.position);
                                mDistance = distance;
                                mPosition = teamA[i].transform;
                            }
                        }
                    }
                    else
                    {
                        distance = Vector3.Distance(playerPosition.position, teamA[i].transform.position);

                        if (distance < mDistance)
                        {
                            //Debug.Log(teamA[i].transform.position);
                            mDistance = distance;
                            mPosition = teamA[i].transform;
                        }
                    }
                }
            }
            else if (time == 'B')
            {
                if (!teamB[i].withBallB)
                {
                    if (!tocarPraTras)
                    {
                        if (teamB[i].transform.position.x <= playerPosition.position.x) // so pode passar para frente
                        {
                            distance = Vector3.Distance(playerPosition.position, teamB[i].transform.position);

                            if (distance < mDistance)
                            {
                                //Debug.Log(teamB[i].transform.position);
                                mDistance = distance;
                                mPosition = teamB[i].transform;
                            }
                        }
                    }
                    else
                    {
                        distance = Vector3.Distance(playerPosition.position, teamB[i].transform.position);

                        if (distance < mDistance)
                        {
                            //Debug.Log(teamB[i].transform.position);
                            mDistance = distance;
                            mPosition = teamB[i].transform;
                        }
                    }
                }
            }
        }

        return mPosition;
    }

    //essa func deixa nossas tiles do grid transparentes pra o campo ser exibido em foco principal
    //porém ainda mantendo a estrutura tabuleiro 
    private void InvisibleGrid() 
    {
        // obtendo um array com todas as tiles que compoem nosso grid
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
        // Debug.Log("Match started " + tiles.Length); //debug message

        // loop que percorre o array e modifica a cor de cada tile
        for (int i = 0; i < tiles.Length; i++)
        {
            _nrenderer = tiles[i].GetComponent<SpriteRenderer>();
            _nrenderer.color = _matchColor;
            // Debug.Log(tiles[i].transform.position); //debug message 
        }
    } 
}
