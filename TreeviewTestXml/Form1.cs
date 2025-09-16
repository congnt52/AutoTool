using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TreeviewTestXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.AfterCheck += TreeView1_AfterCheck;

        }
        private XmlDocument XmlDoc = new XmlDocument();
        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Tránh lặp sự kiện khi cập nhật node con
            treeView1.AfterCheck -= TreeView1_AfterCheck;

            // Cập nhật tất cả node con theo trạng thái của node cha
            CheckAllChildNodes(e.Node, e.Node.Checked);

            treeView1.AfterCheck += TreeView1_AfterCheck;
        }

        private void CheckAllChildNodes(TreeNode parent, bool isChecked)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                child.Checked = isChecked;
                if (child.Nodes.Count > 0)
                {
                    CheckAllChildNodes(child, isChecked);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();

            XmlDoc.Load(Application.StartupPath + @"\\Test.xml");

            LoadTreeViewFromXmlDoc(XmlDoc, treeView1);


            treeView1.ExpandAll();
        }
        private void LoadTreeViewFromXmlDoc(XmlDocument xml_doc, TreeView trv)
        {

            trv.Nodes.Clear();
            AddTreeViewNode(trv.Nodes, xml_doc.DocumentElement);
        }

        private void AddTreeViewNode(TreeNodeCollection parent_nodes, XmlNode xml_node)
        {

            TreeNode new_node = parent_nodes.Add(xml_node.Name);

            foreach (XmlNode child_node in xml_node.ChildNodes)
            {
                AddTreeViewNode(new_node.Nodes, child_node);
            }
        }
    }
}
