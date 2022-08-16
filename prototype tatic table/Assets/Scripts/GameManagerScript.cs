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

    //NOVO
    private float mDistance;
    private float distance = 0f;
    private Transform mPosition;

    public List<PlayerA> teamA = new List<PlayerA>(); // Lista com os jogadores do time A
    string[] jogadores = {"Final", "Jogador", "Jogador1", "Jogador2", "Jogador3"};

    [SerializeField]
    public GameObject Gol;

    public List<GameObject> teamB = new List<GameObject>(); // Lista com os jogadores do time B

    public SpriteRenderer _nrenderer;           // componente reder que será usado adiante pra modificar
                                                // a cor das tiles
   

    // Update is called once per frame
    void Update()
    {
        // se o time A estiver todo em campo, e o espaço houver sido apertado
        if (Input.GetKeyDown(KeyCode.Space) && teamA.Count >= maxPlayers)
        {
            matchon = true;
            Match();
            // Debug.Log("Match started"); // debug message
        }
    }

    //func responsável pela partida, gestão de turnos e execução da IA 
    private void Match()
    {
        // vizinhos estao na lista, percorrendo toda vendo o numero dos jogadores

        // se for o turno 0 fazemos o grid ficar transparente
        if (turno == 0)
        {
            GameObject gol = Instantiate(Gol, new Vector3(15.97f, 3.94f, 0), Quaternion.identity);
            teamA.Add(gol.GetComponent<PlayerA>());
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
            if (teamA[i].withBall)
            {
                return true;
            }
        } 
        return false;
    }

    //NOVO
    public Transform MenorDistancia(Transform playerPosition)
    {
        mPosition = null;
        mDistance = 99999f;

        for (int i = 0; i < teamA.Count; i++)
        {
            if (!teamA[i].withBall)
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
