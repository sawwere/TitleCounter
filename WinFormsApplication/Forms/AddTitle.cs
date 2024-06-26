﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace hltb
{
    public partial class AddContent : Form
    {
        public AddContent()
        {
            InitializeComponent();
        }

        public void RecieveResponse(string title, Image decodedImage)
        {
            contentPicture.Image = new Bitmap(decodedImage);
            nameLabel.Text =  title;
        }

        /// <summary>
        /// Change statusLabel text according to search operation result
        /// Set addButton visible if operationCode = 0 allowing to add new content
        /// </summary>
        /// <returns>String splitted into rows</returns>
        public void SetStatus(char operationCode)
        {
            statusLabel.Text = "Status";
            if (operationCode != '0')
            {
                addButton.Visible = false;
                switch (operationCode)
                {
                    case '1':
                        statusLabel.Text += (": title has not found");
                        break;
                    case '2':
                        statusLabel.Text += (": title is already in the list");
                        break;
                    case '3':
                        statusLabel.Text += (": incorrect type. Choose correct mode");
                        break;
                }
            }
            else
            {
                addButton.Visible = true;
                statusLabel.Text += ": Found succesfuly";
            }
        }
    }
}
