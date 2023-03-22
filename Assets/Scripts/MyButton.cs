using UnityEngine.UI;
using UnityEngine.Events;

public class MyButton : Selectable
{
    public UnityEvent OnClick;

    bool activated = false;

    SelectionState selectionState;
    
    void Update()
    {
        selectionState = currentSelectionState;

        if(selectionState == SelectionState.Pressed)
        {
            if(activated == false)
            {
                OnClick.Invoke();
                activated = true;
            }
        }
        else
        {
            activated = false;
        }
    }
}
