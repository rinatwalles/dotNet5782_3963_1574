//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Runtime.Serialization;


//namespace DAL
//{
//    class Exceptions
//    {
//        [Serializable]
//        public class BadDroneIdExcption : Exception
//        {

//            public int Id;

//            public BadDroneIdExcption(int id) : base() => Id = id;
//            public BadDroneIdExcption(int id,string message) : base(message) => Id = id;
//            public BadDroneIdExcption(int id,string message, Exception inner) : base(message, inner) => Id = id;

//            override public string ToString() => base.ToString() + $",exicting Drone id:{Id}";
//        }

//    }
//    public class BadParcelIdExcption : Exception
//    {

//        public int Id;

//        public BadParcelIdExcption(int id) : base() => Id = id;
//        public BadParcelIdExcption(int id, string message) : base(message) => Id = id;
//        public BadParcelIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

//        override public string ToString() => base.ToString() + $",exicting Parcel id:{Id}";
//    }
//    public class BadStationIdExcption : Exception
//    {

//        public int Id;

//        public BadStationIdExcption(int id) : base() => Id = id;
//        public BadStationIdExcption(int id, string message) : base(message) => Id = id;
//        public BadStationIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

//        override public string ToString() => base.ToString() + $",exicting Station id:{Id}";
//    }
//    public class BadCustomerIdExcption : Exception
//    {

//        public int Id;

//        public BadCustomerIdExcption(int id) : base() => Id = id;
//        public BadCustomerIdExcption(int id, string message) : base(message) => Id = id;
//        public BadCustomerIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

//        override public string ToString() => base.ToString() + $",exicting Station id:{Id}";
//    }
//    public class NotExcitingIdExcption : Exception
//    {

//        public int Id;

//        public NotExcitingIdExcption(int id) : base() => Id = id;
//        public NotExcitingIdExcption(int id, string message) : base(message) => Id = id;
//        public NotExcitingIdExcption(int id, string message, Exception inner) : base(message, inner) => Id = id;

//        override public string ToString() => base.ToString() + $",not exicting Drone id:{Id}";
//    }

//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    [Serializable]
    public class MissingIdException : Exception
    {
        public int ID;

        public string EntityName;
        public MissingIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - missing id: {ID}";
    }

    [Serializable]
    public class DuplicateIdException : Exception
    {
        public int ID;

        public string EntityName;
        public DuplicateIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - duplicate id: {ID}";
    }
}