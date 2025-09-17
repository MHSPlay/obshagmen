using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Item reqKey;
    [SerializeField] private string reqKeyID;
    [SerializeField] private bool isLocked = true;
    private Animation anim;

    private bool isOpen = false;

    public UnityEvent onDoorOpened;
    public UnityEvent onDoorClosed;
    public UnityEvent onDoorUnlocked;
    public UnityEvent onAccessDenied;

    void Start()
    {
        if (reqKey != null && reqKey.itemType == Item.ItemType.Key)
            reqKeyID = reqKey.itemID;

        anim = GetComponent<Animation>();
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
        // @todo: fix text
        if (isLocked)
            return $"[E] ������� (���������: {(reqKey ? reqKey.itemName : "����")})";
        else
            return isOpen ? "[E] ������� �����" : "[E] ������� �����";

    }

    void unlock()
    {
        isLocked = false;

        //PlaySound();

        Inventory.Instance.remove(reqKey);
        onDoorUnlocked?.Invoke();
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
        if(anim.isPlaying)
            return;

        if (isOpen)
            return;

        isOpen = true;

        //PlaySound();

        onDoorOpened?.Invoke();

        anim.Play("DoorOpen");
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

        anim.Play("DoorClose");
    }

    public bool IsLocked() => isLocked;
    public bool IsOpen() => isOpen;

}
