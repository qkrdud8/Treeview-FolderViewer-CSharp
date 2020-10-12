﻿using System;
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
                //선택 됬을때 (DialogResult가 OK일때 실행되야 함으로 if문안에 들어가야함
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
                output.Nodes.Add("");
                //변수 string d, f는 각각 foreach문에서만 동작하는 지역변수임으로 같은 이름을 사용하여도 상관없음
                //foreach (string d in Directory.GetDirectories(path))
                //{
                //    output.Nodes.Add(Makenode(d));
                //}
                //foreach (string f in Directory.GetFiles(path))
                //{
                //  output.Nodes.Add(Makenode(f));
                //}
                
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
            else //파일이 존제하는지도 확인을 해야함.(오류방지)
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

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            foreach (string d in Directory.GetDirectories((string)e.Node.Tag))
            {
                TreeNode tn = new TreeNode();
                tn.Tag = d;
                tn.Text = Path.GetFileName(d);
                tn.Nodes.Add("");
                e.Node.Nodes.Add(tn);
            }
            foreach (string d in Directory.GetFiles((string)e.Node.Tag))
            {
                TreeNode tn = new TreeNode();
                tn.Tag = d;
                tn.Text = Path.GetFileName(d);
                e.Node.Nodes.Add(tn);
            }
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add("");

            
        }
    }
}
