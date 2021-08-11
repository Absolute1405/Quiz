using System;
using UnityEngine;

public class PointerRaycaster : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private LayerMask _cardLayer;
    [SerializeField]
    private MonoBehaviour _levelLoader;
    private ILevelLoader LevelLoader => (ILevelLoader)_levelLoader;

    private float _distance;

    private void OnValidate()
    {
        if (_levelLoader is ILevelLoader)
            return;

        Debug.LogError(_levelLoader.name + " needs to implement " + nameof(ILevelLoader));
        _levelLoader = null;
    }



    private void Awake()
    {
        if (_camera is null)
            throw new ArgumentNullException(nameof(_camera));

        _distance = Mathf.Abs(_camera.transform.position.z * 2);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _distance, _cardLayer);

            if (hit)
                CheckHit(hit);
        }
    }

    private void CheckHit(RaycastHit2D hit)
    {
        string key = hit.transform.gameObject.GetComponent<Card>().Symbol.Key;

        bool isAnswer = LevelLoader.CurrentAnswer == key;
        hit.transform.gameObject.GetComponent<CardAnimation>().AnimateClick(isAnswer, out var duration);

        if (isAnswer)
            StartCoroutine(LevelLoader.LoadNew(duration));
    }
}
