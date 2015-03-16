namespace DataPS.DGML
{
    public class Node
    {
        private readonly string id;
        public NodeAttributes Attributes { get; set; }
        public string Label;

        public Node(string id)
        {
            this.id = id;
            this.Attributes = new NodeAttributes();
        }

        public Node(string id, NodeAttributes attributes)
        {
            this.id = id;
            this.Attributes = attributes;
        }

        public Node(string id, string label, NodeAttributes attributes)
        {
            this.id = id;
            this.Label = label;
            this.Attributes = attributes;
        }

        public string ID
        {
            get
            {
                return this.id;
            }
        }
    }
}