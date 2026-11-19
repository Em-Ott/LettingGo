using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Vector3 _interactRange;
    [SerializeField]
    private DialogueView _view;
    [SerializeField]
    private PlayerMovement _move;
    private InputAction _interactAction;
    private bool _inDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // To do: Move into InputHandler
    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
        _inDialogue = false;
        _interactAction.performed += HandleInteract;
    }

    public void HandleKindleTie()
    {
        Collider2D[] objectsNearby = Physics2D.OverlapBoxAll(this.gameObject.transform.position, _interactRange, 0f);
        foreach (Collider2D coll in objectsNearby)
        {
            if (coll.tag == "Wizard")
            {
                // To do make Wizard Class
                Debug.Log("Wizard found");
                coll.GetComponent<WizardTieHolder>().FormTie();
            }
        }
    }

    void HandleInteract(InputAction.CallbackContext lookInformation)
    {
        if (_inDialogue) { return; }

        Collider2D[] objectsNearby = Physics2D.OverlapBoxAll(this.gameObject.transform.position, _interactRange, 0f);
        List<DialogueInstance> allDialogueObjects = new List<DialogueInstance>();
        foreach (Collider2D coll in objectsNearby)
        {
            DialogueInstance[] possibleDialogueOptions = coll.gameObject.GetComponentsInChildren<DialogueInstance>();
            allDialogueObjects.AddRange(possibleDialogueOptions);
        }

        DialogueInstance highestPriorityObject = null;
        int priority = 100;
        for (int i = 0; i < allDialogueObjects.Count; i++)
        {
            if (!allDialogueObjects[i].Read && allDialogueObjects[i].ReadOrder < priority)
            {
                highestPriorityObject = allDialogueObjects[i];
                priority = allDialogueObjects[i].ReadOrder;
            }
        }

        if (!highestPriorityObject)
        {
            return;
        }
        else
        {
            _ = HandleFlagChange(highestPriorityObject);
            highestPriorityObject.Read = true;
        }
    }

    async Task HandleFlagChange(DialogueInstance currentInstance)
    {
        _inDialogue = true;
        _move.AllowMove(false);
        await _view.OnStartDialogue(currentInstance.Dialogue);
        _move.AllowMove(true);
        _inDialogue = false;
    }
}
