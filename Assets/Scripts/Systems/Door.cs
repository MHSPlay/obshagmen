using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Item reqKey;
    [SerializeField] private string reqKeyID;
    [SerializeField] private bool isLocked = true;

    [SerializeField] private Sprite doorOpenIcon;
    [SerializeField] private Sprite doorClosedIcon;
    [SerializeField] private Sprite doorLockedIcon;

    [SerializeField] private bool isSwapSceneDoor = false;
    [SerializeField] private string targetSceneName;

    private Animation anim;
    private bool isOpen = false;
    private SceneManagers sceneManager;

    public UnityEvent onDoorOpened;
    public UnityEvent onDoorClosed;
    public UnityEvent onDoorUnlocked;
    public UnityEvent onAccessDenied;

    public UnityEvent onDoorStateChanged;

    void Start()
    {
        if (reqKey != null && reqKey.itemType == Item.ItemType.Key)
            reqKeyID = reqKey.itemID;

        anim = GetComponent<Animation>();

        sceneManager = GetComponent<SceneManagers>();
        if (isSwapSceneDoor && sceneManager == null)
            sceneManager = gameObject.AddComponent<SceneManagers>();
        
    }

    public void interact()
    {
        if (isLocked)
        {
            if (Inventory.Instance.has(reqKey.itemID))
            {
                unlock();
                toggle();
            }
            else
            {
                onAccessDenied?.Invoke();
            }
        }
        else
        {
            toggle();
        }
    }

    public string get_text()
    {
        if (isLocked)
            return $"[E] Дверь закрыта";
        else
            return isOpen ? "[E] Закрыть дверь" : "[E] Открыть дверь";

    }

    public Sprite get_icon()
    {
        if ( isLocked )
            return doorLockedIcon;
        else
            return isOpen ? doorOpenIcon : doorClosedIcon;
    }

    void unlock()
    {
        isLocked = false;

        //PlaySound();

        Inventory.Instance.remove(reqKey);
        onDoorUnlocked?.Invoke();
        onDoorStateChanged?.Invoke();
    }

    void toggle()
    {
        if (isOpen)
            close();
        else
            open();
    }

    public void open()
    {
        if(anim.isPlaying || isOpen)
            return;

        isOpen = true;

        //PlaySound();

        onDoorOpened?.Invoke();
        onDoorStateChanged?.Invoke();

        anim.Play("DoorOpen");

        if (isSwapSceneDoor && sceneManager != null && !string.IsNullOrEmpty(targetSceneName))
        {
            StartCoroutine(DelayedSceneTransition());
        }

    }

    public void close()
    {
        if (anim.isPlaying)
            return;

        if (!isOpen)
            return;

        isOpen = false;

        //PlaySound();

        onDoorClosed?.Invoke();
        onDoorStateChanged?.Invoke();

        anim.Play("DoorClose");
    }

    private IEnumerator DelayedSceneTransition()
    {
        yield return new WaitForSeconds( 0.0007f );
        sceneManager.load(targetSceneName);
    }

    public bool IsLocked() => isLocked;
    public bool IsOpen() => isOpen;

}
