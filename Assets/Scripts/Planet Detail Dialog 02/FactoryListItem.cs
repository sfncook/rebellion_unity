using UnityEngine;

public class FactoryListItem : MonoBehaviour
{
    public SpriteRenderer factoryImg;

    public void setFactory(Factory factory)
    {
        factoryImg.sprite = Resources.Load<Sprite>("Images/Factories/" + factory.type.name);
    }
}
