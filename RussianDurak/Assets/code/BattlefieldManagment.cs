using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

class PlaceHolder
{
    public bool IsFree = true;
    public List<Card> content = new() { };
}
public class BattlefieldManagment : MonoBehaviour
{
    private List<PlaceHolder> BattlefieldMatrix = new() { 
        new PlaceHolder(), new PlaceHolder(), new PlaceHolder(), 
        new PlaceHolder(), new PlaceHolder(), new PlaceHolder()};

    [SerializeField] public float duration = 1f;
    public bool PutCard(GameObject cardObject, Card card)
    {
        bool areThereFreeSpace = false;
        int freeSpaceIndex = -1;
        for (int i = 0; i < BattlefieldMatrix.Count; i++)
        {
            PlaceHolder placeholder = BattlefieldMatrix[i];
            if (
                placeholder.IsFree 
                && 
                (
                    (
                        placeholder.content.Count == 1 
                        && 
                        card.owner.ID != placeholder.content[0].owner.ID
                        &&
                        card.Fight(placeholder.content[0])
                    )
                    ||
                    placeholder.content.Count == 0
                )
               )
            {
                Debug.Log(freeSpaceIndex);
                areThereFreeSpace = true;
                freeSpaceIndex = i;
                break;
            }
        }
        if (!areThereFreeSpace) return false;

        PlaceHolder slot = BattlefieldMatrix[freeSpaceIndex];
        slot.content.Add(card);
        if (slot.content.Count == 2) slot.IsFree = false;

        CardBehaivour cb = cardObject.GetComponent<CardBehaivour>();

        GameObject placeholderObject = transform.GetChild(freeSpaceIndex).gameObject;

        cb.StopAllCoroutines();

        cardObject.transform.SetParent(transform, true);

        cb.StartCoroutine(cb.MoveRoutine(
            cardObject.transform.position, 
            placeholderObject.transform.position + placeholderObject.transform.up * (card.owner.ID == 0  & !slot.IsFree ? 1: -1), 
            duration
        ));

        cb.StartCoroutine(cb.ResizeRoutine(
            cardObject.transform.lossyScale,
            placeholderObject.transform.localScale,
            duration
        ));
        return true;
    }
}
