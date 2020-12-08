using MongoDTO;
using SocialWinFormApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialWFApp
{
    public partial class ViewComment : Form
    {
        private readonly CommentDTO _comment;
        public ViewComment(CommentDTO comment)
        {
            this._comment = comment;
            InitializeComponent();
            UpdateData();
        }
        private void UpdateData()
        {
            CommentText.Text = this._comment.CommentText;
        }
    }
}
