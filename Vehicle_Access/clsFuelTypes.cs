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
    public class clsFuelTypes
    {
        public static List<FuelTypeDTO> GetAllFuelTypes()
        {
            List<FuelTypeDTO> FuelTypesInfo = new List<FuelTypeDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllFuelTypes", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FuelTypesInfo.Add
                                    (
                                    new FuelTypeDTO(
                                        reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                        reader.GetString(reader.GetOrdinal("FuelTypeName"))
                                        ));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FuelTypesInfo = null;
            }
            return FuelTypesInfo;
        }
        public static FuelTypeDTO GetFuelType(int FuelTypeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetFuelTypeBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FuelTypeID", FuelTypeID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new FuelTypeDTO
                                    (
                                        reader.GetInt32(reader.GetOrdinal("FuelTypeID")),
                                        reader.GetString(reader.GetOrdinal("FuelTypeName"))
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
        public static int? AddNewFuelType(FuelTypeDTO fDTO)
        {
            int? FuelTypeID = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddNewFuelType", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FuelTypeName", fDTO.FuelTypeName);
                        var outputparam = new SqlParameter("@FuelTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                        };
                        cmd.Parameters.Add(outputparam);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        FuelTypeID = (int)outputparam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return FuelTypeID;
        }
        public static bool IsFuelTypeExistsBy(int FuelTypeID)
        {
            bool Result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_IsFuelTypeExists", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FuelTypeID", FuelTypeID);
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
        public static bool DeletedFuelType(int FuelTypeID)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DeletedFuelTypeBy", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FuelTypeID", FuelTypeID);
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
        public static bool UpdatedFuelType(FuelTypeDTO fDTO)
        {
            int RecordEffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(clsGlobal.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdatedFuelTypeBy", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FuelTypeID", fDTO.FuelTypeID);
                        cmd.Parameters.AddWithValue("@FuelTypeName", fDTO.FuelTypeName);

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
