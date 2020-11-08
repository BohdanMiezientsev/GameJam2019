using System;
using System.Runtime.Serialization;

namespace Resources
{
    [Serializable()]
    public class Player : ISerializable
    {
        private string _nickName;
        private int _score;

        public Player(string nickName, int score)
        {
            this._nickName = nickName;
            this._score = score;
        }
        
        //Deserialization constructor.
        public Player(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            _nickName = (String)info.GetValue("nickName", typeof(string));
            _score = (int)info.GetValue("score", typeof(int));
        }
        
        

        public string NickName
        {
            get => _nickName;
        }

        public int Score
        {
            get => _score;
        }
        
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("nickName", _nickName);
            info.AddValue("score", _score);
        }
    }
}