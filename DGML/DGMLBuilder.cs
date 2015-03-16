using System.Collections.Generic;

namespace DataPS.DGML
{
    public class DGMLBuilder
    {
        private Dictionary<string,Node> nodes;
        private List<Link> links;
        private List<Category> categories;

        public DGMLBuilder()
        {
            init();
        }

        private void init()
        {
            this.GraphAttributes = new GraphAttributes();
            this.nodes = new Dictionary<string, Node>();           
            this.links = new List<Link>();
            this.categories = new List<Category>();

            this.AutoCreateNodes = true;
            this.AutoMergeNodes = true;
        }

        public Node AddNode(string id)
        {
            var n = this.AddNode(id, null);
            return n;
        }

        public Node AddNode(string id,string label)
        {
            var n = this.AddNode(id, label, null);
            return n;
        }


        public Node AddNode(string id, string label, NodeAttributes attributes)
        {
            Node n=null;

            if (this.nodes.ContainsKey(id))
            {
                if (!AutoMergeNodes)
                {
                    string msg = string.Format("Node with id=\"{0}\" already exists", id);
                    throw new DGMLException(msg);
                }

                n = this.nodes[id];
                if (attributes!=null)
                {
                    n.Attributes = attributes;
                }
                else
                {
                    // leave the existing node alone
                }
            }
            else
            {
                if (attributes==null)
                {
                    attributes = new NodeAttributes();
                }
                n = new Node(id, attributes);
                n.Label = label;
            }    

            this.nodes[id] = n;
            return n;
        }

        public Link AddLink(string from, string to)
        {
            return this.AddLink(from, to, null);
        }

        public Link AddLink(string from, string to, LinkAttributes attributes)
        {
            var from_node = this.GetNode(from);
            var to_node = this.GetNode(to);
            return AddLink(from_node, to_node, attributes);
        }

        public Link AddLink(Node from_node, Node to_node, LinkAttributes attributes)
        {
            if (attributes == null)
            {
                attributes = new LinkAttributes();
            }
            var link = new Link(from_node.ID, to_node.ID, attributes);
            this.links.Add(link);
            return link;
        }

        public Category AddCategory(string id, bool iscontainment)
        {
            var cat = new Category(id);
            cat.IsContainment= true;

            this.categories.Add(cat);
            return cat;
        }
        
        private Node GetNode(string id)
        {
            if (this.nodes.ContainsKey(id))
            {
                return this.nodes[id];
            }
            else
            {
                if (AutoCreateNodes)
                {
                    var n = this.AddNode(id);
                    return n;
                }
                else
                {
                    string msg = string.Format("Node id=\"{0}\" does not exist", id);
                    throw new DGMLException(msg);
                }
            }
        }

        public IEnumerable<Link> Links
        {
            get
            {
                return this.links;
            }
        }

        public IEnumerable<Node> Nodes
        {
            get
            {
                return this.nodes.Values;
            }
        }

        public GraphAttributes GraphAttributes { get; private set; }
        public NodeAttributes DefaultNodeAttributes { get; private set; }
        public LinkAttributes DefaultLinkAttributes { get; private set; }
        public bool AutoCreateNodes { get; set; }
        public bool AutoMergeNodes { get; set; }

        public void Save(string filename)
        {
            if (filename == null)
            {
                throw new System.ArgumentNullException("filename");
            }

            var dw = new DGML.DGMLWriter(filename);

            // Start
            dw.StartDocument();
            dw.StartDirectedGraph(this.GraphAttributes);
            
            // Nodes
            dw.StartNodes();
            foreach (Node n in this.nodes.Values)
            {
                dw.Node(n.ID,n.Label,n.Attributes);
            }
            dw.EndNodes();

            dw.StartLinks();
            foreach (Link link in this.links)
            {
                dw.Link(link.From,link.To,link.Attributes);
            }
            dw.EndLinks();

            // Categories
            dw.StartCategories();
            foreach (var cat in this.categories)
            {
                dw.Category(cat);
            }
            dw.EndCategories();

            // Properties
            dw.StartProperties();
            dw.EndProperties();

            // Styles
            dw.StartStyles();
            dw.EndStyles();

            // End
            dw.EndDirectedGraph();
            dw.EndDocument();
            dw.Close();
        }

    }
}