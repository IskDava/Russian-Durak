using UnityEngine;

public class UnderBank : MonoBehaviour
{
    private void Start()
    {
        Sprite underbank_sprite = Resources.Load<Sprite>("cards/" + Table.UnderBank.filename);
        GetComponent<SpriteRenderer>().sprite = underbank_sprite;
    }
}
