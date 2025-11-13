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
    public class clsMakeModels
    {
        public static List<MakeModelDTO> GetAllMakeModels()
        {
            List<MakeModelDTO> MakeModelsInfo = new List<MakeModelDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllMakeModels", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MakeModelsInfo.Add
                                    (
                                    new MakeModelDTO(
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetString(reader.GetOrdinal("ModelName"))
                                        ));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MakeModelsInfo = null;
            }
            return MakeModelsInfo;
        }
        public static MakeModelDTO GetMakeModel(int ModelID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetMakeModelBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModelID", ModelID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MakeModelDTO
                                    (
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetInt32(reader.GetOrdinal("MakeID")),
                                        reader.GetString(reader.GetOrdinal("ModelName"))
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
        public static int? AddNewMakeModel(MakeModelDTO mDTO)
        {
            int? ModelID = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewMakeModel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MakeID", mDTO.MakeID);
                        cmd.Parameters.AddWithValue("@ModelName", mDTO.ModelName);
                        var outputparam = new SqlParameter("@NewModelID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                        };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        ModelID = (int)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ModelID;
        }
        public static bool IsMakeModelExists(int ModelID)
        {
            bool Result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_IsMakeModelExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModelID", ModelID);
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
        public static bool DeleteMakeModel(int ModelID)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteMakeModelBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModelID", ModelID);
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
        public static bool UpdatedMakeModel(MakeModelDTO mDTO)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateMakeModelBy", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModelID", mDTO.ModelID);
                        cmd.Parameters.AddWithValue("@MakeID", mDTO.MakeID);
                        cmd.Parameters.AddWithValue("@ModelName", mDTO.ModelName);

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
