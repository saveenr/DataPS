namespace DataPS.DGML
{
    public class Link
    {
        public Link(string from, string to)
        {
            this.From = @from;
            this.To = to;
        }

        public Link(string from, string to, LinkAttributes attributes)
        {
            this.From = @from;
            this.To = to;
            this.Attributes = attributes;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public LinkAttributes Attributes { get; set; }
    }

    public class Category
    {

            //<Category Id="Contains" Label="Contains" Description="Whether the source of the link contains the target object" CanBeDataDriven="False" CanLinkedNodesBeDataDriven="True" IncomingActionLabel="Contained By" IsContainment="True" OutgoingActionLabel="Contains" />

        public string ID;
        public bool IsContainment;

        public Category(string ID)
        {
            this.ID = ID;
        }

    }
}