using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] tetrominoes;

    void Start()
    {
        NewTetromino();
    }

    void Update()
    {

    }

    public void NewTetromino()
    {
        Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
