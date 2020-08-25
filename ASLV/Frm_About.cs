﻿/* This file is part of AHD ID3 Tag Editor (AITE)
 * A program that edit and create ID3 Tag.
 *
 * Copyright © Ala Ibrahim Hadid 2012 - 2015
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace AHD.ID3.Viewer
{
    public partial class Frm_About : Form
    {
        public Frm_About()
        {
            InitializeComponent();
            label_version.Text = Program.Version;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("http://sourceforge.net/projects/aite/"); }
            catch { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start("https://www.facebook.com/AhdId3TagEditor"); }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.LinkText);
            }
            catch { }
        }
    }
}
