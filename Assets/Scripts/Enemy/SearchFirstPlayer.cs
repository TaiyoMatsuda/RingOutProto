using UnityEngine;
using static IMortality;

public class SearchFirstPlayer : MonoBehaviour
{

    private IMortality _controller;

    void Start()
    {
        _controller = this.transform.root.gameObject.GetComponent<IMortality>();
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            State state = _controller.GetState();
            if (state != State.Chase)
            {
                _controller.SetState(State.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            _controller.SetState(State.Wait);
        }
    }
}