using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using PMGF.PMGUnity.Managers;
using PMGF.PMGGenerator;
using PMGF.PMGGameInstance;

//this is the main program application, manages other managers


//-----------------------------------------------------------------------// unity singleton for gameManager
public class SingleApplicationManager : MonoBehaviour
{

    public  SingleApplicationManager() { }
    static SingleApplicationManager instance = null;
    public static SingleApplicationManager Instance
    {
        get
        {
            if (SingleApplicationManager.instance == null)
            {
                // dont destroy on load lets the game manager survive when switching scenes
                DontDestroyOnLoad(SingleApplicationManager.instance);
                SingleApplicationManager.instance = new SingleApplicationManager();
            }
            return SingleApplicationManager.instance;
        }

    }
    //-----------------------------------------------------------------------//
    public static SingleApplicationManager InstanceOBJ { get; private set; }

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
        SingleApplicationManager.instance = null;
    }
    //-----------------------------------------------------------------------//*/
    // runs once in the beginning(Awake() is before this however)
    public void Start()
    {
        

        SingleMenuManager.Instance.Setup(true);
        SingleInputManager.Instance.Setup(false);
        SingleMapManager.Instance.Setup();
        //decodes and makes parsed set
        SingleInstanceManager.Instance.setup();
    }
    //runs all the time
    public void Update()
    {
        SingleInputManager.Instance.MenuInputUpdate();

        //update actors when game is not paused
        if (!SingleInputManager.Instance.inMenu && SingleInstanceManager.Instance.ThisGameForNow.SpawnedActors.Count >0)
        {
            SingleInstanceManager.Instance.ThisGameForNow.UpdateActors();
        }

        //update unity actors
        SingleInstanceManager.Instance.UpdateUnityActors();

    }
    
    //-----------------------------------------------------------------------//
}
