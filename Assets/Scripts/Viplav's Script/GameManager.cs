using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // after making instance you can access it from any script
    // instance is like a prefab for scripts its like a reference to a script available to use in different scripts
    public static GameManager instance;
    // creates a list for waypoints 
    public List<Transform> wayPoints = new List<Transform>();
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

