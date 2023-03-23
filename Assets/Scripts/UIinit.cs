
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIinit : MonoBehaviour
{
    // Start is called before the first frame update
    #region initButton

    public void initSlot(int slotCount, GameObject slotPrefab, Transform slotContent)
    {
        int length = 1;

        //創建slot
        for (int i = 0; i < slotCount; i++)
        {
            var index = i; // 保存当前索引值
            var temp = Instantiate(slotPrefab, slotContent.position, Quaternion.identity, slotContent);
            StartCoroutine(AddListener(temp.GetComponent<Button>(), index));
        }


        length = (int)(slotContent.parent.GetComponent<RectTransform>().sizeDelta.x / 100.0f);

        slotContent.position -= new Vector3(0, 1000, 0);
        slotContent.GetComponent<GridLayoutGroup>().constraintCount = length;
    }

    IEnumerator AddListener(Button btn, int i)
    {
        btn.onClick.AddListener(() => slot_event(i));
        yield return null;
    }

    public virtual void slot_event(int i)
    {
        return;
    }

    #endregion
}
