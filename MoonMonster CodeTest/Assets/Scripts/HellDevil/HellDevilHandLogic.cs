using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellDevilHandLogic : MonoBehaviour
{
    #region Editor Fields

    [SerializeField]
    private GameObject _hand;
    [SerializeField]
    private GameObject _openedPalm;
    [SerializeField]
    private GameObject _closedPalm;
    [SerializeField]
    private float _lowestYPos = 0f;
    [SerializeField]
    private float _downVelocity = -33f;
    [SerializeField]
    private float _highestYPos = 100f;
    [SerializeField]
    private float _upVelocity = 20f;
    [SerializeField]
    private float _grabDuration = 2f;

    #endregion

    #region Methods

    private void Start()
    {
        StartCoroutine(TakeThemToHell());
    }

    private IEnumerator TakeThemToHell()
    {
        yield return StartCoroutine(MoveHand(_lowestYPos, _downVelocity));
        yield return StartCoroutine(Grab());
        yield return StartCoroutine(MoveHand(_highestYPos, _upVelocity));

        Destroy(gameObject);
    }

    private IEnumerator MoveHand(float yPos, float yVelocity)
    {
        Vector3 handPos = Vector3.zero;

        while (Mathf.Sign(yVelocity) < 0 ? _hand.transform.localPosition.y >  yPos : _hand.transform.localPosition.y < yPos)
        {
            handPos = _hand.transform.position;
            handPos.y += yVelocity * Time.deltaTime;
            _hand.transform.position = handPos;
            yield return null;
        }
        handPos.y = yPos;
        _hand.transform.position = handPos;

    }
    private IEnumerator Grab()
    {
        _openedPalm.SetActive(false);
        _closedPalm.SetActive(true);

        yield return new WaitForSeconds(_grabDuration);
    }

    #endregion
}
