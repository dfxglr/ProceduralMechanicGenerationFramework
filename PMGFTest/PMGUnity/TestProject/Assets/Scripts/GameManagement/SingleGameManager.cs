using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UN_SingleInputManager;
using UN_SingleMenuManager;

//-----------------------------------------------------------------------// unity singleton for gameManager
public class SingleGameManager : MonoBehaviour
{
    protected SingleGameManager() { }
    private static SingleGameManager instance = null;
    public static SingleGameManager Instance
    {
        get
        {
            if (SingleGameManager.instance == null)
            {
                // dont destroy on load lets the game manager survive when switching scenes
                DontDestroyOnLoad(SingleGameManager.instance);
                SingleGameManager.instance = new SingleGameManager();
            }
            return SingleGameManager.instance;
        }

    }
//-----------------------------------------------------------------------//
    public static SingleGameManager InstanceOBJ { get; private set; }

    private void Awake()
    {
        if (InstanceOBJ != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        InstanceOBJ = this;
        DontDestroyOnLoad(gameObject);
    }
    // might not be needed for now
    public void OnApplicationQuit()
    {
        SingleGameManager.instance = null;
    }
    //-----------------------------------------------------------------------//*/
    // runs once in the beginning(Awake() is before this however)
    public void Start()
    {
        SingleMenuManager.Instance.Setup(true);
        SingleInputManager.Instance.Setup(true);
    }
    //runs all the time
    public void Update()
    {
        SingleInputManager.Instance.InputUpdate();
    }
    //-----------------------------------------------------------------------//
}
