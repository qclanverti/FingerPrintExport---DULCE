using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FingerPrintExport
{
    public partial class amputeeInsert : Form
    {
        public amputeeInsert()
        {
            InitializeComponent();
        }
        string qry = "";
        private void amputeeInsert_Load(object sender, EventArgs e)
        {
           qry= "INSERT INTO [dbo].[tbl_bio]" +
        " ([RefNum]" +
        " ,[MIDRTN]" +
        " ,[fld_LeftPrimaryFP_template]" +
        " ,[fld_LeftPrimaryFP_IsOverride]" +
        " ,[fld_LeftPrimaryFP_Ansi]" +
        " ,[fld_LeftPrimaryFP_Wsq]" +
        " ,[fld_LeftSecondaryFP_template]" +
        " ,[fld_LeftSecondaryFP_IsOverride]" +
        " ,[fld_LeftSecondaryFP_Ansi]" +
        " ,[fld_LeftSecondaryFP_Wsq]" +
        " ,[fld_RightPrimaryFP_template]" +
        " ,[fld_RightPrimaryFP_IsOverride]" +
        " ,[fld_RightPrimaryFP_Ansi]" +
        " ,[fld_RightPrimaryFP_Wsq]" +
        " ,[fld_RightSecondaryFP_template]" +
        " ,[fld_RightSecondaryFP_IsOverride]" +
        " ,[fld_RightSecondaryFP_Ansi]" +
        " ,[fld_RightSecondaryFP_Wsq]" +
        " ,[EntryDate]" +
        " ,[LastUpdate])" +
     " VALUES" +
       " (@RefNum" +
       " ,@MIDRTN" +
       " ,'a'" +
       " ,1" +
       " ,@fld_LeftPrimaryFP_Ansi" +
       " ,@fld_LeftPrimaryFP_Wsq" +
       " ,'a'" +
       " ,1" +
       " ,@fld_LeftSecondaryFP_Ansi" +
       " ,@fld_LeftSecondaryFP_Wsq" +
       " ,'a'" +
       " ,1" +
       " ,@fld_RightPrimaryFP_Ansi" +
       " ,@fld_RightPrimaryFP_Wsq" +
       " ,'a'" +
       " ,1" +
       " ,@fld_RightSecondaryFP_Ansi" +
       " ,@fld_RightSecondaryFP_Wsq" +
       " ,getdate()" +
       " ,getdate())";

            
        }
        CSBaseDALMS dal = new CSBaseDALMS();
        private void textBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            if (opd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = opd.FileName;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string[] rec = File.ReadAllLines(textBox1.Text);

            foreach (string r in rec)
    {

                string refnum = r.Split(',')[0];
                 string mid = r.Split(',')[1];
                    byte[] blank =new byte[0];
              
                dal.cmd.Parameters.Clear();
               dal.cmd.Parameters.AddWithValue("@RefNum",refnum);
               dal.cmd.Parameters.AddWithValue("@MIDRTN", mid);
               //dal.cmd.Parameters.AddWithValue("@fld_LeftPrimaryFP_Ansi",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_LeftPrimaryFP_Wsq",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_LeftSecondaryFP_Ansi",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_LeftSecondaryFP_Wsq",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_RightPrimaryFP_Ansi",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_RightPrimaryFP_Wsq",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_RightSecondaryFP_Ansi",blank);
               //dal.cmd.Parameters.AddWithValue("@fld_RightSecondaryFP_Wsq",blank);
               dal.cmd.Parameters.Add("@fld_LeftPrimaryFP_Ansi",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_LeftPrimaryFP_Wsq",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_LeftSecondaryFP_Ansi",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_LeftSecondaryFP_Wsq",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_RightPrimaryFP_Ansi",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_RightPrimaryFP_Wsq",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_RightSecondaryFP_Ansi",SqlDbType.Image).Value = blank;
               dal.cmd.Parameters.Add("@fld_RightSecondaryFP_Wsq",SqlDbType.Image).Value = blank;
                bool res = dal.Execute(qry, CommandType.Text);
                if (res == true)
                {

                }
                else {
                    MessageBox.Show("Failed ");
                }}
        }
    }
}
