using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XRMasters.Networking {

    public class CloudEventTrigger : EventTrigger {


        private void OnEnable() {
        }
        private void OnDisable() {
        }

        public override void OnPointerClick(PointerEventData eventData) {
            ExecuteEvent(EventTriggerType.PointerClick, eventData);
        }
        public override void OnPointerDown(PointerEventData eventData) {
            ExecuteEvent(EventTriggerType.PointerDown, eventData);
        }
        public override void OnPointerUp(PointerEventData eventData) {
            ExecuteEvent(EventTriggerType.PointerUp, eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData) {
            ExecuteEvent(EventTriggerType.PointerEnter, eventData);
        }
        public override void OnPointerExit(PointerEventData eventData) {
            ExecuteEvent(EventTriggerType.PointerExit, eventData);
        }

        protected void ExecuteEvent(EventTriggerType type, PointerEventData eventData, bool sendToNetwork = true) {
            var callback = GetEventCallback(type);
            if (callback != null) {
                callback.Invoke(eventData);
            }
        }

        protected TriggerEvent GetEventCallback(EventTriggerType id) {
            for (int i = 0, imax = triggers.Count; i < imax; ++i) {
                var ent = triggers[i];
                if (ent.eventID == id)
                    return ent.callback;
            }
            return null;
        }

    }

}

