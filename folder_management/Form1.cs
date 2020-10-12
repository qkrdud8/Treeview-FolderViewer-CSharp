using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace folder_management
{
    public partial class Form1 : Form
    {
        string ppath;

        public Form1()
        {
            InitializeComponent();

        }


        public void openfolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                treeView1.Nodes.Add(Makenode(fbd.SelectedPath));
                ppath = fbd.SelectedPath;
            }
            
        }



        public TreeNode Makenode(string path)
        {
            TreeNode output = new TreeNode();
            DirectoryInfo di = new DirectoryInfo(path);
            output.Tag = path;
            output.Text = Path.GetFileName((string)output.Tag);
            

            if (di.Attributes == FileAttributes.Directory)
            {
                foreach (string d in Directory.GetDirectories(path))
                {
                    output.Nodes.Add(Makenode(d));
                }
                foreach (string d in Directory.GetFiles(path))
                {
                    output.Nodes.Add(Makenode(d));
                }
                
            }
            return output;            
        }



        private void delete_Click(object sender, EventArgs e)
        {
            if (Directory.Exists((string)treeView1.SelectedNode.Tag))
            {
                Directory.Delete((string)treeView1.SelectedNode.Tag, true);
                treeView1.SelectedNode.Remove();
            }
            else if (File.Exists((string)treeView1.SelectedNode.Tag)) 
            {
                File.Delete((string)treeView1.SelectedNode.Tag);
                treeView1.SelectedNode.Remove();
            }

        }

        private void addfolder_Click(object sender, EventArgs e)
        {
            string pathstring = (string)treeView1.SelectedNode.Tag;
            Directory.CreateDirectory(pathstring+"\\"+textBox1.Text);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(Makenode(ppath));
            
        }
    }
}
