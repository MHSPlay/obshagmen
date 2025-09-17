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

    private bool isOpen = false;

    public UnityEvent onDoorOpened;
    public UnityEvent onDoorClosed;
    public UnityEvent onDoorUnlocked;
    public UnityEvent onAccessDenied;

    void Start()
    {
        if (reqKey != null && reqKey.itemType == Item.ItemType.Key)
            reqKeyID = reqKey.itemID;

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
                // locked door feedback ?
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
        if (isOpen)
            return;

        isOpen = true;

        //PlaySound();

        onDoorOpened?.Invoke();

        StartCoroutine(Animation(0, 90));
    }

    public void close()
    {
        if (!isOpen)
            return;

        isOpen = false;

        //PlaySound();

        onDoorClosed?.Invoke();

        StartCoroutine(Animation(90, 0));
    }

    IEnumerator Animation(float start, float end)
    {
        while (transform.localRotation.y <= end || transform.localRotation.y >= end)
        {
            Debug.Log("1");
            transform.localRotation = new Quaternion(0,Mathf.Lerp(start, end, Time.deltaTime),0, 0);
            yield return null;
        }
        yield return null;
    }

    public bool IsLocked() => isLocked;
    public bool IsOpen() => isOpen;

}
