using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;

namespace ServiceRest_20190140104_EkaNovaPramudya
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        string koneksi = "Data Source=LAPTOP-GTRP6JSV;Initial Catalog=TI_UMY;Persist Security Info=True; User ID=sa;password=nova2000";
        SqlConnection conn;
        SqlCommand cmd;               

        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string message = "GAGAL";
            SqlConnection conn = new SqlConnection(koneksi);
            string query = String.Format("INSERT INTO dbo.Mahasiswa VALUES('{0}','{1}','{2}','{3}')", mhs.nama, mhs.nim, mhs.prodi, mhs.angkatan);
            SqlCommand cmd = new SqlCommand(query,conn);

            try 
            {
                conn.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                conn.Close();
                message = "SUKSES";
            } 
            catch(Exception e) 
            { 
                Console.WriteLine(e.Message);
                Console.WriteLine(query);
                message = "GAGAL";
            }

            return message;
        }
        
        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahasiswa = new List<Mahasiswa>();

            SqlConnection conn = new SqlConnection(koneksi);
            string query = "SELECT Nama, NIM, Prodi, Angkatan FROM dbo.Mahasiswa";
            SqlCommand cmd = new SqlCommand(query, conn);

            try 
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahasiswa.Add(mhs);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(query);
            }
            
            return mahasiswa;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            SqlConnection conn = new SqlConnection(koneksi);
            string query = String.Format("SELECT Nama, NIM, Prodi, Angkatan FROM dbo.Mahasiswa WHERE NIM = '{0}'", nim);
            SqlCommand cmd = new SqlCommand(query, conn);

            try 
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nama = reader.GetString(0);
                    mhs.nim = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(query);
            }            
            return mhs;
        }       
    }
}
