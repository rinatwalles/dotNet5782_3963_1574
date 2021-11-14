using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace DAL
{
    class Exceptions
    {
        [Serializable]
        public class BadDroneIdExcption : Exception
        {

            public int Id;

            public BadDroneIdExcption(int id) : base() => Id = id;
            public BadDroneIdExcption(int id,string message) : base(message) => Id = id;
            public BadDroneIdExcption(int id,string message, Exception inner) : base(message, inner) => Id = id;

            override public string ToString() => base.ToString() + $",bad Drone id:{Id}";
        }

    }
    public class BadParcelIdExcption : Exception
    {

        public int Id;

        public BadParcelIdExcption(int id) : base() => Id = id;
        public BadParcelIdExcption(int id, string message) : base(message) => Id = id;
        public BadParcelIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

        override public string ToString() => base.ToString() + $",bad Parcel id:{Id}";
    }
    public class BadStationIdExcption : Exception
    {

        public int Id;

        public BadStationIdExcption(int id) : base() => Id = id;
        public BadStationIdExcption(int id, string message) : base(message) => Id = id;
        public BadStationIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

        override public string ToString() => base.ToString() + $",bad Station id:{Id}";
    }
    public class BadCustomerIdExcption : Exception
    {

        public int Id;

        public BadCustomerIdExcption(int id) : base() => Id = id;
        public BadCustomerIdExcption(int id, string message) : base(message) => Id = id;
        public BadCustomerIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

        override public string ToString() => base.ToString() + $",bad Station id:{Id}";
    }


}

