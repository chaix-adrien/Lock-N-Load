    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using UnityEngine.Events;
     
    public class InputFieldSubmitOnly : InputField {
        protected override void Start () {
            base.Start();
     
            for (int i = 0; i < this.onEndEdit.GetPersistentEventCount(); ++i) {
                int index = i; // Local copy for listener delegate
                this.onEndEdit.SetPersistentListenerState(index, UnityEventCallState.Off);
                this.onEndEdit.AddListener(delegate(string text) {
                    if (!EventSystem.current.alreadySelecting) {
                        ((Component)this.onEndEdit.GetPersistentTarget(index)).SendMessage(this.onEndEdit.GetPersistentMethodName(index), text);
                    }
                });
            }
        }
    }
