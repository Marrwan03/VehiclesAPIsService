using DTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Vehicle_Access
{
    public class clsSubModels
    {
        public static List<SubModelsDTO> GetAllSubModels()
        {
            List<SubModelsDTO> SubModelsInfo = new List<SubModelsDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllSubModels", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SubModelsInfo.Add
                                    (
                                    new SubModelsDTO(
                                        reader.GetInt32(reader.GetOrdinal("SubModelID")),
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetString(reader.GetOrdinal("SubModelName"))
                                        ));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                SubModelsInfo = null;
            }
            return SubModelsInfo;
        }
        public static SubModelsDTO GetSubModelBy(int SubModelID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetSubModelBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubModelID", SubModelID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new SubModelsDTO
                                    (
                                        reader.GetInt32(reader.GetOrdinal("SubModelID")),
                                        reader.GetInt32(reader.GetOrdinal("ModelID")),
                                        reader.GetString(reader.GetOrdinal("SubModelName"))
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
        public static int? AddNewSubModel(SubModelsDTO sDTO)
        {
            int? SubModelID = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewSubModel", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ModelID", sDTO.ModelID);
                        cmd.Parameters.AddWithValue("@SubModelName", sDTO.SubModelName);
                        var outputparam = new SqlParameter("@NewSubModelID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                        };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        SubModelID = (int)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return SubModelID;
        }
        public static bool IsSubModelExists(int SubModelID)
        {
            bool Result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_IsSubModelExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubModelID", SubModelID);
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
        public static bool DeleteSubModel(int SubModelID)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteSubModelBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubModelID", SubModelID);
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
        public static bool UpdatedSubModel(SubModelsDTO sDTO)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateSubModelBy", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubModelID", sDTO.SubModelID);
                        cmd.Parameters.AddWithValue("@ModelID", sDTO.ModelID);
                        cmd.Parameters.AddWithValue("@SubModelName", sDTO.SubModelName);

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
