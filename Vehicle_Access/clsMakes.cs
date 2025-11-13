using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Access
{
    public class clsMakes
    {
        public static List<MakeDTO> GetAllMakes()
        {
            List<MakeDTO> MakesInfo = new List<MakeDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllMakes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MakesInfo.Add
                                    (
                                    new MakeDTO(
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetString(reader.GetOrdinal("Make"))
                                        ));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MakesInfo = null;
            }
            return MakesInfo;
        }
        public static MakeDTO GetMake(int MakeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetMakeBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", MakeID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MakeDTO
                                    (
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetString(reader.GetOrdinal("Make"))
                                    );
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public static int? AddNewMake(MakeDTO mDTO)
        {
            int? MakeID = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewMake", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Make", mDTO.Make);
                        var outputparam = new SqlParameter("@NewMakeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                        };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        MakeID = (int)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return MakeID;
        }
        public static bool IsMakeExistsBy(int MakeID)
        {
            bool Result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_IsMakeExistsBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", MakeID);
                        var outputparam = new SqlParameter("@Result", SqlDbType.Bit)
                        { Direction = ParameterDirection.Output, };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        Result = (bool)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Result;
        }
        public static bool DeleteMake(int MakeID)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteBodyBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", MakeID);
                        conn.Open();

                        RecordEffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RecordEffected > 0;
        }
        public static bool UpdatedMake(MakeDTO mDTO)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdatedMakeBy", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", mDTO.MakeID);
                        cmd.Parameters.AddWithValue("@Make", mDTO.Make);

                        RecordEffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RecordEffected > 0;
        }
    }
}
