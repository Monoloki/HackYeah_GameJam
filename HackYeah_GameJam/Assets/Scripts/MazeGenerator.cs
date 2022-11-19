using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    [SerializeField] private GameObject slabPrefab;
    [Header("Maze stats")]
    [SerializeField] private int mazeWidth = 5;
    [SerializeField] private int mazeHeight = 10;
    [SerializeField] private float slabSize = 1;
    [SerializeField] private float blockY = -0.5f;
    [SerializeField] private List<GameObject> steps;

    private void Awake() {
        //Debug.Log($"front:{new Vector2(0, 1) - new Vector2(0, 0)} prawy:{new Vector2(1, 0) - new Vector2(0, 0)} lewy:{new Vector2(-1, 0) - new Vector2(0, 0)}");
        //GenerateMaze();
    }

    private void CleanMaze() {
        foreach (var slab in steps) {
            Destroy(slab);     
        }
        steps.Clear();
    }

    public void GenerateMaze() {
        CleanMaze();

        Vector2 current;
        Vector2 previous;
        Vector3 spawned = Vector3.zero;
        previous = new Vector2(spawned.x, spawned.z);
        current = new Vector2(1, 0);      

        while (current.y != mazeHeight) {
            Vector2 step = current - previous;
            int nextstep = 0;
            if (current.x == mazeWidth || current.x == -mazeWidth) {
                nextstep = GetNewPositionBorder(step,current);                       
            }
            else {
                nextstep = GetNewPosition(step);
            }
            previous = current;
            switch (nextstep) {
                case 0:
                    current += new Vector2(-1, 0);
                    break;
                case 1:
                    current += new Vector2(0, 1);
                    break;
                case 2:
                    current += new Vector2(1, 0);
                    break;
                default:
                    break;
            }
            steps.Add(Instantiate(slabPrefab, new Vector3(current.x, blockY, current.y), Quaternion.identity,transform));
        }

    }


    private int GetNewPosition(Vector2 step) {
        int random = 0;
        switch (step) {
            case Vector2 v when v.Equals(Vector2.up)://mial z tylu 0,1
                random = Random.Range(0, 3);
                break;
            case Vector2 v when v.Equals(Vector2.left)://mial po prawej -1,0
                random = Random.Range(0, 2);
                break;
            case Vector2 v when v.Equals(Vector2.right)://mial po lewej 1,0
                random = Random.Range(1, 3);
                break;
            default:
                break;
        }

        return random;
    }

    private int GetNewPositionBorder(Vector2 step, Vector2 currentVector) {
        int random = 0;
        switch (step) {
            case Vector2 v when v.Equals(Vector2.up)://mial z tylu 0,1
                if (currentVector.x == mazeWidth) {
                    random = Random.Range(0, 2);
                }
                else if (currentVector.x == -mazeWidth) {
                    random = Random.Range(1, 3);
                }
                break;
            case Vector2 v when v.Equals(Vector2.left)://mial po prawej -1,0
                random = 1;
                break;
            case Vector2 v when v.Equals(Vector2.right)://mial po lewej 1,0
                random = 1;
                break;
            default:
                break;
        }

        return random;
    }
}
