using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EasyUpdateDemoSDK
{


    public class MsgSystem : MonoBehaviour
    {
        public static MsgSystem instance;

        void Awake()
        {
            instance = this;
        }

        void OnDestroy()
        {
            instance = null;
        }



        public bool is_pause = false;


        [System.Serializable]
        public class MsgReciever
        {
            public string reciever_name = "";
            public int reciever_priority = 0 ;
            public UnityAction<System.Object[]> reciever_callback;
        }
 

        public Dictionary<string, List<MsgReciever>> msg_recievers = new Dictionary<string, List<MsgReciever>>();


        public void RemoveItemToDictionaryWithListValue<T1, T2>(Dictionary<T1, List<T2>> dictionary, T1 key_name, T2 value_action)
        {
            if (dictionary.ContainsKey(key_name) == true)
            {
                if (dictionary[key_name].Contains(value_action) == true)
                    dictionary[key_name].Remove(value_action);
            }
        }



        public void RegistMsgAction(string msg_name, UnityAction<System.Object[]> msg_action , string reciever_name = "" , int reciever_priority = 0)
        {
            if (msg_recievers.ContainsKey(msg_name) == false)
                msg_recievers.Add(msg_name, new List<MsgReciever>());

            MsgReciever reciever = new MsgReciever();
            reciever.reciever_name = reciever_name;
            reciever.reciever_priority = reciever_priority;
            reciever.reciever_callback = msg_action;

            List<MsgReciever> recievers_list = msg_recievers[msg_name];

            recievers_list.Add(reciever);
            recievers_list.Sort((x, y) => -x.reciever_priority.CompareTo(y.reciever_priority));

            OnRecieverRegist(msg_name, msg_action);
        }


        public void RemoveMsgAction(string msg_name, UnityAction<System.Object[]> msg_action)
        {
            if (msg_recievers.ContainsKey(msg_name) == false)
                return;

            List<MsgReciever> recievers_list = msg_recievers[msg_name];

            for (int i = recievers_list.Count - 1; i >= 0; i--)
                if (recievers_list[i].reciever_callback == msg_action)
                    recievers_list.RemoveAt(i);
        }



        public void RemoveMsgAction(string msg_name, string reciever_name  )
        {
            if (msg_recievers.ContainsKey(msg_name) == false)
                return;

            List<MsgReciever> recievers_list = msg_recievers[msg_name];

            for (int i = recievers_list.Count - 1; i >= 0; i--)
                if (recievers_list[i].reciever_name == reciever_name)
                    recievers_list.RemoveAt(i);
        }














        public void SendMsg(string msg_name, System.Object[] msg_content)
        {
            if (msg_recievers.ContainsKey(msg_name) == false)
                return;

            List<MsgReciever> recievers_list = msg_recievers[msg_name];

            for (int i = 0; i < recievers_list.Count; i++)
                if (recievers_list[i].reciever_callback != null) recievers_list[i].reciever_callback(msg_content);
        }



        public void ClearAllMsg()
        {
            msg_recievers.Clear();
        }


        [System.Serializable]
        public class BulletinMsg
        {
            public string msg_name = "";
            public System.Object[] msg_content;
        }

        public List<BulletinMsg> bulletin_msgs = new List<BulletinMsg>();


        public void SendBulletinMsg( string msg_name, System.Object[] msg_content )
        {
            BulletinMsg msg = new BulletinMsg();
            msg.msg_name = msg_name;
            msg.msg_content = msg_content;

            bulletin_msgs.Add(msg);

            SendMsg(msg_name,msg_content);
        }


        public void ClearBulletinMsg( string msg_name )
        {
            for (int i = bulletin_msgs.Count - 1; i >= 0; i--)
                if (bulletin_msgs[i].msg_name == msg_name)
                    bulletin_msgs.RemoveAt(i);
        }

        public void ClearBulletinMsg()
        {
            bulletin_msgs.Clear();
        }



        public void OnRecieverRegist( string msg_name, UnityAction<System.Object[]> msg_action )
        {
            for (int i = 0; i < bulletin_msgs.Count; i++)
                if (bulletin_msgs[i].msg_name == msg_name)
                    msg_action(bulletin_msgs[i].msg_content);
        }







    }
}
