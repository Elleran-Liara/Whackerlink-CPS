﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Whackerlink_CPS
{
    public partial class SystemForm : Form
    {
        private TreeNode _selectedNode;
        private List<string> _systemNames;
        private List<string> _systemIds;

        public event EventHandler<SystemUpdateEventArgs> SystemUpdated;

        public void InvokeUpdateButton()
        {
            btnUpdate.PerformClick();
        }
        public SystemForm(TreeNode selectedNode, List<string> systemNames, List<string> systemIds)
        {
            InitializeComponent();
            _selectedNode = selectedNode;
            _systemNames = systemNames;
            _systemIds = systemIds;

            LoadData();
        }

        private void LoadData()
        {
            if (_selectedNode.Tag is SystemData systemData)
            {
                txtName.Text = systemData.Name;
                txtAddress.Text = systemData.Address;
                txtPort.Text = systemData.Port;
                txtRid.Text = systemData.Rid;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedNode.Tag is SystemData systemData)
            {
                var originalSystemName = systemData.Name;

                systemData.Name = txtName.Text;
                systemData.Address = txtAddress.Text;
                systemData.Port = txtPort.Text;
                systemData.Rid = txtRid.Text;

                SystemUpdated?.Invoke(this, new SystemUpdateEventArgs
                {
                    OriginalSystemName = originalSystemName,
                    SystemName = systemData.Name,
                    Address = systemData.Address,
                    Port = systemData.Port,
                    Rid = systemData.Rid
                });

                // Update the tree view node
                _selectedNode.Text = systemData.Name;
            }
        }

        public class SystemUpdateEventArgs : EventArgs
        {
            public string OriginalSystemName { get; set; }
            public string SystemName { get; set; }
            public string Address { get; set; }
            public string Port { get; set; }
            public string Rid { get; set; }
        }
    }
    public class SystemData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Rid { get; set; } 

    }
}
    



