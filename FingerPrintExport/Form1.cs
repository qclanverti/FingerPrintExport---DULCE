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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

 void showMsgInfo(string msg)
 {
     MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
 }
    void showMsgError(string msg)
    {
        MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
        CSBaseDALMS dal = new CSBaseDALMS();
        DateTime date;
        private void button1_Click(object sender, EventArgs e)
        {

         
            if (!backgroundWorker1.IsBusy)
            {
                date = dateTimePicker1.Value;
                dateTimePicker2.Value = date;
                backgroundWorker1.RunWorkerAsync();
            }
            //timer1.Enabled = true;
            //timer1.Start();
           
            ////, [fld_LeftSecondaryFP_Wsq], [fld_RightPrimaryFP_Wsq], [fld_RightSecondaryFP_Wsq]
            //string sql = "SELECT b.midrtn,[fld_LeftPrimaryFP_Ansi],[fld_LeftSecondaryFP_Ansi],[fld_RightPrimaryFP_Ansi],[fld_RightSecondaryFP_Ansi] FROM TBL_MEMBER m join tbl_bio b on m.refnum = b.refnum WHERE branchcode in (02403,02402)";
            ////string sql = "SELECT b.midrtn,[fld_photo] FROM TBL_MEMBER m join tbl_photo b on m.refnum = b.refnum WHERE branchcode in (02403,02402)";//"SELECT b.midrtn, [fld_LeftSecondaryFP_Wsq]  FROM TBL_MEMBER m join tbl_bio b on m.refnum = b.refnum WHERE branchcode ='02403'";//in (02403,02402)";
            //DataTable dt = new DataTable();
            //dt = dal.GetDatatable(sql, CommandType.Text);


            //if (dt == null)
            //{
            //    showMsgError("Datatable is null");
            //return;}

            //if (dt.Rows.Count==0)
            //{
            //    showMsgError("Datatable is null");
            //    return;
            //}


            //foreach (DataRow dr in dt.Rows)
            //{
            //    string mid = dr[0].ToString();
            //    byte[] lp = (byte[])dr[1];
            //    byte[] rp = (byte[])dr[3];
            //    byte[] ls = (byte[])dr[2];
            //    byte[] rs = (byte[])dr[4];

            //    string lp_FileName = Application.StartupPath + "/ALLCARD EMPLOYEES/LP_Index/" + mid + ".ansi";

            //    System.IO.FileStream lp_FileStream = new System.IO.FileStream(lp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //    lp_FileStream.Write(lp, 0, lp.Length);
            //    lp_FileStream.Close();


            //    string ls_FileName = Application.StartupPath + "/ALLCARD EMPLOYEES/LS_Thumb/" + mid + ".ansi";

            //    System.IO.FileStream ls_FileStream = new System.IO.FileStream(ls_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //    ls_FileStream.Write(ls, 0, ls.Length);
            //    ls_FileStream.Close();


            //    string rp_FileName = Application.StartupPath + "/ALLCARD EMPLOYEES/RP_Index/" + mid + ".ansi";

            //    System.IO.FileStream rp_FileStream = new System.IO.FileStream(rp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //    rp_FileStream.Write(rp, 0, rp.Length);
            //    rp_FileStream.Close();


            //    string rs_FileName = Application.StartupPath + "/ALLCARD EMPLOYEES/RS_Thumb/" + mid + ".ansi";

            //    System.IO.FileStream rs_FileStream = new System.IO.FileStream(rs_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //    rs_FileStream.Write(rs, 0, rs.Length);
            //    rs_FileStream.Close();

            //}

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            amputeeInsert a = new amputeeInsert();
            a.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
        string extractionfolder = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            extractionfolder = Application.StartupPath + @"\EXTRACTED";
            if (!Directory.Exists(extractionfolder))
                Directory.CreateDirectory(extractionfolder);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //timer1.Stop();
        }
        int ctr;
        int ct;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ct=0;
                while (date < DateTime.Now)
                {
                    extractionfolder = Application.StartupPath + @"\EXTRACTED\EXTRACTED_" + date.ToString("yyyyMM");
                    if (!Directory.Exists(extractionfolder))
                        Directory.CreateDirectory(extractionfolder);

                    dal.cmd.Parameters.Clear();
                    dal.cmd.Parameters.AddWithValue("@sd", date.ToString("yyyy-MM-dd"));
                    dal.cmd.Parameters.AddWithValue("@ed", DateTime.Now.ToString("yyyy-MM-dd"));//not use
                    DataTable dt = new DataTable();
                    dt = dal.GetDatatable("spGet_BioPhotoSigNameData", CommandType.StoredProcedure);
                    if (dt == null)
                    {
                        showMsgError("Datatable is null. Try again. Start Date should be " + date.ToString("yyyy-MM-dd"));
                        return;
                    }
                    //
                    ctr = dt.Rows.Count;
                    ct = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string mid = dr[0].ToString();
                        string lastname = dr[1].ToString();
                        string firstname = dr[2].ToString();
                        string middlename = dr[3].ToString();
                        string ext = dr[4].ToString();
                        string bdate = Convert.ToDateTime(dr[5]).ToString("yyyy-MM-dd");
                        byte[] lp = (byte[])dr[6];
                        byte[] rp = (byte[])dr[8];
                        byte[] ls = (byte[])dr[7];
                        byte[] rs = (byte[])dr[9];
                        byte[] wlp = (byte[])dr[10];
                        byte[] wrp = (byte[])dr[12];
                        byte[] wls = (byte[])dr[11];
                        byte[] wrs = (byte[])dr[13];

                        byte[] poto = (byte[])dr[14];

                        //folder
                        string midfolder = extractionfolder + @"\" + mid;
                        if (!Directory.Exists(midfolder))
                        {
                            Directory.CreateDirectory(midfolder);
                        }
                        //txt
                        string txtfile = midfolder + @"\" + mid + ".txt";
                        string str = "MID,LASTNAME,FIRSTNAME,MIDDLENAME,EXT,BIRTHDATE";
                        File.AppendAllText(txtfile, str + Environment.NewLine);
                        str = mid + "," + lastname + "," + firstname + "," + middlename + "," + ext + "," + bdate;
                        File.AppendAllText(txtfile, str);

                        //

                        string lp_FileName = midfolder + @"\" + mid + "_LP.ansi";
                        string ls_FileName = midfolder + @"\" + mid + "_LS.ansi";
                        string rp_FileName = midfolder + @"\" + mid + "_RP.ansi";
                        string rs_FileName = midfolder + @"\" + mid + "_RS.ansi";
                        string wlp_FileName = midfolder + @"\" + mid + "LP.wsq";
                        string wls_FileName = midfolder + @"\" + mid + "LS.wsq";
                        string wrp_FileName = midfolder + @"\" + mid + "RP.wsq";
                        string wrs_FileName = midfolder + @"\" + mid + "RS.wsq";


                        string poto_FileName = midfolder + @"\" + mid + ".jpg";

                        using (FileStream lp_FileStream = new System.IO.FileStream(lp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            lp_FileStream.Write(lp, 0, lp.Length);
                            lp_FileStream.Close();
                        }
                        using (FileStream ls_FileStream = new System.IO.FileStream(ls_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            ls_FileStream.Write(ls, 0, ls.Length);
                            ls_FileStream.Close();
                        }
                        using (FileStream rp_FileStream = new System.IO.FileStream(rp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            rp_FileStream.Write(rp, 0, rp.Length);
                            rp_FileStream.Close();
                        }
                        using (FileStream rs_FileStream = new System.IO.FileStream(rs_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            rs_FileStream.Write(rs, 0, rs.Length);
                            rs_FileStream.Close();
                        }
                        using (FileStream wlp_FileStream = new System.IO.FileStream(wlp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            wlp_FileStream.Write(wlp, 0, wlp.Length);
                            wlp_FileStream.Close();
                        }
                        using (FileStream wls_FileStream = new System.IO.FileStream(wls_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            wls_FileStream.Write(wls, 0, wls.Length);
                            wls_FileStream.Close();
                        }
                        using (FileStream wrp_FileStream = new System.IO.FileStream(wrp_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            wrp_FileStream.Write(wrp, 0, wrp.Length);
                            wrp_FileStream.Close();
                        }
                        using (FileStream wrs_FileStream = new System.IO.FileStream(wrs_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            wrs_FileStream.Write(wrs, 0, wrs.Length);
                            wrs_FileStream.Close();
                        }
                        using (FileStream wrs_FileStream = new System.IO.FileStream(wrs_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            wrs_FileStream.Write(wrs, 0, wrs.Length);
                            wrs_FileStream.Close();
                        }

                        using (FileStream poto_FileStream = new System.IO.FileStream(poto_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            poto_FileStream.Write(poto, 0, poto.Length);
                            poto_FileStream.Close();
                        }
                        ct += 1;
                        backgroundWorker1.ReportProgress(ct);
                    }


                    date = date.AddDays(1);
                    //
                  //  ctr = Convert.ToInt32((ct / (DateTime.Now - date).TotalDays)*100);
                 
                
                }
            }
            catch (Exception ex)
            {
                showMsgError(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label3.Text = ct.ToString() + " of " + ctr.ToString();
            dateTimePicker2.Value = date;
        }
    }
}
