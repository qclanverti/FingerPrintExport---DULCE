using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;

class CSBaseDALMS: IDisposable
{
    public static SqlConnection cn = new SqlConnection();
    public SqlCommand cmd = new SqlCommand();
    public static SqlDataAdapter da = new SqlDataAdapter();
    public static SqlDataReader rd;
    public static long rtrnVal;
    public string constr = string.Format("Server={0};Database= {1};User= {2}; Password= {3};", FingerPrintExport.Properties.Settings.Default.Server, FingerPrintExport.Properties.Settings.Default.Database, FingerPrintExport.Properties.Settings.Default.Username, FingerPrintExport.Properties.Settings.Default.Password);
    //public string constr1 = string.Format("Server={0};Database= {1};User= {2}; Password= {3};", PagIBIGDummyRecard.Properties.Settings.Default.Server, PagIBIGDummyRecard.Properties.Settings.Default.Database_Phase1, PagIBIGDummyRecard.Properties.Settings.Default.Username, PagIBIGDummyRecard.Properties.Settings.Default.Password);
    public string tempConstr { get; set; }
    public DataTable getDataTableTestCon(string strCmd, CommandType cmdType)
    {
        DataTable dt = new DataTable();
        cn = new SqlConnection(tempConstr);

        try
        {
            if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
            {
                cn.Close();
            }

            cmd.CommandText = strCmd;
            cmd.CommandType = cmdType;
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            cn.Open();
            cmd.CommandTimeout = 0;
            //cmd.ExecuteNonQuery();
            da.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            cmd.Parameters.Clear();
            da.Dispose();
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
    public virtual DataTable GetDatatable(string strCmd, CommandType cmdType)
    {
        DataTable dt = new DataTable();
        cn = new SqlConnection(constr);

        try
        {
            if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
            {
                cn.Close();
            }
            
            cmd.CommandText = strCmd;
            cmd.CommandType = cmdType;
            cmd.Connection = cn;
            da.SelectCommand = cmd;
            cn.Open();
            cmd.CommandTimeout = 3600;
            //cmd.ExecuteNonQuery();
            da.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            cmd.Parameters.Clear();
            da.Dispose();
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }

    }
    //public virtual DataTable GetDatatable1(string strCmd, CommandType cmdType)
    //{
    //    DataTable dt = new DataTable();
    //    cn = new SqlConnection(constr1);

    //    try
    //    {
    //        if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
    //        {
    //            cn.Close();
    //        }

    //        cmd.CommandText = strCmd;
    //        cmd.CommandType = cmdType;
    //        cmd.Connection = cn;
    //        da.SelectCommand = cmd;
    //        cn.Open();
    //        cmd.CommandTimeout = 3600;
    //        //cmd.ExecuteNonQuery();
    //        da.Fill(dt);
    //        return dt;
    //    }
    //    catch (Exception e)
    //    {
    //        return null;
    //    }
    //    finally
    //    {
    //        cmd.Parameters.Clear();
    //        da.Dispose();
    //        if (cn.State == System.Data.ConnectionState.Open)
    //        {
    //            cn.Close();
    //        }
    //    }

    //}
    public virtual bool Execute(string strCmd, CommandType cmdType)
    {
        cn = new SqlConnection(constr);

        try
        {

            cmd.CommandText = strCmd;
            cmd.Connection = cn;
            cmd.CommandType = cmdType;
            cn.Open();

            if (DBNull.Value!=cmd.ExecuteScalar())
            {

                return true;            // result is not null/return result then return true
            }
            else 
            {
                return false;           // no result or null return false
            }

            //--return true;
        }
        catch (Exception e)
        {
            // MessageBox.Show(e.Message );
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"/Logs/" + DateTime.Now.ToString("yyyyMMdd") + ".txt", DateTime.Now.ToString("HH:mm:ss") + " - " + e.Message.ToString() + Environment.NewLine);
            return false;
        }
        finally
        {
            da.Dispose();
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }

    }
    public virtual string GetValue(string strCmd, CommandType cmdType)
    {

        string rtrnVal = "";
        cn = new SqlConnection(constr);

        try
        {
            cmd.CommandText = strCmd;
            cmd.Connection = cn;
            cmd.CommandType = cmdType;
            cn.Open();
            rtrnVal = cmd.ExecuteScalar().ToString();
            if (rtrnVal == null)
            {
                rtrnVal = "";
            }
            return rtrnVal;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message );
            return "";
        }
        finally
        {
            da.Dispose();
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
    }
    public virtual int GetInt(string strCmd, CommandType cmdType)
    {
        int rtrnVal = 0;
        cn = new SqlConnection(constr);
        try
        {
            cmd.CommandText = strCmd;
            cmd.Connection = cn;
            cmd.CommandType = cmdType;
            cn.Open();
            rtrnVal = Convert.ToInt32(cmd.ExecuteScalar());
            if (rtrnVal == null)
            {
                rtrnVal = 0;
            }
            return rtrnVal;
        }
        catch (Exception)
        {
            return 0;
        }
            finally
            {
            da.Dispose();
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
                 }
            }
    }


         // Other managed resource this class uses.
        private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;

        // The class constructor.
        public CSBaseDALMS()
        {

        }
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!this.disposed)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                // Dispose managed resources.
                component.Dispose();
            }
            disposed = true;

        }
    }
        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~CSBaseDALMS()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
    public void Dispose()
    {
        Dispose(true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SupressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    }

}
