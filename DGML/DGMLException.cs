namespace DataPS.DGML
{
    [global::System.Serializable]
    public class DGMLException : System.Exception
    {
        public DGMLException() { }
        public DGMLException(string message) : base(message) { }
        public DGMLException(string message, System.Exception inner) : base(message, inner) { }
        protected DGMLException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}