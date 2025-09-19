using UnityEngine;
using UnityEngine.UI;

public class BeatInvenManager : MonoBehaviour
{
    public RectTransform BombInvenTransform;
    public RectTransform DetoInvenTransform;

    public int Num;
    public float Space;
    public Vector2 Size;

    private Button[] _bombButtons;
    private Button[] _detoButtons;

    private void ClickBombInven(int index)
    {
        GameManager.Instance.Player.PlayerController.BombBeat[index] = true;

        GameObject go = new GameObject($"idk", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(this.transform, worldPositionStays: false);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = Size;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        RectTransform origin = BombInvenTransform;
        Vector3 originWorld = origin.TransformPoint(origin.rect.center);
        Vector3 originLocal = transform.InverseTransformPoint(originWorld);
        Vector2 pos = (Vector2)originLocal + new Vector2(index * Space, 0f);
        rt.anchoredPosition = pos;

    }

    private void ClickDetoInven(int index)
    {
        GameManager.Instance.Player.PlayerController.DetoBeat[index] = true;

        GameObject go = new GameObject($"idk", typeof(RectTransform), typeof(Image));
        go.transform.SetParent(this.transform, worldPositionStays: false);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = Size;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);

        RectTransform origin = DetoInvenTransform;
        Vector3 originWorld = origin.TransformPoint(origin.rect.center);
        Vector3 originLocal = transform.InverseTransformPoint(originWorld);
        Vector2 pos = (Vector2)originLocal + new Vector2(index * Space, 0f);
        rt.anchoredPosition = pos;

    }

    private void Start()
    {
        _bombButtons = CreateRow(
            origin: BombInvenTransform,
            count: Num,
            onClick: ClickBombInven,
            rowName: "BombBtn_"
        );

        _detoButtons = CreateRow(
            origin: DetoInvenTransform,
            count: Num,
            onClick: ClickDetoInven,
            rowName: "DetoBtn_"
        );
    }

    private Button[] CreateRow(RectTransform origin, int count, System.Action<int> onClick, string rowName)
    {
        var buttons = new Button[count];

        Vector3 originWorld = origin.TransformPoint(origin.rect.center);
        Vector3 originLocal = transform.InverseTransformPoint(originWorld);

        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject($"{rowName}{i}", typeof(RectTransform), typeof(Image), typeof(Button));
            go.transform.SetParent(this.transform, worldPositionStays: false);

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = Size;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            Vector2 pos = (Vector2)originLocal + new Vector2(i * Space, 0f);
            rt.anchoredPosition = pos;

            Image img = go.GetComponent<Image>();
            img.raycastTarget = true;
            img.color = new(0f, 0f, 0f, 0f);

            Button btn = go.GetComponent<Button>();
            int capturedIndex = i;
            btn.onClick.AddListener(() => onClick?.Invoke(capturedIndex));

            buttons[i] = btn;
        }

        return buttons;
    }
}
